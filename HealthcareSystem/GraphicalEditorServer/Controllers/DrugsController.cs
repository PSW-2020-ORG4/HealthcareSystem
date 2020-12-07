using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Model.Manager;
using Backend.Service.DrugAndTherapy;
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

        public DrugsController(IDrugService drugService, IDrugTypeService drugTypeService)
        {
            _drugService = drugService;
            _drugTypeService = drugTypeService;
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
                return Ok(drugsInRoom);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("drugsInRoomByDrugId/{drugId}")]
        public IActionResult GetDrugsInRoomByDrugId(int drugId)
        {
            try
            {
                DrugInRoom drugInRoom = _drugService.GetDrugInRoomByDrugId(drugId);
                return Ok(drugInRoom);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }


        [HttpGet]
        public IActionResult GetAllDrugs()
        {
            try
            {
                List<Drug> drugs = _drugService.ViewConfirmedDrugs();
                return Ok(drugs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
