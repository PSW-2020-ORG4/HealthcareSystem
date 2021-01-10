using System;
using System.Collections.Generic;
using Backend.Model.Exceptions;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
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

        [HttpGet]
        public ActionResult GetAllRooms()
        {
            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            try
            {
                _roomService.ViewRooms().ForEach(room => roomDTOs.Add(RoomMapper.BackendRoomToGraphicalEditorRoom(room)));
                return Ok(roomDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
