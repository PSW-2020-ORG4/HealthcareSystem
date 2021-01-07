using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Model.Pharmacies;
using Backend.Service;
using Backend.Service.Pharmacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Settings;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionBenefitService _actionBenefitService;
        private readonly IPharmacyService _pharmacyService;
        private readonly ServiceSettings _serviceSettings;

        public ActionController(IActionBenefitService actionBenefitService,
                                IPharmacyService pharmacyService,
                                IOptions<ServiceSettings> serviceSettings)
        {
            _actionBenefitService = actionBenefitService;
            _pharmacyService = pharmacyService;
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
