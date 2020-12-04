using Backend.Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugInRoomRepository
{
   public interface IDrugInRoomRepository
    {
        List<DrugInRoom> GetDrugsInRoomByRoomNumber(int roomNumber);
        DrugInRoom SetDrug(DrugInRoom drugInRoom);
        bool DeleteDrug(int id);
        DrugInRoom AddDrug(DrugInRoom drugInRoom);
        DrugInRoom GetDrugInRoomByDrugId(int drugId);
    }
}
