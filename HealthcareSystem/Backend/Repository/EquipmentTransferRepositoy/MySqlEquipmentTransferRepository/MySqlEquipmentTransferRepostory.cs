using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.EquipmentTransferRepository.MySqlTransferEquipmentRepository
{
    public class MySqlEquipmentTransferRepostory : IEquipmentTransferRepository
    {
        private readonly MyDbContext _context;

        public MySqlEquipmentTransferRepostory(MyDbContext context)
        {
            _context = context;
        }

        public void AddEquipmentTransfer(EquipmentTransfer equpmentTransfer)
        {
            _context.EqupmentTransfer.Add(equpmentTransfer);
            _context.SaveChanges();

        }

        public void DeleteEquipmentTransfer(int id)
        {
            EquipmentTransfer equipmentTransfer = _context.EqupmentTransfer.SingleOrDefault(d => d.Id == id);
            if (equipmentTransfer != null)
            {
                _context.Remove(equipmentTransfer);
                _context.SaveChanges();
            }

        }

        public EquipmentTransfer GetEquipmentTransferById(int id)
        {
            EquipmentTransfer equipmentTransfer;
            try
            {
                equipmentTransfer = _context.EqupmentTransfer.Find(id);
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
            if (equipmentTransfer == null)
                throw new NotFoundException("EquipmentTransfer doesn't exist.");

            return equipmentTransfer;
        }

        public EquipmentTransfer GetEquipmentTransferByRoomNumberAndDate(int roomNumber, DateTime dateOfTransfer)
        {
            return _context.EqupmentTransfer.SingleOrDefault(x => x.RoomNumber == roomNumber && x.DateAndTimeOfTransfer == dateOfTransfer);

        }

        public ICollection<EquipmentTransfer> GetEquipmentTransfersByRoomNumberAndPeriod(DateTime start, DateTime end, int roomNumber)
        {
            try
            {
                return _context.EqupmentTransfer.Where(e => e.RoomNumber == roomNumber && e.DateAndTimeOfTransfer > start && e.DateAndTimeOfTransfer < end).ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }

        public ICollection<EquipmentTransfer> GetFollowingEquipmentTransversByRoom(int roomId)
        {
            try
            {
                return _context.EqupmentTransfer.Where(e => e.RoomNumber == roomId).ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }
    }
}
