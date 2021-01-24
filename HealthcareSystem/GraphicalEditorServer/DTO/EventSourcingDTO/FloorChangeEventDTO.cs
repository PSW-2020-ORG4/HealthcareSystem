﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO.EventSourcingDTO
{
    public class FloorChangeEventDTO
    {
        public String Username { get; set; }
        public int BuildingNumber { get; set; }
        public int Floor { get; set; }

        public FloorChangeEventDTO() {}

        public FloorChangeEventDTO(string username, int buildingNumber, int floor)
        {
            Username = username;
            BuildingNumber = buildingNumber;
            Floor = floor;
        }
    }
}
