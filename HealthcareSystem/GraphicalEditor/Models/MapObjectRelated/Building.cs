﻿using GraphicalEditor.Enumerations;
using System;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Building : MapObjectEntity
    {
        public int NumOfFloors { get; set; }

        public Building(String description, int numOfFloors)
           : base(TypeOfMapObject.BUILDING, description)
        {
            NumOfFloors = numOfFloors;

            FormatObjectDescription(Description);
        }

        public Building(int numOfFloors)
           : base(TypeOfMapObject.BUILDING, "")
        {
            NumOfFloors = numOfFloors;

            FormatObjectDescription(Description);
        }

        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName + " " + Id + " ima " + NumOfFloors + " spratova.";
            }
        }
    }
}
