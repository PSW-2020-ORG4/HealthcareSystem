using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;

namespace UserService.ActionsBenefits
{
    public class ActionBenefitRepository : IActionBenefitRepository
    {
        private readonly MyDbContext _context;

        public ActionBenefitRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ActionBenefit> GetPublic()
        {
            try
            {
                return _context.ActionsBenefits.Where(a => a.IsPublic).Select(
                    a => new ActionBenefit(a.Id, a.Pharmacy.Name, a.Message.Subject, a.Message.Message));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
