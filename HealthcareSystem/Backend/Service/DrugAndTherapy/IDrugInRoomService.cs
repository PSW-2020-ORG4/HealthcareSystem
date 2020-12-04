using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.DrugAndTherapy
{
   public interface IDrugInRoomService
    {
        DrugInRoom AddDrugInRoom(DrugInRoom drugInRoom);
        DrugInRoom EditDrugInRoom(DrugInRoom drugInRoom);
        List<Drug> GetDrugsByRoomNumber(int roomNumber);
        bool DeleteDrugInRoom(int drugId);
        DrugInRoom ViewDrugInRoom(int drugId);
    }
}
