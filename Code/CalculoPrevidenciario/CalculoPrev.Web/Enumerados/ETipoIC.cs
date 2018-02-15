using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculoPrev.Web.Enumerados
{
    public class ETipoIC
    {
        public ETipoIC(int codigo, string desc)
        {
            Codigo = codigo;
            Descricao = desc;
        }
        
        /// <summary>
        /// CODIGO DO TIPO DE DADO
        /// </summary>
        public virtual int Codigo
        {
            get;
            set;
        }


        /// <summary>
        /// DESCRICAO DO TIPO DE DADO
        /// </summary>
        public virtual string Descricao
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ETipoIC> getAll()
        {
            List<ETipoIC> ret = new List<ETipoIC>();
            ret.Add(new ETipoIC(1, "Índice em percentagem"));
            ret.Add(new ETipoIC(2, "Índice em valor"));
            ret.Add(new ETipoIC(3, "Índice com data de aniversário"));
            ret.Add(new ETipoIC(4, "Índice para somatório (sem capitalização)"));
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getDescricao(int codigo)
        {
            switch (codigo)
            {
                case 1: return "Índice em percentagem";
                case 2: return "Índice em valor";
                case 3: return "Índice com data de aniversário";
                case 4: return "Índice para somatório (sem capitalização)";
                default: return "Default";
            }
        }
    }
}