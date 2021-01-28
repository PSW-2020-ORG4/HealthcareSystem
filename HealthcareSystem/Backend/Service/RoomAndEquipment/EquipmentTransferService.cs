using Backend.Model.Manager;
using Backend.Repository.EquipmentTransferRepository;
using System;
using System.Collections.Generic;

namespace Backend.Service.RoomAndEquipment
{
    public class EquipmentTransferService : IEquipmentTransferService
    {
        private IEquipmentTransferRepository _equipmentTransferRepository;
        public EquipmentTransferService(IEquipmentTransferRepository equipmentTransferRepository)
        {
            _equipmentTransferRepository = equipmentTransferRepository;

        }

        public void DeleteEquipmentType(int id)
        {
            _equipmentTransferRepository.DeleteEquipmentTransfer(id);
        }
        public void AddEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            _equipmentTransferRepository.AddEquipmentTransfer(equipmentTransfer);
        }

        public EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber, DateTime dateOfTransfer)
        {
            return _equipmentTransferRepository.GetEquipmentTransferByRoomNumberAndDate(roomNumber, dateOfTransfer);
        }

        void IEquipmentTransferService.DeleteEquipmentTransfer(int id)
        {
            _equipmentTransferRepository.DeleteEquipmentTransfer(id);
        }
        public ICollection<EquipmentTransfer> GetEquipmentTransfersByRoomNumberAndPeriod(DateTime start, DateTime end, int roomNumber)
        {
            return _equipmentTransferRepository.GetEquipmentTransfersByRoomNumberAndPeriod(start, end, roomNumber);
        }
    }
}
