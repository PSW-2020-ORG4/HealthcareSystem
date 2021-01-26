using GraphicalEditorServer.Controllers.Adapter;
using GraphicalEditorServer.DTO.EventSourcingDTO;
using GraphicalEditorServer.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
