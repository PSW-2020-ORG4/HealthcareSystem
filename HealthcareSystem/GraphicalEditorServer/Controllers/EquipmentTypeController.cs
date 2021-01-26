using Backend.Model.Manager;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypeController : ControllerBase
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypeController(IEquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpPost]
        public ActionResult AddEquipmentType([FromBody] EquipmentType equipmentType)
        {
            EquipmentType addedEquipmentType = _equipmentTypeService.AddEquipmentType(equipmentType);
            return Ok(addedEquipmentType.Id);
        }

        [HttpGet]
        public ActionResult GetAllEquipmentTypes()
        {
            List<EquipmentTypeDTO> equipmentTypesDTO = new List<EquipmentTypeDTO>();

            foreach (EquipmentType equipmentType in _equipmentTypeService.GetEquipmentTypes())
            {
                equipmentTypesDTO.Add(EquipmentTypesMapper.EquipmentType_To_EquipmentTypeDTO(equipmentType));
            }

            return Ok(equipmentTypesDTO);
        }

        [HttpGet("{id}")]
        public EquipmentType GetEquipmentType(int id)
        {
            return _equipmentTypeService.GetEquipmentTypeById(id);
        }
    }
}
