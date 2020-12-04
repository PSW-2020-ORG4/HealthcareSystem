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
using Backend.Model.Exceptions;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentInRoomsController : ControllerBase
    {
        private readonly IEquipmentInRoomsService _equipmentInRoomService;

        public EquipmentInRoomsController(IEquipmentInRoomsService equipmentInRoomService)
        {
            _equipmentInRoomService = equipmentInRoomService;
        }

        [HttpPost]
        public ActionResult AddEquipmentInRoom([FromBody] String JSONString)
        {
            String JSONContent = StringToJSONFormat(JSONString);

            EquipmentInRooms equipmentInRooms = JsonConvert.DeserializeObject<EquipmentInRooms>(JSONContent);           
            _equipmentInRoomService.AddEquipmentInRoom(equipmentInRooms);
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
