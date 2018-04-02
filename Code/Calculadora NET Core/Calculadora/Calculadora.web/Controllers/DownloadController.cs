using System.IO;
using System.Threading.Tasks;
using Calculadora.Domain.Models;
using Calculadora.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WkWrap.Core;


namespace Calculadora.web.Controllers
{

    public class DownloadController : Controller
    {
        private readonly IViewRenderService _viewRenderService;

        public DownloadController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

       

    }
    
}