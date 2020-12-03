using Backend.Model.Exceptions;
using Backend.Model.Manager;
using Backend.Service.RoomAndEquipment;
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
            List<EquipmentType> equipmentTypes = new List<EquipmentType>();
            try
            {
                equipmentTypes = _equipmentTypeService.GetEquipmentTypes();
                return Ok(equipmentTypes);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public EquipmentType GetEquipmentType(int id) {
            return _equipmentTypeService.GetEquipmentTypeById(id);
        }
    }
}
