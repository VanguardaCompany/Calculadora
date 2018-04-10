using Calculadora.Simulador.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Calculadora.Simulador.Utils;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Calculadora.Simulador
{
    public static class SimuladorINSS
    {
        /// <summary>
        /// recebe o um objeto do tipo SimulacaoINSS e realiza os cálculos e simulações, devolvendo o resultado em um objeto de mesmo tipo SimulacaoINSS
        /// </summary>
        /// <param name="simulacaoINSS"></param>
        /// <returns></returns>
        /// 
        public static SimulacaoINSS Simular(SimulacaoINSS simulacaoINSS)
        {
            if (simulacaoINSS != null)
            {
                if (simulacaoINSS.ListaVinculosTrabalhistas != null && simulacaoINSS.ListaVinculosTrabalhistas.Count > 0)
                {
                    simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.AddRange(simulacaoINSS.ListaVinculosTrabalhistas);

                    simulacaoINSS = PriorizarPeriodosComIntersecao(simulacaoINSS); //trabalha com a lista Auxiliar;
                    simulacaoINSS = CalcularResultados(simulacaoINSS); //trabalha com a lista Auxiliar;

                    simulacaoINSS = CalcularPeriodosEmDuplicidade(simulacaoINSS);//trabalha com a lista oficial;
                    simulacaoINSS = AdicionarPeriodosSemLancamento(simulacaoINSS);//trabalha com a lista auxiliar e oficial;
                }
            }
            return simulacaoINSS;
        }

        //public static List<VinculoTrabalhista> ExtrairVinculosTrabalhistasCNIS(Stream file)//string file)//
        //{
        //    string searchText = "Seq.";
        //    StringBuilder text = new StringBuilder();
        //    List<string> lines = new List<string>();
        //    List<string> listaVinculosPdf = new List<string>();
        //    List<VinculoTrabalhista> listaVinculos = new List<VinculoTrabalhista>();
        //    bool ehVinculo = false;

        //    try
        //    {
        //        using (PdfReader reader = new PdfReader(file))
        //        {
        //            for (int i = 1; i <= reader.NumberOfPages; i++)
        //            {
        //                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
        //                string currentText = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
        //                text.Append(currentText);
        //            }
        //        }

        //        lines = text.ToString().Trim().Split('\n').ToList();
        //        foreach (string item in lines)
        //        {
        //            if (!string.IsNullOrEmpty(item))
        //            {
        //                if (ehVinculo && !item.ToUpper().Contains("Benefício".ToUpper()) && !item.ToUpper().Contains("Segurado Especial".ToUpper()))
        //                    listaVinculosPdf.Add(item);

        //                ehVinculo = (item.ToUpper().Contains(searchText.ToUpper()));
        //            }
        //        }

        //        foreach (string linha in listaVinculosPdf)
        //        {
        //            DateTime dataInicio = DateTime.MinValue;
        //            DateTime dataFim = DateTime.MinValue;
        //            string nomeEmpregador = String.Empty;
        //            string[] colunas = linha.Split(' ');
        //            string padraoCnpj = @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)";
        //            int aux = -1;

        //            foreach (string coluna in colunas)
        //            {
        //                if (int.TryParse(coluna, out aux) || (Regex.IsMatch(coluna, padraoCnpj))) continue;

        //                if (dataInicio == DateTime.MinValue && !DateTime.TryParse(coluna, out dataInicio))
        //                {
        //                    nomeEmpregador = nomeEmpregador + " " + coluna;
        //                    continue;
        //                }

        //                //if (dataInicio == DateTime.MinValue && DateTime.TryParse(coluna, out dataInicio)) continue;

        //                if (dataInicio != DateTime.MinValue && dataFim == DateTime.MinValue)
        //                    if (DateTime.TryParse(coluna, out dataFim)) break;
        //            }

        //            if (dataInicio != DateTime.MinValue && dataFim != DateTime.MinValue)
        //                listaVinculos.Add(new VinculoTrabalhista(dataInicio, dataFim, 0, nomeEmpregador.TrimStart()));
        //        }

        //        return listaVinculos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private static SimulacaoINSS CalcularPeriodosEmDuplicidade(SimulacaoINSS simulacaoINSS)
        {
            foreach (VinculoTrabalhista vinculo in simulacaoINSS.ListaVinculosTrabalhistas)
            {
                foreach (VinculoTrabalhista vinculoAux in simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Where(v => v.Priorizado).ToList())
                {
                    if (vinculo.Id != vinculoAux.Id)
                    {
                        DateTimeInterval intersecao = DateTimeUtils.GetIntervalIntersection(new DateTimeInterval(vinculo.DataInicio, vinculo.DataFim), new DateTimeInterval(vinculoAux.DataInicio, vinculoAux.DataFim));

                        if (intersecao.From != null && intersecao.To != null &&
                            vinculo.DataInicio >= vinculoAux.DataInicio && vinculo.DataInicio <= vinculoAux.DataFim)
                        {
                            var periodo = new Duracao(intersecao.From.Value, intersecao.To.Value);
                            vinculo.PeriodoEmDuplicidade = Duracao.SomarDuracoes(periodo, vinculo.PeriodoEmDuplicidade);
                        }
                    }
                }
            }

            return simulacaoINSS;
        }

        private static SimulacaoINSS AdicionarPeriodosSemLancamento(SimulacaoINSS simulacaoINSS)
        {
            //ordenar a lista em ordem de inicio.
            List<VinculoTrabalhista> vinculosPriorizados = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Where(v => v.Priorizado).OrderBy(v => v.DataInicio).ToList();

            for (int indice = 1; indice < vinculosPriorizados.Count; indice++)
            {
                VinculoTrabalhista vinculo1 = vinculosPriorizados[indice - 1];
                VinculoTrabalhista vinculo2 = vinculosPriorizados[indice];
                if ((vinculo2.DataInicio - vinculo1.DataFim).TotalSeconds > 1)
                {
                    simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Add(new VinculoTrabalhista(vinculo1.DataFim.AddSeconds(1), vinculo2.DataInicio.AddSeconds(-1), 0, "", true, false, true));
                    simulacaoINSS.ListaVinculosTrabalhistas.Add(new VinculoTrabalhista(vinculo1.DataFim.AddSeconds(1), vinculo2.DataInicio.AddSeconds(-1), 0, "", true, false, true));
                    //ordenar a lista em ordem de inicio.
                    simulacaoINSS.ListaVinculosTrabalhistasAuxiliar = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.OrderBy(v => v.DataInicio).ToList();
                    simulacaoINSS.ListaVinculosTrabalhistas = simulacaoINSS.ListaVinculosTrabalhistas.OrderBy(v => v.DataInicio).ToList();
                }
            }
            return simulacaoINSS;
        }

        private static SimulacaoINSS CalcularResultados(SimulacaoINSS simulacaoINSS)
        {
            List<VinculoTrabalhista> listaVinculos = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Where(v => v.Priorizado).ToList();

            foreach (VinculoTrabalhista vinculo in listaVinculos)
            {
                simulacaoINSS.TempoSimulado = Duracao.SomarDuracoes(new Duracao(vinculo.DataInicioSemIntersecao, vinculo.DataFimSemIntersecao), simulacaoINSS.TempoSimulado);
            }

            return simulacaoINSS;
        }

        private static SimulacaoINSS PriorizarPeriodosComIntersecao(SimulacaoINSS simulacaoINSS, bool tratarIntersecao = false)
        {
            //ordenar a lista em ordem de inicio.
            simulacaoINSS.ListaVinculosTrabalhistasAuxiliar = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.OrderBy(v => v.DataInicio).ToList();

            if (simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Count > 1)
            {
                for (int indice = 0; indice < simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Count; indice++)
                {
                    VinculoTrabalhista vinculo = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar[indice];

                    for (int indiceAux = indice + 1; indiceAux < simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Count; indiceAux++)
                    {
                        if (indiceAux > 0)
                        {
                            VinculoTrabalhista vinculoAux = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar[indiceAux];

                            if (!tratarIntersecao)
                                //caso os períodos sejam identicos, prioriza o período com maior contribuição.
                                TratarPeriodosIdenticos(vinculo, vinculoAux);
                            else
                                //caso haja alguma interseção remove as interseções priorizando o período selecionado pelo usuário ou o maior valor de contribuição.
                                TratarPeriodosComIntersecao(simulacaoINSS, vinculo, vinculoAux);
                        }
                    }
                }
            }
            else if (simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Count == 1)
            {
                simulacaoINSS.ListaVinculosTrabalhistasAuxiliar[0].PriorizadoSistema = true;
            }

            if (!tratarIntersecao)
                PriorizarPeriodosComIntersecao(simulacaoINSS, true);

            return simulacaoINSS;
        }

        private static void TratarPeriodosIdenticos(VinculoTrabalhista vinculo, VinculoTrabalhista vinculoAux)
        {
            if (vinculoAux.DataInicioSemIntersecao == vinculo.DataInicioSemIntersecao && vinculoAux.DataFimSemIntersecao == vinculo.DataFimSemIntersecao)
            {
                if (vinculo.ValorContribuicao >= vinculoAux.ValorContribuicao)
                    vinculo.PriorizadoSistema = true;
                else
                    vinculoAux.PriorizadoSistema = true;
            }
            else
            {
                vinculo.PriorizadoSistema = true;
                vinculoAux.PriorizadoSistema = true;
            }
        }

        private static void TratarPeriodosComIntersecao(SimulacaoINSS simulacaoINSS, VinculoTrabalhista vinculo, VinculoTrabalhista vinculoAux)
        {
            //tratar apenas os periodos priorizados
            if (!vinculo.Priorizado || !vinculoAux.Priorizado) return;

            if (vinculoAux.DataInicioSemIntersecao >= vinculo.DataInicioSemIntersecao && vinculoAux.DataInicioSemIntersecao <= vinculo.DataFimSemIntersecao)
            {
                if (vinculo.ValorContribuicao >= vinculoAux.ValorContribuicao || vinculo.PriorizadoUsuario)
                {

                    if (vinculoAux.DataFimSemIntersecao > vinculo.DataFimSemIntersecao)
                    {
                        vinculoAux.DataInicioSemIntersecao = vinculo.DataFimSemIntersecao.AddSeconds(1);
                        vinculoAux.PriorizadoSistema = true;
                    }
                    else
                        vinculoAux.PriorizadoSistema = false;
                }
                else
                {

                    if (vinculo.DataFimSemIntersecao > vinculoAux.DataFimSemIntersecao)
                    {
                        simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Add(new VinculoTrabalhista(vinculoAux.DataFimSemIntersecao.AddSeconds(1), vinculo.DataFimSemIntersecao, vinculo.ValorContribuicao, vinculo.NomeEmpregador, true, false, false, vinculo.Id));
                        //ordenar a lista em ordem de inicio.
                        simulacaoINSS.ListaVinculosTrabalhistasAuxiliar = simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.OrderBy(v => v.DataInicioSemIntersecao).ToList();
                    }
                    vinculoAux.PriorizadoSistema = true;

                    vinculo.DataFimSemIntersecao = vinculoAux.DataInicioSemIntersecao.AddSeconds(-1);
                    vinculo.PriorizadoSistema = true;
                }
            }
        }
    }
}
