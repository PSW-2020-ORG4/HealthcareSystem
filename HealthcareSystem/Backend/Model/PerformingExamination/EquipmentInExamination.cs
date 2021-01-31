namespace Backend.Model.PerformingExamination
{
    public class EquipmentInExamination
    {
        public int EquipmentTypeID { get; set; }
        public int ExaminationId { get; set; }

        public EquipmentInExamination()
        {
        }

        public EquipmentInExamination(int equipmentTypeID, int examinationId)
        {
            this.EquipmentTypeID = equipmentTypeID;
            this.ExaminationId = examinationId;
        }
    }
}
