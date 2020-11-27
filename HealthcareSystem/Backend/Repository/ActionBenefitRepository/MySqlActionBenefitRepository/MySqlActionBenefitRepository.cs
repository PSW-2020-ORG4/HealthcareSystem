using Backend.Model;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
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

        public void DeleteActionBenefit(ActionBenefit ab)
        {
            _context.ActionsBenefits.Remove(ab);
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

        public IEnumerable<ActionBenefit> GetPublicActionsBenefits()
        {
            return _context.ActionsBenefits.Where(ab => ab.IsPublic).ToList();
        }

        public void UpdateActionBenefit(ActionBenefit ab)
        {
            _context.Update(ab);
            _context.SaveChanges();
        }
    }
}
