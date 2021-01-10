using Backend.Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
   public class CreateEquipmentInExamiantion
    {
        public List<EquipmentInExamination> CreateValidTestObjectForInitializingEquipmentTransfer()
        {
            return new List<EquipmentInExamination>() { new EquipmentInExamination(2, 5)};
        }
    }
}
