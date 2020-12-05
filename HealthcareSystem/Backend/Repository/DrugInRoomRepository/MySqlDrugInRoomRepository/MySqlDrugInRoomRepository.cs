using Backend.Model;
using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository
{
   public class MySqlDrugInRoomRepository : IDrugInRoomRepository
    {
        private readonly MyDbContext _context;

        public MySqlDrugInRoomRepository(MyDbContext context)
        {
            _context = context;
        }

        public void DeleteDrug(int id)
        {
            throw new NotImplementedException();
        }

        public List<DrugInRoom> GetDrugsInRoomByRoomNumber(int roomNumber)
        {
            var m = _context.DrugsInRooms.Where(x => x.RoomNumber == roomNumber).ToList();
            return (List<DrugInRoom>)m;
        }

        public DrugInRoom AddDrug(DrugInRoom drugInRoom)
        {          
            _context.DrugsInRooms.Add(drugInRoom);
            _context.SaveChanges();
            return drugInRoom;
        }

        public DrugInRoom SetDrug(DrugInRoom drugInRoom)
        {
            throw new NotImplementedException();
        }

        public DrugInRoom GetDrugInRoomByDrugId(int drugId)
        {
            return (DrugInRoom)_context.DrugsInRooms.Where(x => x.DrugId == drugId).ToList()[0];
        }

    }
}
