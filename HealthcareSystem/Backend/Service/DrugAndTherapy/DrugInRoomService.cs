using Backend.Model.Manager;
using Backend.Repository.DrugInRoomRepository;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.DrugAndTherapy
{
   public class DrugInRoomService : IDrugInRoomService
    {
        private IDrugInRoomRepository _drugInRoomRepository;
        private IDrugService _drugService;

        public DrugInRoomService(IDrugInRoomRepository drugInRoomRepository, IDrugService drugService)
        {
            _drugInRoomRepository = drugInRoomRepository;
            _drugService = drugService;

        }

        public DrugInRoom AddDrugInRoom(DrugInRoom drugInRoom)
        {
            Console.WriteLine("usao u servis");
            return _drugInRoomRepository.AddDrug(drugInRoom);
        }

        public void DeleteDrugInRoom(int drugId)
        {
             _drugInRoomRepository.DeleteDrug(drugId);
        }

        public DrugInRoom EditDrugInRoom(DrugInRoom drugInRoom)
        {
            return _drugInRoomRepository.SetDrug(drugInRoom);
        }

        public List<Drug> GetDrugsByRoomNumber(int roomNumber)
        {
            return _drugService.GetDrugsByRoomNumber(roomNumber);
        }

        public List<DrugInRoom> GetDrugsInRoomsFromDrug(Drug drug)
        {
            return _drugInRoomRepository.GetDrugInRoomByDrugId(drug.Id);
        }

    }
}
