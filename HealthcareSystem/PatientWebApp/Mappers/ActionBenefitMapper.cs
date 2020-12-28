﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using PatientWebApp.DTOs;

namespace PatientWebApp.Mappers
{
    public class ActionBenefitMapper
    {
        public static ActionBenefit ActionBenefitDTOToActionBenefit(ActionBenefitDTO actionBenefitDTO)
        {
            ActionBenefit actionBenefit = new ActionBenefit();
            actionBenefit.Id = actionBenefitDTO.Id;
            actionBenefit.PharmacyId = actionBenefitDTO.PharmacyId;
            actionBenefit.Message = actionBenefitDTO.Message;
            actionBenefit.PharmacyId = actionBenefitDTO.PharmacyId;
            actionBenefit.Subject = actionBenefitDTO.Subject;
            return actionBenefit;
        }

        public static ActionBenefitDTO ActionBenefitToActionBenefitDTO(ActionBenefit actionBenefit)
        {
            ActionBenefitDTO actionBenefitDTO = new ActionBenefitDTO();
            actionBenefitDTO.Id = actionBenefit.Id;
            actionBenefitDTO.PharmacyId = actionBenefit.PharmacyId;
            actionBenefitDTO.PharmacyName = actionBenefit.Pharmacy.Name;
            actionBenefitDTO.Message = actionBenefit.Message;
            actionBenefitDTO.PharmacyId = actionBenefit.PharmacyId;
            actionBenefitDTO.Subject = actionBenefit.Subject;
            return actionBenefitDTO;
        }
    }
}