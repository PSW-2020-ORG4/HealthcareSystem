using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO.EventSourcingDTO
{
    public class BuildingSelectionEventDTO
    {
        public String Username { get; set; }
        public int BuildingNumber { get; set; }


        public BuildingSelectionEventDTO() {}

        public BuildingSelectionEventDTO(string username, int buildingNumber)
        {
            Username = username;
            BuildingNumber = buildingNumber;
        }
    }
}
