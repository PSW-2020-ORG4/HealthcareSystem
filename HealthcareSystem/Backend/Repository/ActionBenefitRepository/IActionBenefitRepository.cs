using Backend.Model;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IActionBenefitRepository
    {
        IEnumerable<ActionBenefit> GetAllActionsBenefits();
        ActionBenefit GetActionBenefitById(int id);
        IEnumerable<ActionBenefit> GetPublicActionsBenefits();

        void CreateActionBenefit(ActionBenefit ab);
        void UpdateActionBenefit(ActionBenefit ab);
        void DeleteActionBenefit(ActionBenefit ab);
    }
}
