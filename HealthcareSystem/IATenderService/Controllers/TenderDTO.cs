﻿using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;

namespace IntegrationAdaptersTenderService.Controllers
{
    public class TenderDTO
    {
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<TenderDrug> Drugs { get; set; }
    }
}
