using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicalEditor.DTO.EventSourcingDTO;

namespace GraphicalEditor.Service
{
    class EventSourcingService : GenericHTTPService
    {
        public void AddRoomSelectionEvent(RoomSelectionEventDTO roomSelectionEventDTO)
        {  
            AddHTTPPostRequest("eventSourcing/roomSelection", roomSelectionEventDTO);
        }


        public void AddBuildingSelectionEvent(BuildingSelectionEventDTO buildingSelectionEventDTO)
        {
            AddHTTPPostRequest("eventSourcing/buildingSelection", buildingSelectionEventDTO);
        }


        public void AddFloorChangeEvent(FloorChangeEventDTO floorChangeEventDTO)
        {
            AddHTTPPostRequest("eventSourcing/floorChange", floorChangeEventDTO);
        }
    }
}
