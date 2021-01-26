using Backend.Model.Manager;
using System.Collections.Generic;

namespace Backend.Repository.DrugInRoomRepository
{
    public interface IDrugInRoomRepository
    {
        List<DrugInRoom> GetDrugsInRoomByRoomNumber(int roomNumber);
        DrugInRoom SetDrug(DrugInRoom drugInRoom);
        void DeleteDrug(int id);
        DrugInRoom AddDrug(DrugInRoom drugInRoom);
        List<DrugInRoom> GetDrugInRoomByDrugId(int drugId);

    }
}
