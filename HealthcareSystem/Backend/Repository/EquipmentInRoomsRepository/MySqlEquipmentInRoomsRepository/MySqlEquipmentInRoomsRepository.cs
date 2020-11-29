using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository
{
    public class MySqlEquipmentInRoomsRepository : IEquipmentInRoomsRepository
    {
        private readonly MyDbContext _context;

        public MySqlEquipmentInRoomsRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public int GetEquipment(int idEquipment)
        {
            throw new NotImplementedException();
        }

        public List<EquipmentInRooms> GetEquipmentByRoom(int roomNumber)
        {
            var m =  _context.EquipmentsInRooms.Where(x => x.RoomNumber == roomNumber).ToList();
            return (List<EquipmentInRooms>)m;
        }
        


        public EquipmentInRooms NewEquipment(EquipmentInRooms equipment)
        {
            _context.EquipmentsInRooms.Add(equipment);
            _context.SaveChanges();
            return equipment;
        }

        public EquipmentInRooms SetEquipment(EquipmentInRooms equipment)
        {
            throw new NotImplementedException();
        }

        EquipmentInRooms IEquipmentInRoomsRepository.GetEquipment(int idEquipment)
        {
            return _context.EquipmentsInRooms.Single(x => x.IdEquipment == idEquipment);
        }
    }
}
