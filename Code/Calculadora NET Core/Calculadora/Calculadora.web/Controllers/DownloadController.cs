using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WkWrap.Core;


namespace Calculadora.web.Controllers
{

    public class DownloadController : Controller
    {       

        public IActionResult DownloadPdf()
        {
            var htmlContent = "<div>Hi</div>";
            var wkhtmltopdf = new FileInfo(@"C:\Users\rerum\Documents\GitHub\Calculadora\Code\Calculadora NET Core\Calculadora\Calculadora.web\wwwroot\Rotativa\wkhtmltopdf.exe");
            var converter = new HtmlToPdfConverter(wkhtmltopdf);
            var pdfBytes = converter.ConvertToPdf(htmlContent);

            FileResult fileResult = new FileContentResult(pdfBytes, "application/pdf");
            fileResult.FileDownloadName = "ResultadoCalculo.pdf";
            return fileResult;
        }

        /// <summary>
        /// Renderiza a view recebida para o formato em string.
        /// </summary>
        /// <param name="viewName">nome da view a ser renderizada.</param>
        /// <param name="model">model associado a visão.</param>
        /// <returns>string com a visão dos dados.</returns>
        //public static string RenderViewToString(this Controller controller, string viewName, object model)
        //{
        //    var context = controller.ControllerContext;
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = context.RouteData.GetRequiredString("action");

        //    var viewData = new ViewDataDictionary(model);

        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = Microsoft.AspNetCore.Mvc.ViewEngines.Engines.FindPartialView(context, viewName);
        //        var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
        //        viewResult.View.Render(viewContext, sw);

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
    }
    
}