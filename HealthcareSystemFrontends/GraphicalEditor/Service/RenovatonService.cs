using GraphicalEditor.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Service
{
   public class RenovatonService : GenericHTTPService
    {
        public List<RenovationPeriodDTO> GetAlternativeAppointments(BaseRenovationDTO baseRenovationDTO)
        {
            return (List<RenovationPeriodDTO>)HTTPGetRequestWithObjectAsParam<RenovationPeriodDTO>("baseRenovation/getAlternativeAppointments", baseRenovationDTO);
        }

        public bool ScheduleBaseRenovation(BaseRenovationDTO baseRenovationDTO)
        {
            IRestResponse response = AddHTTPPostRequest("baseRenovation", baseRenovationDTO);
            return response.IsSuccessful;
        }
    }
}
