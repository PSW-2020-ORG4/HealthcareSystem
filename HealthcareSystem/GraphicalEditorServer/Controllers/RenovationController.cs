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
        private readonly IRenovationService _renovationService;

        public RenovationController(IRenovationService renovationService)
        {
            _renovationService = renovationService;
        }

        [HttpPost("addBaseRenovation")]
        public ActionResult AddBaseRenovation(BaseRenovationDTO baseRenovationDTO)
        {
            BaseRenovation addedBaseRenovation = _renovationService.AddBaseRenovation(BaseRenovationMapper.BaseRenovationDTOToBaseRenovation(baseRenovationDTO));
             if (addedBaseRenovation == null) {
                return NotFound("NotFound");
            }
            return Ok();
        }
        [HttpPost("addMergeRenovation")]
        public ActionResult AddMergeRenovation(MergeRenovationDTO mergeRenovationDTO)
        {
            MergeRenovation addedBaseRenovation = (MergeRenovation)_renovationService.AddMergeRenovation(MergeRenovationMapper.MergeRenovationDTOToMergeRenovation(mergeRenovationDTO));
            if (addedBaseRenovation == null)
            {
                return NotFound("NotFound");
            }
            return Ok();
        }
        [HttpPost("addDivideRenovation")]
        public ActionResult AddDivideRenovation(DivideRenovationDTO divideRenovationDTO)
        {
            DivideRenovation addedBaseRenovation = (DivideRenovation)_renovationService.AddDivideRenovation(DivideRenovationMapper.DivideRenovationDTOToDivideRenovation(divideRenovationDTO));
            if (addedBaseRenovation == null)
            {
                return NotFound("NotFound");
            }
            return Ok();
        }

        [HttpDelete("deleteByRoomId/{baseRenovationId}")]
        public ActionResult DeleteBaseRenovation(int baseRenovationId)
        {
            _renovationService.DeleteRenovation(baseRenovationId);
            return Ok();
        }

        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetBaseRenovationsByRoomNumber(int roomNumber)
        {
            try
            {
                List<BaseRenovation> baseRenovationsInRoom = _renovationService.GetRenovationByRoomNumber(roomNumber);
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
        [HttpPost("getBaseRenovationAlternativeAppointments")]
        public IActionResult GetAlternativeAppointmentsForBaseRenovation(BaseRenovationDTO baseRenovatonDTO)
        {
            List<RenovationPeriodDTO> alternativeAppointments = new List<RenovationPeriodDTO>();
            try
            {
                _renovationService.GetAlternativeAppointemntsForBaseRenovation(new RenovationPeriod(baseRenovatonDTO.StartTime, baseRenovatonDTO.EndTime),baseRenovatonDTO.RoomId).ForEach(r => alternativeAppointments.Add(RenovationPeriodMapper.RenovationPeriodToRenovationPeriodDTO(r)));
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
        [HttpPost("getMergeRenovationAlternativeAppointments")]
        public IActionResult GetAlternativeAppointmentsForMergeRenovation(MergeRenovationDTO mergeRenovationDTO)
        {
            List<RenovationPeriodDTO> alternativeAppointments = new List<RenovationPeriodDTO>();
            try
            {
                 _renovationService.GetMergeRenovationAlternativeAppointmets(MergeRenovationMapper.MergeRenovationDTOToMergeRenovation(mergeRenovationDTO)).ToList().ForEach(x=> alternativeAppointments.Add(new RenovationPeriodDTO(x.BeginDate,x.EndDate)));
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
        [HttpPost("getDivideRenovationAlternativeAppointments")]
        public IActionResult GetAlternativeAppointmentsForDivideRenovation(DivideRenovationDTO divideRenovationDTO)
        {
            List<RenovationPeriodDTO> alternativeAppointments = new List<RenovationPeriodDTO>();
            try
            {
                _renovationService.GetDivideRenovationAlternativeAppointmets(DivideRenovationMapper.DivideRenovationDTOToDivideRenovation(divideRenovationDTO)).ToList().ForEach(x => alternativeAppointments.Add(new RenovationPeriodDTO(x.BeginDate, x.EndDate)));
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
                List<BaseRenovation> baseRenovations = _renovationService.GetAllRenovations();
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
                BaseRenovation baseRenovation = _renovationService.GetRenovationById(baseRenovationId);
                return Ok(BaseRenovationMapper.BaseRenovationToBaseRenovationDTO(baseRenovation));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
