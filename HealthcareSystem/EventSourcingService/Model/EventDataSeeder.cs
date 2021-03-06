﻿using EventSourcingService.Model.GraphicalEditor;
using System;

namespace EventSourcingService.Model
{
    public class EventDataSeeder
    {
        private Random RandomGenerator { get; set; }
        private bool Verbose { get; set; }

        public EventDataSeeder()
        {
            RandomGenerator = new Random();
            Verbose = false;
        }

        public EventDataSeeder(bool verbose)
        {
            RandomGenerator = new Random();
            Verbose = verbose;
        }

        public void SeedAll(EventSourcingDbContext context)
        {
            if (Verbose) Console.WriteLine("Seeding events.");
            SeedEvents(context);
            if (Verbose) Console.WriteLine("Seeding GE events.");
            SeedGraphicalEditorEvents(context);

            context.SaveChanges();
        }

        private void SeedGraphicalEditorEvents(EventSourcingDbContext context)
        {
            /* SEQUENCE */
            context.Add(new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(0),
                Username = "perapera",
                BuildingNumber = 6
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(2),
                Username = "perapera",
                RoomNumber = 20
            });

            /* SEQUENCE */
            context.Add(new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(5),
                Username = "perapera",
                BuildingNumber = 6
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(7),
                Username = "perapera",
                RoomNumber = 21
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(9),
                Username = "perapera",
                RoomNumber = 20
            });

            /* SEQUENCE */
            context.Add(new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(11),
                Username = "perapera",
                BuildingNumber = 6
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(13),
                Username = "perapera",
                RoomNumber = 18
            });

            /* SEQUENCE */
            context.Add(new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(15),
                Username = "perapera",
                BuildingNumber = 6
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(17),
                Username = "perapera",
                RoomNumber = 19
            });

            /* SEQUENCE */
            context.Add(new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(19),
                Username = "perapera",
                BuildingNumber = 6
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(21),
                Username = "perapera",
                RoomNumber = 20
            });

            /* SEQUENCE */
            context.Add(new BuildingSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(23),
                Username = "perapera",
                BuildingNumber = 6
            });

            context.Add(new RoomSelectionEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(25),
                Username = "perapera",
                RoomNumber = 20
            });

            context.Add(new FloorChangeEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(1).AddHours(2).AddMinutes(30),
                Username = "perapera",
                BuildingNumber = 6,
                Floor = 2
            });

            context.SaveChanges();
        }

        private void SeedEvents(EventSourcingDbContext context)
        {
            context.Add(new PatientStartSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                UserAge = 33,
                UserGender = Enum.Gender.Female

            });

            context.Add(new PatientStartSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-2).AddHours(4).AddMinutes(10),
                UserAge = 18,
                UserGender = Enum.Gender.Male

            });

            context.Add(new PatientStartSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-3).AddHours(5).AddMinutes(15),
                UserAge = 70,
                UserGender = Enum.Gender.Male

            });

            context.Add(new PatientStartSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-4).AddHours(1).AddMinutes(20),
                UserAge = 100,
                UserGender = Enum.Gender.Female

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(35),
                StartSchedulingEventId = 1,
                UserAge = 33,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Date,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(36),
                StartSchedulingEventId = 1,
                UserAge = 33,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Specialty,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(37),
                StartSchedulingEventId = 1,
                UserAge = 33,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Doctor,
                ClickEvent = Enum.ClickEvent.Previous,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(38),
                StartSchedulingEventId = 1,
                UserAge = 33,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Specialty,
                ClickEvent = Enum.ClickEvent.Close,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-2).AddHours(4).AddMinutes(12),
                StartSchedulingEventId = 2,
                UserAge = 18,
                UserGender = Enum.Gender.Male,
                EventStep = Enum.EventStep.Date,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-2).AddHours(4).AddMinutes(13),
                StartSchedulingEventId = 2,
                UserAge = 18,
                UserGender = Enum.Gender.Male,
                EventStep = Enum.EventStep.Specialty,
                ClickEvent = Enum.ClickEvent.Close,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-3).AddHours(5).AddMinutes(17),
                StartSchedulingEventId = 3,
                UserAge = 70,
                UserGender = Enum.Gender.Male,
                EventStep = Enum.EventStep.Date,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-3).AddHours(5).AddMinutes(20),
                StartSchedulingEventId = 3,
                UserAge = 70,
                UserGender = Enum.Gender.Male,
                EventStep = Enum.EventStep.Specialty,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-3).AddHours(5).AddMinutes(25),
                StartSchedulingEventId = 3,
                UserAge = 70,
                UserGender = Enum.Gender.Male,
                EventStep = Enum.EventStep.Doctor,
                ClickEvent = Enum.ClickEvent.Previous,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-3).AddHours(5).AddMinutes(25),
                StartSchedulingEventId = 3,
                UserAge = 70,
                UserGender = Enum.Gender.Male,
                EventStep = Enum.EventStep.Doctor,
                ClickEvent = Enum.ClickEvent.Close,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-4).AddHours(1).AddMinutes(23),
                StartSchedulingEventId = 4,
                UserAge = 100,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Date,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-4).AddHours(1).AddMinutes(26),
                StartSchedulingEventId = 4,
                UserAge = 100,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Specialty,
                ClickEvent = Enum.ClickEvent.Next,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-4).AddHours(1).AddMinutes(29),
                StartSchedulingEventId = 4,
                UserAge = 100,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Doctor,
                ClickEvent = Enum.ClickEvent.Previous,

            });

            context.Add(new PatientStepSchedulingEvent()
            {
                TriggerTime = DateTime.Now.Date.AddDays(-4).AddHours(1).AddMinutes(29),
                StartSchedulingEventId = 4,
                UserAge = 100,
                UserGender = Enum.Gender.Female,
                EventStep = Enum.EventStep.Appointment,
                ClickEvent = Enum.ClickEvent.Previous,

            });

            context.Add(new PatientEndSchedulingEvent()
            {

                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                StartSchedulingEventTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                UserAge = 33,
                UserGender = Enum.Gender.Female,
                ReasonForEndOfAppointment = Enum.ReasonForEndOfAppointment.Success

            });

            context.Add(new PatientEndSchedulingEvent()
            {

                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                StartSchedulingEventTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                UserAge = 18,
                UserGender = Enum.Gender.Male,
                ReasonForEndOfAppointment = Enum.ReasonForEndOfAppointment.Success

            });

            context.Add(new PatientEndSchedulingEvent()
            {

                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                StartSchedulingEventTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                UserAge = 100,
                UserGender = Enum.Gender.Female,
                ReasonForEndOfAppointment = Enum.ReasonForEndOfAppointment.Unsuccess

            });

            context.Add(new PatientEndSchedulingEvent()
            {

                TriggerTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                StartSchedulingEventTime = DateTime.Now.Date.AddDays(-1).AddHours(2).AddMinutes(30),
                UserAge = 70,
                UserGender = Enum.Gender.Male,
                ReasonForEndOfAppointment = Enum.ReasonForEndOfAppointment.Unsuccess

            });


            context.SaveChanges();
        }
    }
}
