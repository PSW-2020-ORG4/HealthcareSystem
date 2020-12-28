using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class EquipmentInExaminationDTO
    {
        public int EquipmentID { get; set; }
        public int ExaminationId { get; set; }

        public EquipmentInExaminationDTO()
        {
        }

        public EquipmentInExaminationDTO(int equipmentID, int examinationId)
        {
            this.EquipmentID = equipmentID;
            this.ExaminationId = examinationId;
        }
    }
}
