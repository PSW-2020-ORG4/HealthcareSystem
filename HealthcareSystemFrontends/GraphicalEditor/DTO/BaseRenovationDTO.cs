using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class BaseRenovationDTO
    {
       
        public int RoomId { get; set; }
        public RenovationPeriodDTO RenovationPeriod { get; set; }
        public string Description { get; set; }
        public TypeOfRenovation TypeOfRenovation { get; set; }

        public BaseRenovationDTO()
        {
        }

        public BaseRenovationDTO(int roomId, RenovationPeriodDTO renovationPeriod, string description, TypeOfRenovation typeOfRenovation)
        {
            RoomId = roomId;
            RenovationPeriod = renovationPeriod;
            Description = description;
            TypeOfRenovation = typeOfRenovation;
        }
    }
}
