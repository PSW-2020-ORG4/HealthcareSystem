using Backend.Model.Manager;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipmentType
    {
        public EquipmentType CreateValidTestObject()
        {
            return new EquipmentType("table", true);
        }
    }
}
