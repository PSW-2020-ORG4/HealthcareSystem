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
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionBenefitService _actionBenefitService;
        private readonly IPharmacyService _pharmacyService;

        public ActionController(IActionBenefitService actionBenefitService, IPharmacyService pharmacyService)
        {
            _actionBenefitService = actionBenefitService;
            _pharmacyService = pharmacyService;
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
