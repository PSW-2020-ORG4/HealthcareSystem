using System.Collections.Generic;

namespace UserService.ActionsBenefits
{
    public interface IActionBenefitRepository
    {
        public IEnumerable<ActionBenefit> GetPublic();
    }
}
