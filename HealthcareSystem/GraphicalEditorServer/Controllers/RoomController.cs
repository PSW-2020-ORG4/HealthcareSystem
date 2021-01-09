using System;
using Backend.Model.Enums;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServer.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService) 
        {
            _roomService = roomService;
        }

        [HttpPost]
        public ActionResult AddRoom([FromBody] String JSONString)
        {
            String JSONContent = StringToJSONFormat(JSONString);

            RoomDTO room = JsonConvert.DeserializeObject<RoomDTO>(JSONContent);
            Model.Manager.Room newRoom = new Model.Manager.Room((int)room.Id, (TypeOfUsage)room.Usage, 0, 0, false);
            _roomService.AddRoom(newRoom);
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
