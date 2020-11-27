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

        [HttpPost]
        public ActionResult AddEquipment([FromBody] String test)
        {
            string[] atributes = test.Split(",");
            String final = "{";
            final += test;
            final += "}";

            if (atributes.Length<3) {
                 NonConsumableEquipment nonCosumableEquipment = JsonConvert.DeserializeObject<NonConsumableEquipment>(final);

                _nonConsumableEquipmentService.newNonConsumableEquipment(nonCosumableEquipment);
            }
            else
            {
                ConsumableEquipment cosumableEquipment = JsonConvert.DeserializeObject<ConsumableEquipment>(final);

                _consumableEquipmentService.newConsumableEquipment(cosumableEquipment);
            }
            return Ok();
        }
    }

}
