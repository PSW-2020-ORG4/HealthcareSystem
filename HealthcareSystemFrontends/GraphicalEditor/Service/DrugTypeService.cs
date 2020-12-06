using GraphicalEditor.Models.Drugs;
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
        public string AddDrugType(DrugType drugType)
        {
            IRestResponse response = AddHTTPPostRequest("drugType", drugType);
            return response.Content;
        }

        public List<DrugType> GetDrugTypes()
        {
            return (List<DrugType>)HTTPGetRequest<DrugType>("drugType");
        }
    }
}
