using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class PlacementDTO
    {
        public string patientCard { get; set; }
        public int room { get; set; }
        public int Id { get; set; }
        public string DateOfPlacement { get; set; }
        public string DateOfDismison { get; set; }

        public PlacementDTO() { }

        public PlacementDTO(PlacementDTO p)
        {
            this.patientCard = p.patientCard;
            this.room = p.room;
            this.Id = p.Id;
            this.DateOfPlacement = p.DateOfPlacement;
            this.DateOfDismison = p.DateOfDismison;
        }
    }
}
