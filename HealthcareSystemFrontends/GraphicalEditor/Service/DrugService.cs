

using GraphicalEditor.DTO;
using GraphicalEditor.Models;
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
        public List<DrugDTO> GetDrugsByRoomNumber(int roomNumber)
        {
            return (List<DrugDTO>)HTTPGetRequest<DrugDTO>("drugs/byRoomNumber/ " + roomNumber);
        }
        public List<DrugWithRoomDTO> GetDrugsWithRoomForSearchTerm(String searchTerm)
        {
            return (List<DrugWithRoomDTO>)HTTPGetRequest<DrugWithRoomDTO>("drugs/search?term=" + searchTerm);
        }
    }
}
