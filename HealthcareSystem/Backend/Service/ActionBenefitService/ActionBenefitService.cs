using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void CreateActionBenefit(string exchangeName, string message)
        {
            throw new NotImplementedException();
        }

        public void DeleteActionBenefit(int id)
        {
            throw new NotImplementedException();
        }

        public ActionBenefit GetActionBenefitById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ActionBenefit> GetAllActionsBenefits()
        {
            throw new NotImplementedException();
        }

        public List<ActionBenefit> GetPublicActionsBenefits()
        {
            throw new NotImplementedException();
        }

        public void UpdateActionBenefit(ActionBenefit ab)
        {
            throw new NotImplementedException();
        }
    }
}
