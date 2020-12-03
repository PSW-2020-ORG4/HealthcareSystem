using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServer.DTO;
using Newtonsoft.Json;
using Model.Manager;
using Backend.Model.Manager;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEquipmentTypeService _equipmentTypeService;


        public EquipmentController(IEquipmentService equipmentService, IEquipmentTypeService equipmentTypeService)
        {
            _equipmentService = equipmentService;
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpPost]
        public ActionResult AddEquipment([FromBody] Equipment equipment)
        {
            Equipment addedEquipment =_equipmentService.AddEquipment(equipment);
            return Ok(addedEquipment.Id);
        }

        private string StringToJSONFormat(string JSONString)
        {
            string[] atributes = JSONString.Split(",");
            String JSONContent = "{";
            JSONContent += JSONString;
            JSONContent += "}";
            return JSONContent;
        }

    }

}
