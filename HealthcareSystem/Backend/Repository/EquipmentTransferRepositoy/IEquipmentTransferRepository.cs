using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Manager;

namespace Backend.Repository.EquipmentTransferRepository
{
   public interface IEquipmentTransferRepository
    {
        void AddEquipmentTransfer(EquipmentTransfer equpmentTransfer);
        EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber,DateTime dateOfTransfer);
        void DeleteEquipmentTransfer(int id);       
        ICollection<EquipmentTransfer> GetFollowingEquipmentTransversByRoom(int roomId);
    }
}
