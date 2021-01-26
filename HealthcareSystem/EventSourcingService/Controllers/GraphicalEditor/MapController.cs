using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Service.GraphicalEditor;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingService.Controllers.GraphicalEditor
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IRoomSelectionService _roomSelectionService;
        private readonly IBuildingSelectionService _buildingSelectionService;
        private readonly IFloorChangeService _floorChangeService;

        public MapController(
            IRoomSelectionService roomSelectionService,
            IBuildingSelectionService buildingSelectionService,
            IFloorChangeService floorChangeService)
        {
            _roomSelectionService = roomSelectionService;
            _buildingSelectionService = buildingSelectionService;
            _floorChangeService = floorChangeService;
        }

        [HttpPost("roomSelection")]
        public ActionResult AddRoomSelectionEvent(RoomSelectionEvent roomSelectionEvent)
        {
            _roomSelectionService.Add(roomSelectionEvent);
            return NoContent();
        }

        [HttpPost("buildingSelection")]
        public ActionResult AddBuildingSelectionEvent(BuildingSelectionEvent buildingSelectionEvent)
        {
            _buildingSelectionService.Add(buildingSelectionEvent);
            return NoContent();
        }

        [HttpPost("floorChange")]
        public ActionResult AddFloorChangeEvent(FloorChangeEvent floorChangeEvent)
        {
            _floorChangeService.Add(floorChangeEvent);
            return NoContent();
        }
    }
}