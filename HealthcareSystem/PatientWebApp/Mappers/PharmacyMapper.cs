using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using PatientWebApp.DTOs;

namespace PatientWebApp.Mappers
{
    public class PharmacyMapper
    {
        public static PharmacySystem PharmacyDTOToPharmacy(PharmacyDTO pharmacyDTO)
        {
            PharmacySystem pharmacySystem = new PharmacySystem();
            pharmacySystem.Id = pharmacyDTO.Id;
            pharmacySystem.Name = pharmacyDTO.Name;
            
            return pharmacySystem;
        }

        public static PharmacyDTO PharmacyToPharmacyDTO(PharmacySystem pharmacySystem)
        {
            PharmacyDTO pharmacyDTO = new PharmacyDTO();
            pharmacyDTO.Id = pharmacySystem.Id;
            pharmacyDTO.Name = pharmacySystem.Name;
            pharmacyDTO.ActionsBenefitsExchangeName = pharmacySystem.ActionsBenefitsExchangeName;
            pharmacyDTO.ActionsBenefitsSubscribed = pharmacySystem.ActionsBenefitsSubscribed;
            pharmacyDTO.ApiKey = pharmacySystem.ApiKey;
            pharmacyDTO.GrpcHost = pharmacySystem.GrpcHost;
            pharmacyDTO.GrpcPort = pharmacySystem.GrpcPort;
            pharmacyDTO.Url = pharmacySystem.Url;
            return pharmacyDTO;
        }
    }
}
