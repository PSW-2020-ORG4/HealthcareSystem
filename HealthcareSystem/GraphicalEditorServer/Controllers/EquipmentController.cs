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
        private readonly INonConsumableEquipmentService _nonConsumableEquipmentService;
        private readonly IConsumableEquipmentService _consumableEquipmentService;

        public EquipmentController(INonConsumableEquipmentService nonConsumableEquipmentService, IConsumableEquipmentService consumableEquipmentService)
        {
            _nonConsumableEquipmentService = nonConsumableEquipmentService;
            _consumableEquipmentService = consumableEquipmentService; 
        }

        [HttpPost("consumable")]
        public ActionResult AddConsumableEquipment([FromBody] String JSONString)
        {
            String JSONContent = StringToJSONFormat(JSONString);            

            ConsumableEquipment cosumableEquipment = JsonConvert.DeserializeObject<ConsumableEquipment>(JSONContent);
            _consumableEquipmentService.newConsumableEquipment(cosumableEquipment);

            return Ok();
        }

        [HttpPost("nonconsumable")]
        public ActionResult AddNonConsumableEquipment([FromBody] String JSONString)
        {
            String JSONContent = StringToJSONFormat(JSONString);

            NonConsumableEquipment nonCosumableEquipment = JsonConvert.DeserializeObject<NonConsumableEquipment>(JSONContent);
            _nonConsumableEquipmentService.newNonConsumableEquipment(nonCosumableEquipment);

            return Ok();
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
