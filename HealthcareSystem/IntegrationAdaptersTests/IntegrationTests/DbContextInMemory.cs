// using Backend.Model;
// using Backend.Model.Pharmacies;
// using Microsoft.EntityFrameworkCore;
// using Model.Manager;
// using System;
// using System.Collections.Generic;

// namespace IntegrationAdaptersTests.IntegrationTests
// {
//     public class DbContextInMemory
//     {
//         private DbContextOptionsBuilder<MyDbContext> _builder = new DbContextOptionsBuilder<MyDbContext>();
//         private DbContextOptions<MyDbContext> _options;
//         public MyDbContext _context { get; private set; }

//         public DbContextInMemory()
//         {
//             _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
//             _options = _builder.Options;
//             _context = new MyDbContext(_options);

//             var pharmacy = new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "http://localhost:8080", GrpcHost = "localhost", GrpcPort = 9090, ActionsBenefitsExchangeName = "exchange", ActionsBenefitsSubscribed = true };
//             _context.Add(pharmacy);
//             _context.Add(new ActionBenefit { Id = 1, PharmacyId = 1, Pharmacy = pharmacy, Subject = "s", Message = "m", IsPublic = false });
//             _context.Add(new ActionBenefit { Id = 2, PharmacyId = 1, Pharmacy = pharmacy, Subject = "ss", Message = "mm", IsPublic = true });

//             _context.Add(new DrugType("tableta", "lek za glavu"));
//             _context.Add(new Drug() { DrugType_Id = 1, Name = "Brufen", Code = "20033", Quantity = 0 });

//             _context.Add(new Tender() { Id = 1, Name = "tender-1", EndDate = new DateTime(2021, 5, 5), IsClosed = false, QueueName = "tender-1", RoutingKey = "routing-key-1", Drugs = new List<TenderDrug>() { new TenderDrug() { DrugId = 1, Quantity = 10 } } });

//             _context.SaveChanges();
//         }
//     }
// }
