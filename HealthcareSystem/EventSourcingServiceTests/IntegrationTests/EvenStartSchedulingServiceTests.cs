using System;
using System.Collections.Generic;
using System.Text;
using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventSourcingServiceTests.IntegrationTests
{
    public class EvenStartSchedulingServiceTests
    {
        private EventSourcingService.Service.EventStorePatientStartSchedulingService SetupEventStartService()
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
            return new EventSourcingService.Service.EventStorePatientStartSchedulingService(eventRepo);
        }

        [Fact]
        public void SuccessAddNewStartEvent()
        {
            EventSourcingService.Service.EventStorePatientStartSchedulingService eventStartService = SetupEventStartService();
            PatientStartSchedulingEvent startEvent = eventStartService.Add(new PatientStartSchedulingEventDTO() { TriggerTime = DateTime.Now, UserAge = 35, UserGender = EventSourcingService.Model.Enum.Gender.Female});

            Assert.False(startEvent is null);
        }

        [Fact]
        public void SuccessGetAllStartEvent()
        {
            EventSourcingService.Service.EventStorePatientStartSchedulingService eventStartService = SetupEventStartService();
            IEnumerable<PatientStartSchedulingEvent> startEvents = eventStartService.GetAll();

            Assert.NotEmpty(startEvents);
        }

    }
}
