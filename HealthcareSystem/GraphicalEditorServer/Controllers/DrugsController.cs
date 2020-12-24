using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Model.Manager;
using Backend.Service.DrugAndTherapy;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Manager;
using Newtonsoft.Json;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugService _drugService;
        private readonly IDrugTypeService _drugTypeService;
        private readonly IDrugInRoomService _drugInRoomService;

        public DrugsController(IDrugService drugService, IDrugTypeService drugTypeService, IDrugInRoomService drugInRoomService)
        {
            _drugService = drugService;
            _drugTypeService = drugTypeService;
            _drugInRoomService = drugInRoomService;
        }

        [HttpPost]
        public ActionResult AddDrugs([FromBody] Drug drug)
        {
            _drugService.AddConfirmedDrug(drug);
            return Ok(drug.Id);
        }

        [HttpGet("byRoomNumber/{roomNumber}")]
        public IActionResult GetDrugsByRoomNumber(int roomNumber)
        {
            try
            {
                List<Drug> drugsInRoom = _drugService.GetDrugsByRoomNumber(roomNumber);
                List<DrugDTO> drugsInRoomDTO = new List<DrugDTO>();
                foreach (var drugInRoom in drugsInRoom) {
                    drugsInRoomDTO.Add(DrugMapper.DrugToDrugDTO(drugInRoom));
                }
                return Ok(drugsInRoomDTO);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("search")]
        public ActionResult GetDrugWithRoomForSearchTerm(string term = "")
        {

            List<Drug> drugs = _drugService.GetDrugWithRoomForSearchTerm(term);

            List<DrugWithRoomDTO> allDrugWithRoomDTOs = new List<DrugWithRoomDTO>();
            foreach (Drug d in drugs)
            {
                List<DrugWithRoomDTO> drugInRoomDTO = DrugWithRoomMapper.DrugToDrugWithRoomDTO(d, _drugInRoomService.GetDrugsInRoomsFromDrug(d));
                allDrugWithRoomDTOs.AddRange(drugInRoomDTO);
            }

            return Ok(allDrugWithRoomDTOs);
        }


        [HttpGet]
        public IActionResult GetAllDrugs()
        {
            try
            {
                List<Drug> drugs = _drugService.ViewConfirmedDrugs();
                List<DrugDTO> drugsDTO = new List<DrugDTO>();
                foreach (var drug in drugs) {
                    drugsDTO.Add(DrugMapper.DrugToDrugDTO(drug));
                }
                return Ok(drugsDTO);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
