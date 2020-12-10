

using GraphicalEditor.DTO;
using GraphicalEditor.Models;
using GraphicalEditor.Models.Drugs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
   public class DrugService : GenericHTTPService
    {
        public string AddDrug(Drug drug)
        {
            IRestResponse response = AddHTTPPostRequest("drugs", drug);
            return response.Content;
        }

        private string DrugInRoomToJSONConverter(MapObject mapObject, Drug drug)
        {
            String JSONContent = "'DrugId': " + drug.Id;
            JSONContent += ",'RoomNumber': " + mapObject.MapObjectEntity.Id;
            JSONContent += ",'Quantity': " + drug.Quantity;

            return JSONContent;
        }

        public void AddDrugToRoom(MapObject mapObject, Drug drug)
        {
            if (mapObject.CheckIfDBAddableRoom())
            {
                String JSONContent = DrugInRoomToJSONConverter(mapObject, drug);
                AddHTTPPostRequest("drugInRoom", JSONContent);
            }
        }

        public List<Drug> GetDrugsByRoomNumber(int roomNumber)
        {
            return (List<Drug>)HTTPGetRequest<Drug>("drugs/byRoomNumber/ " + roomNumber);
        }
        public List<DrugWithRoomDTO> GetEquipmentWithRoomForSearchTerm(String searchTerm)
        {
            return (List<DrugWithRoomDTO>)HTTPGetRequest<DrugWithRoomDTO>("drugs/search?term=" + searchTerm);
        }
    }
}
