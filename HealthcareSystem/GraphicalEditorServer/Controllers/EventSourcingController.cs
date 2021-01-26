using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service;
using Backend.Service.Encryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using GraphicalEditorServer.Settings;
using Microsoft.Extensions.Options;
using GraphicalEditorServer.Controllers.Adapter;
using GraphicalEditorServer.DTO.EventSourcingDTO;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSourcingController : ControllerBase
    {

        private readonly ServiceSettings _serviceSettings;

        public EventSourcingController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [HttpPost("roomSelection")]
        public ActionResult AddRoomSelectionEvent(RoomSelectionEventDTO roomSelectionEventDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.EventSourcingServiceUrl, "/api/map/roomSelection", roomSelectionEventDTO);
        }


        [HttpPost("buildingSelection")]
        public ActionResult AddBuildingSelectionEvent(BuildingSelectionEventDTO buildingSelectionEventDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.EventSourcingServiceUrl, "/api/map/buildingSelection", buildingSelectionEventDTO);
        }


        [HttpPost("floorChange")]
        public ActionResult AddFloorChangeEvent(FloorChangeEventDTO floorChangeEventDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.EventSourcingServiceUrl, "/api/map/floorChange", floorChangeEventDTO);
        }
    }
}
