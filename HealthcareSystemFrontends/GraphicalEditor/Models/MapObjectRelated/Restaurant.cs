﻿using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphicalEditor.Models.MapObjectRelated
{
    class Restaurant : MapObjectEntity
    {
        public Restaurant(String description = "")
          : base(TypeOfMapObject.RESTAURANT, description)
        {
            FormatObjectDescription(Description);
        }

        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName;
            }
        }
    }
}
