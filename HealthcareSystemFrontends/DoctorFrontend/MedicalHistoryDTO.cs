using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class MedicalHistoryDTO
    {
        public string date { get; set; }

        public string disease { get; set; }

        public string therapy { get; set; }

        public MedicalHistoryDTO() { }

        public MedicalHistoryDTO(MedicalHistoryDTO mtDTO) {
            this.date = mtDTO.date;
            this.disease = mtDTO.disease;
            this.therapy = mtDTO.therapy;
        }
    }
}
