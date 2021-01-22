using System;
using System.Collections.Generic;
using System.Text;
using EventSourcingService.DTO;
using EventSourcingService.DTO.PatientSchedulingEventDTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;
using EventSourcingService.Service;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventSourcingServiceTests.IntegrationTests
{
    public class EventStepSchedulingServiceTests
    {
        private EventStorePatientStepSchedulingService SetupEventStepService()
        {
            EventDataSeeder dataSeeder = new EventDataSeeder();
            DbContextOptionsBuilder<EventSourcingDbContext> builder = new DbContextOptionsBuilder<EventSourcingDbContext>(); ;
            DbContextOptions<EventSourcingDbContext> options;
            EventSourcingDbContext context;
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = new EventSourcingDbContext(options);

            dataSeeder.SeedAll(context);

            var eventRepo = new DomainEventRepository<PatientStepSchedulingEvent>(context);
            return new EventStorePatientStepSchedulingService(eventRepo);
        }

        [Fact]
        public void SuccessAddNewStepEvent()
        {
            EventStorePatientStepSchedulingService eventStepService = SetupEventStepService();
            PatientStepSchedulingEvent stepEvent = eventStepService.Add(new PatientStepSchedulingEventDTO() { TriggerTime = DateTime.Now, StartSchedulingEventId = 1, UserAge = 35, UserGender = EventSourcingService.Model.Enum.Gender.Female, EventStep = EventSourcingService.Model.Enum.EventStep.Date, ClickEvent = EventSourcingService.Model.Enum.ClickEvent.Next });

            Assert.True(eventStepService.Contain(stepEvent.Id));
        }

        [Fact]
        public void SuccessGetAllStepEvent()
        {
            EventStorePatientStepSchedulingService eventStepService = SetupEventStepService();
            IEnumerable<PatientStepSchedulingEvent> stepEvents = eventStepService.GetAll();

            Assert.NotEmpty(stepEvents);
        }

        [Fact]
        public void GetClosedStepStatistic()
        {
            EventStorePatientStepSchedulingService eventStepService = SetupEventStepService();
            StepClosureStatisticDTO stepStatistic = eventStepService.ClosedSchedulingStepStatistic();

            Assert.Equal(3, stepStatistic.TotalNumberOfClosures);
        }

        [Fact]
        public void GetPreviousStepStatistic()
        {
            EventStorePatientStepSchedulingService eventStepService = SetupEventStepService();
            StepPreviousStatisticDTO stepStatistic = eventStepService.PreviousSchedulingStepStatistic();

            Assert.Equal(4, stepStatistic.TotalNumberOfPrevious);
        }

    }
}
