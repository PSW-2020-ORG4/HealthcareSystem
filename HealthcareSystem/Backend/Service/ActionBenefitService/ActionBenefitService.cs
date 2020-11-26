﻿using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class ActionBenefitService : IActionBenefitService
    {
        private IActionBenefitRepository _actionBenefitRepository;
        private IPharmacyRepo _pharmacyRepo;

        public ActionBenefitService(IActionBenefitRepository actionBenefitRepository, IPharmacyRepo pharmacyRepo)
        {
            _actionBenefitRepository = actionBenefitRepository;
            _pharmacyRepo = pharmacyRepo;
        }

        public void CreateActionBenefit(ActionBenefit ab)
        {
            _actionBenefitRepository.CreateActionBenefit(ab);
        }

        public void CreateActionBenefit(string exchangeName, ActionBenefitMessage message)
        {
            Pharmacy p = _pharmacyRepo.GetPharmacyByExchangeName(exchangeName);
            if (p == null)
                return;
            ActionBenefit ab = new ActionBenefit(p.Id, message);

            CreateActionBenefit(ab);
        }

        public void DeleteActionBenefit(int id)
        {
            ActionBenefit ab = _actionBenefitRepository.GetActionBenefitById(id);
            if (ab == null)
                throw new ArgumentNullException(nameof(ab));

            _actionBenefitRepository.DeleteActionBenefit(ab);
        }

        public ActionBenefit GetActionBenefitById(int id)
        {
            return _actionBenefitRepository.GetActionBenefitById(id);
        }

        public List<ActionBenefit> GetAllActionsBenefits()
        {
            return _actionBenefitRepository.GetAllActionsBenefits().ToList();
        }

        public List<ActionBenefit> GetPublicActionsBenefits()
        {
            return _actionBenefitRepository.GetPublicActionsBenefits().ToList();
        }

        public void UpdateActionBenefit(ActionBenefit ab)
        {
            _actionBenefitRepository.UpdateActionBenefit(ab);
        }
    }
}