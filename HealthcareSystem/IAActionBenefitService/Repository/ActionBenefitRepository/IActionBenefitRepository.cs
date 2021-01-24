using Backend.Model;
using System.Collections.Generic;

namespace IntegrationAdaptersActionBenefitService.Repository
{
    public interface IActionBenefitRepository
    {
        IEnumerable<ActionBenefit> GetAllActionsBenefits();
        ActionBenefit GetActionBenefitById(int id);

        void CreateActionBenefit(ActionBenefit ab);
        void UpdateActionBenefit(ActionBenefit ab);
    }
}
