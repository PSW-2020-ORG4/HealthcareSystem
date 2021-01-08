﻿using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;

namespace Backend.Service.Tendering
{
    public interface ITenderService
    {
        IEnumerable<Tender> GetAllTenders();
        Tender GetTenderById(int id);
        List<TenderDrugDTO> GetDrugsForTender(int id);
    }
}
