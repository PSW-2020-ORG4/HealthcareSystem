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
using Backend.Model.Manager;
using GraphicalEditorServer.Mappers;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IEquipmentInRoomsService _equipmentInRoomsService;


        public EquipmentController(
            IEquipmentService equipmentService, 
            IEquipmentTypeService equipmentTypeService,
            IEquipmentInRoomsService equipmentInRoomsService)
        {
            _equipmentService = equipmentService;
            _equipmentTypeService = equipmentTypeService;
            _equipmentInRoomsService = equipmentInRoomsService;
        }

        [HttpPost]
        public ActionResult AddEquipment([FromBody] Equipment equipment)
        {
            Equipment addedEquipment =_equipmentService.AddEquipment(equipment);
            return Ok(addedEquipment.Id);
        }

        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetEquipmentByRoomNumber(int roomNumber)
        {
            try
            {
                List<Equipment> equipmentsInRoom = _equipmentService.GetEquipmentByRoomNumber(roomNumber);
                List<EquipmentDTO> equipmentsInRoomDTOs = new List<EquipmentDTO>();
                if (equipmentsInRoom.Count==0) {
                    return NotFound("NotFound");
                }
                equipmentsInRoom.ForEach(equipment => equipmentsInRoomDTOs.Add(EquipmentMapper.EquipmentToEquipmentDTO(equipment)));
                return Ok(equipmentsInRoomDTOs);
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        [HttpGet("search")]
        public ActionResult GetEquipmentWithRoomForSearchTerm(string term = "")
        {
       
            List<Equipment> equipment = _equipmentService.GetEquipmentWithRoomForSearchTerm(term);

            List<EquipmentWithRoomDTO> allEquipmentWithRoomDTOs = new List<EquipmentWithRoomDTO>();
            foreach(Equipment e in equipment)
            {
                List<EquipmentWithRoomDTO> equipmentInRoomDTO = EquipmentWithRoomMapper.EquipmentToEquipmentWithRoomDTO(e, _equipmentInRoomsService.GetEquipmentInRoomsFromEquipment(e));

                allEquipmentWithRoomDTOs.AddRange(equipmentInRoomDTO);
            }

            return Ok(allEquipmentWithRoomDTOs);
        }

    }

}
