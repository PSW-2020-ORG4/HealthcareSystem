using Backend.Model;
using Backend.Model.Pharmacies;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.DrugConsumptionRepository.MySqlDrugConsumptionRepository
{
    public class MySqlDrugConsumptionRepository : IDrugConsumptionRepository
    {
        private readonly MyDbContext _context;

        public MySqlDrugConsumptionRepository(MyDbContext context)
        {
            _context = context;
        }
        public IEnumerable<DrugConsumption> GetAllDrugConsumptions()
        {
            return _context.DrugConsumptions.ToList();
        }

        public DrugConsumption GetDrugConsumptionById(int id)
        {
            return _context.DrugConsumptions.Find(id);
        }

        public void CreateDrugConsumption(DrugConsumption dc)
        {
            _context.Add(dc);
            _context.SaveChanges();
        }

        public void UpdateDrugConsumption(DrugConsumption dc)
        {
            _context.Update(dc);
            _context.SaveChanges();
        }

        public void DeleteDrugConsumption(DrugConsumption dc)
        {
            _context.Remove(dc);
            _context.SaveChanges();
        }

        public IEnumerable<DrugConsumptionReport> GetDrugConsumptionForDate(DateTime from, DateTime to)
        {
            var query = _context.DrugConsumptions
                .Where(d => d.Date >= from)
                .Where(d => d.Date <= to)
                .Join(
                    _context.Drugs,
                    dc => dc.DrugId,
                    d => d.Id,
                    (dc, d) => new
                    {
                        DrugId = d.Id,
                        DrugName = d.Name,
                        TotalConsumption = dc.Quantity
                    })
                .GroupBy(i => new { i.DrugId, i.DrugName })
                .Select(g => new
                {
                    Id = g.Key.DrugId,
                    Name = g.Key.DrugName,
                    Quantity = g.Sum(i => i.TotalConsumption)
                });

            return query.Select(row => new DrugConsumptionReport(row.Id, row.Name, row.Quantity)).ToList();
        }
    }
}
