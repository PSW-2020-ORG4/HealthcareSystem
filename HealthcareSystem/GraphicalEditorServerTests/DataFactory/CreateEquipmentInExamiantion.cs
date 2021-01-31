using Backend.Model.PerformingExamination;
using System.Collections.Generic;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipmentInExamiantion
    {
        public List<EquipmentInExamination> CreateValidTestObjectForInitializingEquipmentTransfer()
        {
            return new List<EquipmentInExamination>() { new EquipmentInExamination(2, 5) };
        }
    }
}
