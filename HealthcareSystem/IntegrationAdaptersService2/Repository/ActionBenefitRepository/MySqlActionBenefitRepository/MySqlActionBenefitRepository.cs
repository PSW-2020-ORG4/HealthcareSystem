using Backend.Model;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersService2.Repository
{
    public class MySqlActionBenefitRepository : IActionBenefitRepository
    {
        private readonly MyDbContext _context;

        public MySqlActionBenefitRepository(MyDbContext context)
        {
            _context = context;
        }

        public void CreateActionBenefit(ActionBenefit ab)
        {
            _context.ActionsBenefits.Add(ab);
            _context.SaveChanges();
        }

        public ActionBenefit GetActionBenefitById(int id)
        {
            return _context.ActionsBenefits.Find(id);
        }

        public IEnumerable<ActionBenefit> GetAllActionsBenefits()
        {
            return _context.ActionsBenefits.ToList();
        }

        public void UpdateActionBenefit(ActionBenefit ab)
        {
            _context.Update(ab);
            _context.SaveChanges();
        }
    }
}
