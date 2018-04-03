using Calculadora.Simulador.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Calculadora.Simulador.Utils;

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

        private static SimulacaoINSS CalcularPeriodosEmDuplicidade(SimulacaoINSS simulacaoINSS)
        {
            foreach (VinculoTrabalhista vinculo in simulacaoINSS.ListaVinculosTrabalhistas)
            {
                foreach (VinculoTrabalhista vinculoAux in simulacaoINSS.ListaVinculosTrabalhistasAuxiliar.Where(v => v.Priorizado).ToList())
                {
                    if (vinculo.Id != vinculoAux.Id)
                    {
                        DateTimeInterval intersecao = DateTimeUtils.GetIntervalIntersection(new DateTimeInterval(vinculo.DataInicio, vinculo.DataFim), new DateTimeInterval(vinculoAux.DataInicio, vinculoAux.DataFim));

                        if (intersecao.From != null && intersecao.To != null)
                        {
                            var periodo = new Duracao(intersecao.From.Value, intersecao.To.Value);
                            vinculo.PeriodoEmDuplicidade = new Duracao(periodo.TotalDias + vinculo.PeriodoEmDuplicidade.TotalDias);
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

            simulacaoINSS.TempoSimulado = new Duracao((int)listaVinculos.Select(v => new Duracao(v.DataInicioSemIntersecao, v.DataFimSemIntersecao).TotalDias).Sum());

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
                    //vinculo.DataInicioSemIntersecao = vinculo.DataInicioSemIntersecao;
                    //vinculo.DataFimSemIntersecao = vinculo.DataFimSemIntersecao;
                    //vinculo.PriorizadoSistema = true;

                    if (vinculoAux.DataFimSemIntersecao > vinculo.DataFimSemIntersecao)
                    {
                        vinculoAux.DataInicioSemIntersecao = vinculo.DataFimSemIntersecao.AddSeconds(1);
                        //vinculoAux.DataFimSemIntersecao = vinculoAux.DataFimSemIntersecao;
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

                    //vinculo.DataInicioSemIntersecao = vinculo.DataInicioSemIntersecao;
                    vinculo.DataFimSemIntersecao = vinculoAux.DataInicioSemIntersecao.AddSeconds(-1);
                    vinculo.PriorizadoSistema = true;
                }
            }
        }
    }
}
