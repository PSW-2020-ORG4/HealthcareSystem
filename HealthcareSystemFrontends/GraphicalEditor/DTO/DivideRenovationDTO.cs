﻿using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
   public class DivideRenovationDTO
    {
        public BaseRenovationDTO baseRenovation { get; set; }
        public string FirstRoomDescription { get; set; }
        public string SecondRoomDescription { get; set; }
        public TypeOfMapObject FirstRoomType { get; set; }
        public TypeOfMapObject SecondRoomType { get; set; }

        public DivideRenovationDTO()
        {
        }

        public DivideRenovationDTO(BaseRenovationDTO baseRenovation, string firstRoomDescription, string secondRoomDescription, TypeOfMapObject firstRoomType, TypeOfMapObject secondRoomType)
        {
            this.baseRenovation = baseRenovation;
            FirstRoomDescription = firstRoomDescription;
            SecondRoomDescription = secondRoomDescription;
            FirstRoomType = firstRoomType;
            SecondRoomType = secondRoomType;
        }
    }
}
