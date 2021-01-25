using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.RoomAndEquipment
{
   public interface IEquipmentTransferService
    {
        void AddEquipmentTransfer(EquipmentTransfer equipmentTransfer);     
        EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber, DateTime dateOfTransfer);
        ICollection<EquipmentTransfer> GetEquipmentTransfersByRoomNumberAndPeriod(DateTime start, DateTime end, int roomNumber);
    }
}
