using Backend.Model;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.DrugRepository.MySQLDrugRepository
{
    public class MySqlConfirmedDrugRepository : IConfirmedDrugRepository
    {
        private readonly MyDbContext _context;
        public MySqlConfirmedDrugRepository(MyDbContext context)
        {
            _context = context;
        }

        public void DeleteDrug(int id)
        {
            throw new NotImplementedException();
        }

        public List<Drug> GetAllDrugs()
        {
            return _context.Drugs.ToList();
        }

        public Drug GetDrugById(int id)
        {
            return _context.Drugs.SingleOrDefault(x => x.Id == id);
        }

        public int getLastId()
        {
            //TODO:Implement
            return 0;
        }

        public Drug AddDrug(Drug drug)
        {
            drug.DrugType = _context.DrugTypes.SingleOrDefault(x => x.Id == drug.DrugType.Id);
            _context.Drugs.Add(drug);
            _context.SaveChanges();
            return drug;
        }

        public Drug SetDrug(Drug drug)
        {
            _context.Update(drug);
            _context.SaveChanges();
            return drug;
        }
    }
}
