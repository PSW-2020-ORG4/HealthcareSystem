using Backend.Model;
using Backend.Model.Pharmacies;
using IntegrationAdaptersService2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersService2.Service
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
            PharmacySystem p = _pharmacyRepo.GetPharmacyByExchangeName(exchangeName);
            if (p == null)
                throw new ArgumentNullException("There is no pharmacy with that exchange!");
            if (message == null)
                throw new ArgumentNullException("Message can not be null!");

            ActionBenefit ab = new ActionBenefit(p.Id, message);

            CreateActionBenefit(ab);
        }

        public ActionBenefit GetActionBenefitById(int id)
        {
            return _actionBenefitRepository.GetActionBenefitById(id);
        }

        public List<ActionBenefit> GetAllActionsBenefits()
        {
            return _actionBenefitRepository.GetAllActionsBenefits().ToList();
        }

        public void MakePublic(int id, bool isPublic)
        {
            ActionBenefit ab = _actionBenefitRepository.GetActionBenefitById(id);
            if (ab == null)
                throw new ArgumentException("Action&Benefit not found");

            ab.IsPublic = isPublic;
            UpdateActionBenefit(ab);
        }

        public void UpdateActionBenefit(ActionBenefit ab)
        {
            _actionBenefitRepository.UpdateActionBenefit(ab);
        }
    }
}
