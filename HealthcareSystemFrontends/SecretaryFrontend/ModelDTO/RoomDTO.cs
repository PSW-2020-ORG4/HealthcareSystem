using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZdravoKorporacija.ModelDTO
{
    class RoomDTO
    {
        public string NumberOfRoom { get; set; }

        public string Purpose { get; set; }

        public string StartRenovationDate { get; set; }

        public string EndRenovationDate { get; set; }

        public bool isRenovate { get; set; }
    }
}
