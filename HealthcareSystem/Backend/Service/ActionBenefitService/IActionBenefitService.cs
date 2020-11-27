using Backend.Model;
using System.Collections.Generic;

namespace Backend.Service
{
    public interface IActionBenefitService
    {
        List<ActionBenefit> GetAllActionsBenefits();
        ActionBenefit GetActionBenefitById(int id);
        List<ActionBenefit> GetPublicActionsBenefits();

        void CreateActionBenefit(ActionBenefit ab);
        void CreateActionBenefit(string exchangeName, ActionBenefitMessage message);
        void UpdateActionBenefit(ActionBenefit ab);
        void DeleteActionBenefit(int id);
    }
}
