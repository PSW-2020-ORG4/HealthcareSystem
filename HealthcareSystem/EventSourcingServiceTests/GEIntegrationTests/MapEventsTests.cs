using EventSourcingService.Model;
using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using EventSourcingService.Service.GraphicalEditor;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EventSourcingServiceTests.GEIntegrationTests
{
    public class MapEventsTests
    {
        private IBuildingSelectionService buildingEventService; 
        private IFloorChangeService floorChangeEventService; 
        private IRoomSelectionService roomEventService;
        
        public MapEventsTests()
        {
            EventDataSeeder dataSeeder = new EventDataSeeder();
            DbContextOptionsBuilder<EventSourcingDbContext> builder = new DbContextOptionsBuilder<EventSourcingDbContext>(); ;
            DbContextOptions<EventSourcingDbContext> options;
            EventSourcingDbContext context;
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = new EventSourcingDbContext(options);

            dataSeeder.SeedAll(context);

            var buildingEventRepo = new DomainEventRepository<BuildingSelectionEvent>(context);
            buildingEventService = new BuildingSelectionService(buildingEventRepo);
            var floorChangeEventRepo = new DomainEventRepository<FloorChangeEvent>(context);
            floorChangeEventService = new FloorChangeService(floorChangeEventRepo);
            var roomEventRepo = new DomainEventRepository<RoomSelectionEvent>(context);
            roomEventService = new RoomSelectionService(roomEventRepo);
        }

        [Fact]
        public void Add_New_Building_Selection_Event()
        {
            BuildingSelectionEvent building = new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30),
                Username = "perapera",
                BuildingNumber = 6
            };

            buildingEventService.Add(building);

            BuildingSelectionEvent last = buildingEventService.GetAll().Last();

            Assert.True(
                last.TriggerTime.Equals(building.TriggerTime) 
                && last.Username == building.Username
                && last.BuildingNumber == building.BuildingNumber
            );
        }

        [Fact]
        public void Add_New_Floor_Change_Event()
        {
            FloorChangeEvent floorChange= new FloorChangeEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30),
                Username = "perapera",
                BuildingNumber = 6,
                Floor = 2
            };

            floorChangeEventService.Add(floorChange);

            FloorChangeEvent last = floorChangeEventService.GetAll().Last();

            Assert.True(
                last.TriggerTime.Equals(floorChange.TriggerTime)
                && last.Username == floorChange.Username
                && last.BuildingNumber == floorChange.BuildingNumber
                && last.Floor == floorChange.Floor
            );
        }

        [Fact]
        public void Add_New_Room_Selection_Event()
        {
            RoomSelectionEvent room = new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30),
                Username = "perapera",
                RoomNumber = 18
            };

            roomEventService.Add(room);

            RoomSelectionEvent last = roomEventService.GetAll().Last();

            Assert.True(
                last.TriggerTime.Equals(room.TriggerTime)
                && last.Username == room.Username
                && last.RoomNumber == room.RoomNumber
            );
        }

        [Fact]
        public void Get_All_Building_Selection_Events()
        {
            IEnumerable<BuildingSelectionEvent> buildingEvents = buildingEventService.GetAll();

            Assert.NotEmpty(buildingEvents);
        }

        [Fact]
        public void Get_All_Floor_Change_Events()
        {
            IEnumerable<FloorChangeEvent> floorEvents = floorChangeEventService.GetAll();

            Assert.NotEmpty(floorEvents);
        }

        [Fact]
        public void Get_All_Room_Selection_Events()
        {
            IEnumerable<RoomSelectionEvent> roomEvents = roomEventService.GetAll();

            Assert.NotEmpty(roomEvents);
        }
    }
}
