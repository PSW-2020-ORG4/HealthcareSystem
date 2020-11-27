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
        public ActionResult AddEquipmentInRoom([FromBody] String test)
        {
            String final = "{";
            final += test;
            final += "}";

            EquipmentInRooms equipmentInRooms = JsonConvert.DeserializeObject<EquipmentInRooms>(final);           
            _equipmentInRoomService.addEquipmentInRoom(equipmentInRooms);
            return Ok();

        }

        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetEquipmentByRoomNumber(int roomNumber)
        {
            try
            {
                List<Equipment> equipmentsInRoom = _equipmentInRoomService.getEquipmentByRoomNumber(roomNumber);
                return Ok(equipmentsInRoom);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
