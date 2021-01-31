using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var specialties = _specialtyService.GetAll().Select(s => s.GetMemento());
            return Ok(specialties);
        }
    }
}