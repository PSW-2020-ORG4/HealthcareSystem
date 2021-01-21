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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public TypeOfRenovation TypeOfRenovation { get; set; }

        public BaseRenovationDTO()
        {
        }

        public BaseRenovationDTO(int roomId, DateTime startTime, DateTime endTime, string description, TypeOfRenovation typeOfRenovation)
        {
            RoomId = roomId;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            TypeOfRenovation = typeOfRenovation;
        }
    }
}
