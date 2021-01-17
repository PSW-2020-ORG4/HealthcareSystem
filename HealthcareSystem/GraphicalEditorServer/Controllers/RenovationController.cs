using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Model.PerformingExamination;
using Backend.Model.Enums;
using Backend.Service;
using Backend.Service.ExaminationAndPatientCard;
using GraphicalEditor.DTO;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Service.RenovationService;
using Backend.Model.Manager;
using Model.Manager;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenovationController : ControllerBase
    {
        private readonly IRenovationService _baseRenovationService;

        public RenovationController(IRenovationService baseRenovationService)
        {
            _baseRenovationService = baseRenovationService;
        }

        [HttpPost]
        public ActionResult AddBaseRenovation(BaseRenovationDTO baseRenovationDTO)
        {
            BaseRenovation addedBaseRenovation = _baseRenovationService.AddBaseRenovation(BaseRenovationMapper.BaseRenovationDTOToBaseRenovation(baseRenovationDTO));
             if (addedBaseRenovation == null) {
                return NotFound("NotFound");
            }
            return Ok();
        }

        [HttpDelete("/{baseRenovationId}")]
        public ActionResult DeleteBaseRenovation(int baseRenovationId)
        {
            _baseRenovationService.DeleteBaseRenovation(baseRenovationId);
            return Ok();
        }

        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetBaseRenovationsByRoomNumber(int roomNumber)
        {
            try
            {
                List<BaseRenovation> baseRenovationsInRoom = _baseRenovationService.GetBaseRenovationByRoomNumber(roomNumber);
                if (baseRenovationsInRoom.Count == 0)
                {
                    return NotFound("NotFound");
                }
                return Ok(baseRenovationsInRoom);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPost("getAlternativeAppointments")]
        public IActionResult GetAlternativeTermsForBaseRenovation(BaseRenovationDTO baseRenovatonDTO)
        {
            List<RenovationPeriodDTO> alternativeAppointments = new List<RenovationPeriodDTO>();
            try
            {
                _baseRenovationService.GetAlternativeAppointemntsForBaseRenovation(new RenovationPeriod(baseRenovatonDTO.StartTime, baseRenovatonDTO.EndTime),baseRenovatonDTO.RoomId).ForEach(r => alternativeAppointments.Add(RenovationPeriodMapper.RenovationPeriodToRenovationPeriodDTO(r)));
                if (alternativeAppointments.Count == 0)
                {
                    return NotFound("NotFound");
                }
                return Ok(alternativeAppointments);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllBaseRenovations()
        {
            try
            {
                List<BaseRenovation> baseRenovations = _baseRenovationService.GetAllBaseRenovations();
                if (baseRenovations.Count == 0)
                {
                    return NotFound("NotFound");
                }
                return Ok(baseRenovations);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("byBaseRenovationId/{baseRenovationId}")]
        public IActionResult GetBaseRenovationsById(int baseRenovationId)
        {
            try
            {
                BaseRenovation baseRenovation = _baseRenovationService.GetBaseRenovationById(baseRenovationId);
                return Ok(baseRenovation);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
