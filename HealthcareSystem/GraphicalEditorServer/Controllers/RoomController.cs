using System;
using Backend.Model.Enums;
using System.Collections.Generic;
using Backend.Model.Exceptions;
using Backend.Service.RoomAndEquipment;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.RenovationService;
using Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Model.Manager;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IExaminationService _examinationService;
        private readonly IEquipmentTransferService _equipmentTransferService;
        private readonly IRenovationService _renovationService;

        public RoomController(
            IRoomService roomService, 
            IExaminationService examinationService,
            IEquipmentTransferService equipmentTransferService,
            IRenovationService renovationService) 
        {
            _roomService = roomService;
            _examinationService = examinationService;
            _equipmentTransferService = equipmentTransferService;
            _renovationService = renovationService;
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

        [HttpGet("roomSchedule/{roomId}")]
        public ActionResult GetRoomSchedules(int roomId)
        {
            try
            {
                List<Examination> allExamination = (List<Examination>) _examinationService.GetExaminationsForPeriodAndRoom(DateTime.Now, DateTime.Now.AddDays(30), roomId);
                List<EquipmentTransfer> allEquipmentTransfers = (List<EquipmentTransfer>) _equipmentTransferService.GetEquipmentTransfersByRoomNumberAndPeriod(DateTime.Now, DateTime.Now.AddDays(30), roomId);
                List<BaseRenovation> allRenovation = (List<BaseRenovation>) _renovationService.GetRenovationForPeriodByRoomNumber(DateTime.Now, DateTime.Now.AddDays(30), roomId);

                List<RoomSchedulesDTO> roomSchedulesDTOs = new List<RoomSchedulesDTO>();
                allExamination.ForEach(e => roomSchedulesDTOs.Add(RoomSchedulesMapper.Examination_To_RoomSchedulesDTO(e)));
                allEquipmentTransfers.ForEach(e => roomSchedulesDTOs.Add(RoomSchedulesMapper.EquipmentTransfer_To_RoomSchedulesDTO(e)));
                allRenovation.ForEach(e => roomSchedulesDTOs.Add(RoomSchedulesMapper.Renovation_To_RoomSchedulesDTO(e)));

                return Ok(roomSchedulesDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
