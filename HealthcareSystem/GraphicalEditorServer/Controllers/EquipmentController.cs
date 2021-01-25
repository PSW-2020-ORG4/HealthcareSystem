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
using Backend.Model.DTO;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly IEquipmentInRoomsService _equipmentInRoomsService;
        private readonly IEquipmentTransferService _equipmentTransferService;

        public EquipmentController(
            IEquipmentService equipmentService, 
            IEquipmentTypeService equipmentTypeService,
            IEquipmentInRoomsService equipmentInRoomsService,
            IEquipmentTransferService equipmentTransferService)
        {
            _equipmentService = equipmentService;
            _equipmentTypeService = equipmentTypeService;
            _equipmentInRoomsService = equipmentInRoomsService;
            _equipmentTransferService = equipmentTransferService;
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

        [HttpPost("initilizeTransfer")]
        public ActionResult InitializeEquipmentTransfer(TransferEquipmentDTO transferEquipmentDTO)
        {
            return Ok(_equipmentService.InitializeEquipmentTransfer(transferEquipmentDTO));
        }

        [HttpPost("getAlternativeAppointments")]
        public ActionResult GetAlternativeAppointments(TransferEquipmentDTO transferEquipmentDTO)
        {
            return Ok(_equipmentService.GetAlternativeAppointments(transferEquipmentDTO));
        }

        [HttpPost("scheduleTransfer")]
        public ActionResult ScheduleEquipmentTransfer(TransferEquipmentDTO transferEquipmentDTO)
        {
            _equipmentService.ScheduleEquipmentTrasfer(transferEquipmentDTO);
            return Ok();
        }

        [HttpDelete("deleteById/{id}")]
        public ActionResult DeleteEquipmentTransfer(int id)
        {
            _equipmentTransferService.DeleteEquipmentTransfer(id);            
            return Ok();
        }

    }

}
