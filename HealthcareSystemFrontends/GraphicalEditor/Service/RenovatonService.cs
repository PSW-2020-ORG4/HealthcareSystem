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
        public List<RenovationPeriodDTO> GetBaseRenovationAlternativeAppointments(BaseRenovationDTO baseRenovationDTO)
        {
            return (List<RenovationPeriodDTO>)HTTPGetRequestWithObjectAsParam<RenovationPeriodDTO>("renovation/getBaseRenovationAlternativeAppointments", baseRenovationDTO);
        }
        public List<RenovationPeriodDTO> GetMergeRenovationAlternativeAppointments(MergeRenovationDTO mergeRenovationDTO)
        {
            return (List<RenovationPeriodDTO>)HTTPGetRequestWithObjectAsParam<RenovationPeriodDTO>("renovation/getMergeRenovationAlternativeAppointments", mergeRenovationDTO);
        }
        public List<RenovationPeriodDTO> GetDivideRenovationAlternativeAppointments(DivideRenovationDTO divideRenovationDTO)
        {
            return (List<RenovationPeriodDTO>)HTTPGetRequestWithObjectAsParam<RenovationPeriodDTO>("renovation/getDivideRenovationAlternativeAppointments", divideRenovationDTO);
        }

        public bool ScheduleBaseRenovation(BaseRenovationDTO baseRenovationDTO)
        {
            IRestResponse response = AddHTTPPostRequest("renovation/addBaseRenovation", baseRenovationDTO);
            return response.IsSuccessful;
        }
        public bool ScheduleMergeRenovation(MergeRenovationDTO mergeRenovationDTO)
        {
            IRestResponse response = AddHTTPPostRequest("renovation/addMergeRenovation", mergeRenovationDTO);
            return response.IsSuccessful;
        }
        public bool ScheduleDivideRenovation(DivideRenovationDTO divideRenovationDTO)
        {
            IRestResponse response = AddHTTPPostRequest("renovation/addDivideRenovation", divideRenovationDTO);
            return response.IsSuccessful;
        }

        public bool DeleteRenovation(int idRenovation)
        {
            IRestResponse response = HTTPDeleteRequest("renovation/deleteByRoomId/ " + idRenovation);
            return response.IsSuccessful;
        }


    }
}
