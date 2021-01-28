using Backend.Model.Manager;
using Model.Manager;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateEquipment
    {
        public Equipment CreateInvalidTestObject()
        {
            return new Equipment(-1, null);
        }
        public Equipment CreateValidTestObject(int equipmentQuantity)
        {
            return new Equipment(equipmentQuantity, new CreateEquipmentType().CreateValidTestObject());
        }
        public Equipment CreateValidTestObject()
        {
            return new Equipment(30, new CreateEquipmentType().CreateValidTestObject());
        }

        public Equipment CreateBedTestObject()
        {
            return new Equipment(30, new EquipmentType(0, "bed", false));
        }

        public Equipment CreateMaskTestObject()
        {
            return new Equipment(30, new EquipmentType(1, "mask", true));
        }

        public Equipment CreateComputerTestObject()
        {
            return new Equipment(30, new EquipmentType(2, "computer", false));
        }

        public Equipment CreateNeedleTestObject()
        {
            return new Equipment(30, new EquipmentType(3, "needle", true));
        }

        public Equipment CreateBendTestObject()
        {
            return new Equipment(30, new EquipmentType(4, "bend", true));
        }


    }
}
