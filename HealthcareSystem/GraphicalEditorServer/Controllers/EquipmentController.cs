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
            Console.WriteLine("TUUU");
            Equipment addedEquipment =_equipmentService.AddEquipment(equipment);
            return Ok(addedEquipment.Id);
        }

        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetEquipmentByRoomNumber(int roomNumber)
        {
            List<Equipment> equipmentsInRoom = _equipmentService.GetEquipmentByRoomNumber(roomNumber);
            return Ok(equipmentsInRoom);
        }

        [HttpGet("search")]
        public ActionResult GetEquipmentWithRoomForSearchTerm(string term = "")
        {
            Console.WriteLine(term);
            List<Equipment> equipment = _equipmentService.GetEquipmentWithRoomForSearchTerm(term);

            List<EquipmentWithRoomDTO> equipmentWithRoomDTOs = new List<EquipmentWithRoomDTO>();
            foreach(Equipment e in equipment)
            {
                EquipmentWithRoomDTO equipmentInRoomDTO = EquipmentWithRoomMapper.EquipmentToEquipmentWithRoomDTO(e, _equipmentInRoomsService.GetEquipmentInRoomsFromEquipment(e));
                equipmentWithRoomDTOs.Add(equipmentInRoomDTO);
            }

            return Ok(equipmentWithRoomDTOs);
        }

    }

}
