using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    [Route("Error")]
    [AllowAnonymous]
    public class ErrorController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Error()
        {
            var exDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exDetails.Path;
            ViewBag.ExceptionMessage = exDetails.Error.Message;

            return View();
        }
    }
}
