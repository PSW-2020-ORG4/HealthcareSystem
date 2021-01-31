using Backend.Model.Exceptions;
using Backend.Model.PerformingExamination;
using Backend.Service.ExaminationAndPatientCard;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentInExaminationController : ControllerBase
    {
        private readonly IEquipmentInExaminationService _equipmentInExaminationService;

        public EquipmentInExaminationController(IEquipmentInExaminationService equipmentInExaminationService)
        {
            _equipmentInExaminationService = equipmentInExaminationService;
        }

        [HttpPost("schedule/")]
        public ActionResult AddEquipmentInExamination([FromBody] List<EquipmentInExaminationDTO> equipmentInExaminationDTOs)
        {
            List<EquipmentInExaminationDTO> addedEquipmentInExaminationDTOs = new List<EquipmentInExaminationDTO>();
            foreach (EquipmentInExaminationDTO equipmentInExaminationDTO in equipmentInExaminationDTOs)
            {
                EquipmentInExamination equipmentInExamination = EquipmentInExaminationMapper.EquipmentInExaminationDTOToEquipmentInExamination(equipmentInExaminationDTO);
                EquipmentInExamination addedEquipment = _equipmentInExaminationService.AddEquipmentInExamination(equipmentInExamination);
                addedEquipmentInExaminationDTOs.Add(EquipmentInExaminationMapper.EquipmentInExaminationToEquipmentInExaminationDTO(addedEquipment));
            }
            return Ok(addedEquipmentInExaminationDTOs);
        }
        [HttpGet("{examinationID}")]
        public IActionResult GetEquipmentByExaminationId(int examinationID)
        {
            try
            {
                List<EquipmentInExamination> equipmentInExamination = _equipmentInExaminationService.GetEquipmentInExaminationFromExaminationID(examinationID);
                List<EquipmentInExaminationDTO> equipmentInExaminationDTOs = new List<EquipmentInExaminationDTO>();
                foreach (var singleEquipmentInExamination in equipmentInExamination)
                {
                    equipmentInExaminationDTOs.Add(EquipmentInExaminationMapper.EquipmentInExaminationToEquipmentInExaminationDTO(singleEquipmentInExamination));
                }
                return Ok(equipmentInExaminationDTOs);
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


    }
}
