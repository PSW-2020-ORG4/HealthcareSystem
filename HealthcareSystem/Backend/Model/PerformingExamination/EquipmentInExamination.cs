using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Model.PerformingExamination
{
    public class EquipmentInExamination
    {
        public int EquipmentID { get; set; }
        public int ExaminationId { get; set; }

        public EquipmentInExamination()
        {
        }

        public EquipmentInExamination(int equipmentID, int examinationId)
        {
            this.EquipmentID = equipmentID;
            this.ExaminationId = examinationId;
        }
    }
}
