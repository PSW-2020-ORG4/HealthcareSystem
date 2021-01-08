using Backend.Service;
using Backend.Service.Pharmacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Settings;
using System.Collections.Generic;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionBenefitService _actionBenefitService;
        private readonly ServiceSettings _serviceSettings;

        public ActionController(IActionBenefitService actionBenefitService,
                                IOptions<ServiceSettings> serviceSettings)
        {
            _actionBenefitService = actionBenefitService;
            _serviceSettings = serviceSettings.Value;
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public IActionResult GetActionBenefits()
        {
            List<ActionBenefitDTO> actionBenefitDTOs = new List<ActionBenefitDTO>();

            _actionBenefitService.GetPublicActionsBenefits().ForEach(action => actionBenefitDTOs.Add(ActionBenefitMapper.ActionBenefitToActionBenefitDTO(action)));
            return Ok(actionBenefitDTOs);

        }
    }
}
