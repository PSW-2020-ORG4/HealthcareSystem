using Backend.Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend.Model.Manager
{
    public class DivideRenovation : BaseRenovation
    {
        public string FirstRoomDescription { get; set; }
        public string SecondRoomDescription { get; set; }
        public TypeOfUsage FirstRoomType { get; set; }
        public TypeOfUsage SecondRoomType { get; set; }

        public DivideRenovation() : base()
        {
        }

        public DivideRenovation(RenovationPeriod renovationPeriod, string description,TypeOfRenovation typeOfRenovation,
            int divideRoomId, string firstRoomDescription, string secondRoomDescription, TypeOfUsage firstRoomType, TypeOfUsage secondRoomType)
            :base(divideRoomId,renovationPeriod,description,typeOfRenovation)
        {
            FirstRoomDescription = firstRoomDescription;
            SecondRoomDescription = secondRoomDescription;
            FirstRoomType = firstRoomType;
            SecondRoomType = secondRoomType;
        }
    }
}
