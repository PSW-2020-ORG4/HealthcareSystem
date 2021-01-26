using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.CustomException;

namespace UserService.Controllers
{
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is ValidationException)
                return BadRequest(context.Error.Message);
            else if (context.Error is NotFoundException)
                return NotFound(context.Error.Message);
            else
                return Problem();
        }

        [Route("error/dev")]
        public IActionResult DevelopmenError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is ValidationException)
                return BadRequest(context.Error.Message);
            else if (context.Error is NotFoundException)
                return NotFound(context.Error.Message);
            else
                return Problem(detail: context.Error.StackTrace,
                               title: context.Error.Message);
        }
    }
}