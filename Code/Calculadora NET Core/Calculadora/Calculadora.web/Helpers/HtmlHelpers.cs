using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Calculadora.web
{
    public static class HtmlHelpers
    {

        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string TempoPorExtenso(this IHtmlHelper htmlHelper, int anos, int meses, int dias)
        {
            string tempo = "";
            if (anos > 0)
                if (anos == 1)
                    tempo = tempo + anos + " ano ";
                else
                    tempo = tempo + anos + " anos ";
            if (meses > 0)
            {
                if (!string.IsNullOrEmpty(tempo))
                    tempo = dias > 0 ? tempo + ", " : tempo + " e ";
                if (meses == 1)
                    tempo = tempo + meses + " mês ";
                else
                    tempo = tempo + meses + " meses ";
            }
            if (dias > 0)
            {
                if (!string.IsNullOrEmpty(tempo))
                    tempo = tempo + "e ";
                if (dias == 1)
                    tempo = tempo + dias + " dia";
                else
                    tempo = tempo + dias + " dias";
            }

            return tempo;
        }

    }
}
