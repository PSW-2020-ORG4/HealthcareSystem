using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.RoomAndEquipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Manager;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumableEquipmentController : ControllerBase
    {
        private readonly IConsumableEquipmentService _consumableEquipmentService;

        public ConsumableEquipmentController(IConsumableEquipmentService consumableEquipmentService)
        {
            _consumableEquipmentService = consumableEquipmentService;
        }
        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetConsumableEquipmentByRoomNumber(int roomNumber)
        {
            try
            {
                List<ConsumableEquipment> equipmentsInRoom = _consumableEquipmentService.GetConsumableEquipmentByRoomNumber(roomNumber);
                return Ok(equipmentsInRoom);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
