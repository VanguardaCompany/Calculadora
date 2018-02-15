using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculoPrev.Web.Enumerados
{
    public class EPeriodicidadeIC
    {
        public EPeriodicidadeIC(int codigo, string desc)
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
        public static List<EPeriodicidadeIC> getAll()
        {
            List<EPeriodicidadeIC> ret = new List<EPeriodicidadeIC>();
            ret.Add(new EPeriodicidadeIC(1, "Diário"));
            ret.Add(new EPeriodicidadeIC(2, "Mensal"));
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
                case 1: return "Diário";
                case 2: return "Mensal";
                default: return "Default";
            }
        }
    }
}