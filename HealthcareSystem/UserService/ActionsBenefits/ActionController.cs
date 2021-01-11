using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserService.ActionsBenefits
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionBenefitRepository _repository;

        public ActionController(IActionBenefitRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetActionBenefits()
        {
            // NOTE: this will be removed from this service as soon as possible
            return Ok(_repository.GetPublic());
        }
    }
}