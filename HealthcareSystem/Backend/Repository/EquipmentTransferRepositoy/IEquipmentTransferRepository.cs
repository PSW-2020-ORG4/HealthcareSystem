using Backend.Model.Manager;
using System;
using System.Collections.Generic;

namespace Backend.Repository.EquipmentTransferRepository
{
    public interface IEquipmentTransferRepository
    {
        void AddEquipmentTransfer(EquipmentTransfer equpmentTransfer);
        EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber, DateTime dateOfTransfer);
        void DeleteEquipmentTransfer(int id);
        ICollection<EquipmentTransfer> GetFollowingEquipmentTransversByRoom(int roomId);
        ICollection<EquipmentTransfer> GetEquipmentTransfersByRoomNumberAndPeriod(DateTime start, DateTime end, int roomNumber);
    }
}
