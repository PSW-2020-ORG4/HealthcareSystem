using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using PatientService.Model;
using PatientService.Repository;
using Xunit;

namespace PatientServiceTests.IntegrationTests
{
    public class PatientServiceTests
    {
        private PatientService.Service.PatientService SetupRepositoryAndService()
        {
            DataSeeder dataSeeder = new DataSeeder();
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>(); ;
            DbContextOptions<MyDbContext> options;
            MyDbContext context;
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = new MyDbContext(options);

            dataSeeder.SeedAll(context);

            var patientRepo = new PatientRepository(context);
            return new PatientService.Service.PatientService(patientRepo);
        }

        [Fact]
        public void SuccessGetTherapiesForExamination()
        {
            PatientService.Service.PatientService patientService = SetupRepositoryAndService();
            IEnumerable<Therapy> therapies = patientService.GetTherapiesForExamination("1309998775018", -86);

            Assert.Empty(therapies);
        }

        [Fact]
        public void SuccessGetExamination()
        {
            PatientService.Service.PatientService patientService = SetupRepositoryAndService();

            Assert.Throws<InvalidOperationException>(() => patientService.GetExamination("1309998775018", -86));
        }
    }
}
