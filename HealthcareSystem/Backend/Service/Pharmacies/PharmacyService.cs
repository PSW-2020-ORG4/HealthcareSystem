﻿using Backend.Model.Pharmacies;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Pharmacies
{
    public class PharmacyService : IPharmacyService
    {
        private IPharmacyRepo _pharmacyRepo;

        public PharmacyService(IPharmacyRepo pharmacyRepo)
        {
            _pharmacyRepo = pharmacyRepo;
        }

        public void CreatePharmacy(Pharmacy p)
        {
            _pharmacyRepo.CreatePharmacy(p);
        }

        public void DeletePharmacy(Pharmacy p)
        {
            _pharmacyRepo.DeletePharmacy(p);
        }

        public IEnumerable<Pharmacy> GetAllPharmacies()
        {
            return _pharmacyRepo.GetAllPharmacies();
        }

        public List<Pharmacy> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _pharmacyRepo.GetPharmaciesBySubscribed(subscribed).ToList();
        }

        public Pharmacy GetPharmacyByExchangeName(string exchangeName)
        {
            return _pharmacyRepo.GetPharmacyByExchangeName(exchangeName);
        }

        public Pharmacy GetPharmacyById(int id)
        {
            return _pharmacyRepo.GetPharmacyById(id);
        }

        public Pharmacy GetPharmacyByIdNoTracking(int id)
        {
            return _pharmacyRepo.GetPharmacyByIdNoTracking(id);
        }

        public void SubscribeUnsubscibe(int id, bool subscribe)
        {
            Pharmacy p = _pharmacyRepo.GetPharmacyById(id);
            if (p == null)
                return;
            p.ActionsBenefitsSubscribed = subscribe;
            UpdatePharmacy(p);
        }

        public void UpdatePharmacy(Pharmacy p)
        {
            _pharmacyRepo.UpdatePharmacy(p);
        }
    }
}
