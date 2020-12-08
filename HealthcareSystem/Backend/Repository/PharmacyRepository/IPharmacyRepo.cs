﻿using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IPharmacyRepo
    {
        bool SaveChanges();

        IEnumerable<Pharmacy> GetAllPharmacies();
        Pharmacy GetPharmacyById(int id);
        Pharmacy GetPharmacyByIdNoTracking(int id);
        Pharmacy GetPharmacyByExchangeName(string exchangeName);
        IEnumerable<Pharmacy> GetPharmaciesBySubscribed(bool subscribed);

        void CreatePharmacy(Pharmacy p);
        void UpdatePharmacy(Pharmacy p);
        void DeletePharmacy(Pharmacy p);
    }
}
