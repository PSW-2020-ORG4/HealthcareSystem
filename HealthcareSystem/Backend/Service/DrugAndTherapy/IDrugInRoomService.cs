using Backend.Model.Manager;
using Model.Manager;
using System.Collections.Generic;

namespace Backend.Service.DrugAndTherapy
{
    public interface IDrugInRoomService
    {
        DrugInRoom AddDrugInRoom(DrugInRoom drugInRoom);
        DrugInRoom EditDrugInRoom(DrugInRoom drugInRoom);
        List<Drug> GetDrugsByRoomNumber(int roomNumber);
        void DeleteDrugInRoom(int drugId);
        List<DrugInRoom> GetDrugsInRoomsFromDrug(Drug drug);
    }
}
