using Microsoft.AspNetCore.Mvc;
using System;

namespace NotificationService
{
    [Route("api/notify")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost("activation")]
        public IActionResult Activation(ActivationRequestDTO request)
        {
            try
            {
                _service.SendActivationRequest(
                    new ActivationRequest(request.Name, request.Email, request.ActivationLink));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            return NoContent();
        }
    }
}