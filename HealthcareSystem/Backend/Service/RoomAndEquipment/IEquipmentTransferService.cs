using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.RoomAndEquipment
{
   public interface IEquipmentTransferService
    {
        void AddEquipmentTransfer(EquipmentTransfer equipmentTransfer);
        void DeleteEquipmentType(int id);
        void DeleteEquipmentTransfer(int id);
        EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber, DateTime dateOfTransfer);
        EquipmentTransfer GetEquipmentTransferById(int id);
        ICollection<EquipmentTransfer> GetEquipmentTransfersByRoomNumberAndPeriod(DateTime start, DateTime end, int roomNumber);
    }
}
