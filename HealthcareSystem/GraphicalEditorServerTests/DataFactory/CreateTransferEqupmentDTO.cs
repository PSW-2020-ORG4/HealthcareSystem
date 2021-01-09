
using Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
   public class CreateTransferEqupmentDTO
    {
        public TransferEquipmentDTO CreateInvalidTestObjectForInitializingEquipmentTransfer1()
        {
            return new TransferEquipmentDTO(6, 9, 13, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc), 28);
        }

        public TransferEquipmentDTO CreateInvalidTestObjectForInitializingEquipmentTransfer2()
        {
            return new TransferEquipmentDTO(2, 5, 13, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc), 15);
        }
        public TransferEquipmentDTO CreateInalidTestObjectForInitializingEquipmentTransfer3()
        {
            return new TransferEquipmentDTO(2, 20, 13, new DateTime(2020, 12, 15, 8, 0, 0, DateTimeKind.Utc), 10);
        }
        public TransferEquipmentDTO CreateValidTestObjectForInitializingEquipmentTransfer()
        {
            return new TransferEquipmentDTO(2, 11, 13, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc), 10);
        }
       
    }
}
