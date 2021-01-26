﻿using Backend.Model.Manager;
using System;
using System.Collections.Generic;

namespace Backend.Service.RoomAndEquipment
{
    public interface IEquipmentTransferService
    {
        void AddEquipmentTransfer(EquipmentTransfer equipmentTransfer);
        void DeleteEquipmentType(int id);
        void DeleteEquipmentTransfer(int id);
        EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber, DateTime dateOfTransfer);
        ICollection<EquipmentTransfer> GetEquipmentTransfersByRoomNumberAndPeriod(DateTime start, DateTime end, int roomNumber);
    }
}
