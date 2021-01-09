using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicalEditor.DTO;

namespace GraphicalEditor.DTO
{
    class ExaminationForReschedulingDTO
    {
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public int RoomId { get; set; }


        public ExaminationForReschedulingDTO() {}

        public ExaminationForReschedulingDTO(DateTime dateTimeFrom, DateTime dateTimeTo, int roomId)
        {
            this.DateTimeFrom = dateTimeFrom;
            this.DateTimeTo = dateTimeTo;
            this.RoomId = roomId;
        }
    }
}
