using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.ActionsBenefits
{
    public interface IActionBenefitRepository
    {
        public IEnumerable<ActionBenefit> GetPublic();
    }
}
