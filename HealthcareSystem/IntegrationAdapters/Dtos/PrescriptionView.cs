﻿using Backend.Model.Pharmacies;
using Model.Users;
using System.Collections.Generic;

namespace IntegrationAdapters.Dtos
{
    public class PrescriptionView
    {
        public List<Patient> patients { get; set; }
        public IEnumerable<PharmacySystem> pharmacySystems { get; set; }
    }
}