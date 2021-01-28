using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Model.Enum;
using EventSourcingService.Repository;
using EventSourcingService.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace EventSourcingServiceTests.IntegrationTests
{
    public class EvenStartSchedulingServiceTests
    {
        private EventStorePatientStartSchedulingService SetupEventStartService()
        {
            EventDataSeeder dataSeeder = new EventDataSeeder();
            DbContextOptionsBuilder<EventSourcingDbContext> builder = new DbContextOptionsBuilder<EventSourcingDbContext>(); ;
            DbContextOptions<EventSourcingDbContext> options;
            EventSourcingDbContext context;
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = new EventSourcingDbContext(options);

            dataSeeder.SeedAll(context);

            var eventRepo = new DomainEventRepository<PatientStartSchedulingEvent>(context);
            return new EventStorePatientStartSchedulingService(eventRepo);
        }

        [Fact]
        public void SuccessAddNewStartEvent()
        {
            EventStorePatientStartSchedulingService eventStartService = SetupEventStartService();
            PatientStartSchedulingEvent startEvent = eventStartService.Add(new PatientStartSchedulingEventDTO() { TriggerTime = DateTime.Now, UserAge = 35, UserGender = Gender.Female });

            Assert.True(eventStartService.Contain(startEvent.Id));
        }

        [Fact]
        public void SuccessGetAllStartEvent()
        {
            EventStorePatientStartSchedulingService eventStartService = SetupEventStartService();
            IEnumerable<PatientStartSchedulingEvent> startEvents = eventStartService.GetAll();

            Assert.NotEmpty(startEvents);
        }

    }
}
