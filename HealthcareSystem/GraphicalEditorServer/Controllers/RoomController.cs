using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.RoomAndEquipment;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Enums;
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
        public ActionResult AddRoom([FromBody] String test)
        {
            String final = "{";
            final += test;
            final += "}";
            RoomDTO room = JsonConvert.DeserializeObject<RoomDTO>(final);
            Model.Manager.Room newRoom = new Model.Manager.Room((int)room.Id, (TypeOfUsage)room.Usage, 0, 0, false);
            _roomService.AddRoom(newRoom);
            return Ok();

        }
    }
}
