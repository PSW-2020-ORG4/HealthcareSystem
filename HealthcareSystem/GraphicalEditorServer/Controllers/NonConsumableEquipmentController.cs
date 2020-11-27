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
    public class NonConsumableEquipmentController : ControllerBase
    {

        private readonly INonConsumableEquipmentService _nonConsumableEquipmentService;

        public NonConsumableEquipmentController(INonConsumableEquipmentService nonConsumableEquipmentService)
        {
            _nonConsumableEquipmentService = nonConsumableEquipmentService;
        }
        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetNonConsumableEquipmentByRoomNumber(int roomNumber)
        {            
            try
            {
                List<NonConsumableEquipment> equipmentsInRoom = _nonConsumableEquipmentService.GetNonConsumableEquipmentByRoomNumber(roomNumber);
                return Ok(equipmentsInRoom);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
