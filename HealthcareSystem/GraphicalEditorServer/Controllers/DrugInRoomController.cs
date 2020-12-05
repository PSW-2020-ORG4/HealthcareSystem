using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Manager;
using Backend.Service.DrugAndTherapy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugInRoomController : ControllerBase
    {
        private readonly IDrugInRoomService _drugInRoomService;

        public DrugInRoomController(IDrugInRoomService drugInRoomService)
        {
            _drugInRoomService = drugInRoomService;
        }
        [HttpPost]
        public ActionResult AddDrugInRoom([FromBody] String JSONString)
        {
            Console.WriteLine("usao u kontroler");
            String JSONContent = StringToJSONFormat(JSONString);

            DrugInRoom drugInRoom = JsonConvert.DeserializeObject<DrugInRoom>(JSONContent);
            _drugInRoomService.AddDrugInRoom(drugInRoom);
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
    }
}
