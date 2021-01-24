using System;
using System.Collections.Generic;
using System.Text;
using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;
using EventSourcingService.Service;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventSourcingServiceTests.IntegrationTests
{
    public class EventEndSchedulingServiceTests
    {
        private EventStorePatientEndSchedulingService SetupEventEndService()
        {
            EventDataSeeder dataSeeder = new EventDataSeeder();
            DbContextOptionsBuilder<EventSourcingDbContext> builder = new DbContextOptionsBuilder<EventSourcingDbContext>(); ;
            DbContextOptions<EventSourcingDbContext> options;
            EventSourcingDbContext context;
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = new EventSourcingDbContext(options);

            dataSeeder.SeedAll(context);

            var eventRepo = new DomainEventRepository<PatientEndSchedulingEvent>(context);
            return new EventStorePatientEndSchedulingService(eventRepo);
        }

        [Fact]
        public void SuccessAddNewEndEvent()
        {
            EventStorePatientEndSchedulingService eventEndService = SetupEventEndService();
            PatientEndSchedulingEvent endEvent = eventEndService.Add(new PatientEndSchedulingEventDTO() { TriggerTime = DateTime.Now, StartSchedulingEventId = 1, StartSchedulingEventTime = DateTime.Now.AddMinutes(3), UserAge = 35, UserGender = EventSourcingService.Model.Enum.Gender.Female, ReasonForEndOfAppointment = EventSourcingService.Model.Enum.ReasonForEndOfAppointment.Success });

            Assert.True(eventEndService.Contain(endEvent.Id));
        }

        [Fact]
        public void SuccessGetAllEndEvent()
        {
            EventStorePatientEndSchedulingService eventEndService = SetupEventEndService();
            IEnumerable<PatientEndSchedulingEvent> endEvents = eventEndService.GetAll();

            Assert.NotEmpty(endEvents);
        }

        [Fact]
        public void GetSuccessfulSchedulingDuration()
        {
            EventStorePatientEndSchedulingService eventEndService = SetupEventEndService();
            MinAvgMaxStatisticDTO endStatistic = eventEndService.SuccessfulSchedulingDuration();

            Assert.Equal(0, endStatistic.Average);
        }

        [Fact]
        public void GetSuccessfulGenderStatistic()
        {
            EventStorePatientEndSchedulingService eventEndService = SetupEventEndService();
            GenderStatisticDTO endStatistic = eventEndService.SuccessfulSchedulingGenderStatistic();

            Assert.Equal(2, endStatistic.TotalNumber);
        }

        [Fact]
        public void GetSuccessfulAgeStatistic()
        {
            EventStorePatientEndSchedulingService eventEndService = SetupEventEndService();
            MinAvgMaxStatisticDTO endStatistic = eventEndService.SuccessfulSchedulingAgeStatistic();

            Assert.Equal(25, endStatistic.Average);
        }

        [Fact]
        public void GetNumberOfUnsuccessfulScheduling()
        {
            EventStorePatientEndSchedulingService eventEndService = SetupEventEndService();
            SuccessfulAndUnsuccessfulSchedulingDTO endStatistic = eventEndService.SuccessfulAndUnsuccessfulScheduling();

            Assert.Equal(2, endStatistic.NumberOfUnsuccessfulScheduling);
        }

    }
}
