using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository
{
    public class MySqlEquipmentInRoomsRepository : IEquipmentInRoomsRepository
    {
        private readonly MyDbContext _context;

        public MySqlEquipmentInRoomsRepository(MyDbContext context)
        {
            _context = context;
        }

        public void DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public List<EquipmentInRooms> GetEquipmentInRoomsByRoomNumber(int roomNumber)
        {
            var m = _context.EquipmentsInRooms.Where(x => x.RoomNumber == roomNumber).ToList();
            return (List<EquipmentInRooms>)m;
        }

        public EquipmentInRooms AddEquipment(EquipmentInRooms equipment)
        {
            _context.EquipmentsInRooms.Add(equipment);
            _context.SaveChanges();
            return equipment;
        }

        public EquipmentInRooms UpdateEquipment(EquipmentInRooms equipment)
        {
            throw new NotImplementedException();
        }

        public List<EquipmentInRooms> GetEquipmenInRoomsByEquipmentId(int idEquipment)
        {
            return _context.EquipmentsInRooms.Where(x => x.IdEquipment == idEquipment).ToList();
        }

    }
}
