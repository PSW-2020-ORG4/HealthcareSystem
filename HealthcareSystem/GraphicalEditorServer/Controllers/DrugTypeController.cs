using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.DrugAndTherapy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Manager;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugTypeController : ControllerBase
    {
        private readonly IDrugTypeService _drugTypeService;

        public DrugTypeController(IDrugTypeService drugTypeService)
        {
            _drugTypeService = drugTypeService;
        }

        [HttpPost]
        public ActionResult AddDrugtType([FromBody] DrugType drugType)
        {
            DrugType addedEquipmentType = _drugTypeService.AddDrugType(drugType);
            return Ok(addedEquipmentType.Id);
        }

        [HttpGet]
        public ActionResult GetAllDrugTypes()
        {
            List<DrugType> drugTypes = new List<DrugType>();
            try
            {
                drugTypes = _drugTypeService.GetDrugTypes();
                return Ok(drugTypes);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public DrugType GetDrugType(int id)
        {
            return _drugTypeService.GetDrugTypeById(id);
        }
    }
}
