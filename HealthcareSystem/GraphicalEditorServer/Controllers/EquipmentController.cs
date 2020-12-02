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


namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService; 
        }

        [HttpPost]
        public ActionResult AddEquipment([FromBody] String JSONString)
        {
            String JSONContent = StringToJSONFormat(JSONString);
            Equipment equipment = JsonConvert.DeserializeObject<Equipment>(JSONContent);
            Equipment addedEquipment =_equipmentService.newEquipment(equipment);
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
