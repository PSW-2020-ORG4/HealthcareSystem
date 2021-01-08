using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Backend.Model;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;

namespace Backend.Repository.TenderRepository.MySqlTenderRepository
{
    public class MySqlTenderRepository : ITenderRepository
    {
        private readonly MyDbContext _context;

        public MySqlTenderRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tender> GetAllTenders()
        {
            var tenders = _context.Tenders.ToList();
            foreach (var tender in tenders)
            {
                tender.Drugs = _context.TenderDrugs.Where(x => x.TenderId == tender.Id).ToList();
            }
            return tenders;
        }

        public Tender GetTenderById(int id)
        {
            var tender = _context.Tenders.First(x => x.Id == id);
            if (tender == null) return null;

            tender.Drugs = _context.TenderDrugs.Where(x => x.TenderId == tender.Id).ToList();
            return tender;
        }

        public List<TenderDrugDTO> GetDrugsForTender(int id)
        {
            var drugs = new List<TenderDrugDTO>();
            foreach (var tenderDrug in GetTenderById(id).Drugs)
            {
                var drug = _context.Drugs.FirstOrDefault(x => x.Id == tenderDrug.DrugId);
                drugs.Add(new TenderDrugDTO()
                {
                    Id = drug.Id,
                    Name = drug.Name,
                    Quantity = tenderDrug.Quantity
                });
            }

            return drugs;
        }
    }
}
