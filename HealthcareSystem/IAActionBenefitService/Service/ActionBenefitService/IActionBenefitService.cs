using Backend.Model;
using System.Collections.Generic;

namespace IntegrationAdaptersActionBenefitService.Service
{
    public interface IActionBenefitService
    {
        List<ActionBenefit> GetAllActionsBenefits();
        ActionBenefit GetActionBenefitById(int id);

        void CreateActionBenefit(ActionBenefit ab);
        void CreateActionBenefit(string exchangeName, ActionBenefitMessage message);
        void UpdateActionBenefit(ActionBenefit ab);
        void MakePublic(int id, bool isPublic);
    }
}
