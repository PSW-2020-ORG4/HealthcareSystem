using GraphicalEditor.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
   public class DrugTypeService : GenericHTTPService
    {
        public List<DrugTypeDTO> GetDrugTypes()
        {
            return (List<DrugTypeDTO>)HTTPGetRequest<DrugTypeDTO>("drugType");
        }
    }
}
