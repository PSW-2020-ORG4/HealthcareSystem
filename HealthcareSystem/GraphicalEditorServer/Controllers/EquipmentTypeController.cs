using Backend.Model.Exceptions;
using Backend.Model.Manager;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypeController: ControllerBase
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypeController(IEquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpPost]
        public ActionResult AddEquipmentType([FromBody] EquipmentType equipmentType)
        {
                EquipmentType addedEquipmentType=_equipmentTypeService.AddEquipmentType(equipmentType);            
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
        public IActionResult GetEquipmentTypeById(int id)
        {
            try
            {
                EquipmentType equipmentType = _equipmentTypeService.GetEquipmentTypeById(id);
                return Ok(EquipmentTypesMapper.EquipmentType_To_EquipmentTypeDTO(equipmentType));
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
