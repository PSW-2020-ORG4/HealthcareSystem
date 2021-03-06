﻿using Backend.Model;
using Backend.Model.Pharmacies;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersDrugService.Repository.Implementation
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

        public IEnumerable<DrugConsumptionReport> GetDrugConsumptionForDate(DateRange dateRange)
        {
            var query = _context.DrugConsumptions
                .Where(d => d.Date >= dateRange.From)
                .Where(d => d.Date <= dateRange.To)
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
