﻿using Backend.Model.Manager;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class StubRepository
    {
        private readonly CreateEquipment _createEquipment;
        private readonly CreateEquipmentType _createEquipmentType;
        private readonly CreateEquipmentInRoom _createEquipmentInRoom;
        private readonly CreateRoom _createRooms;
        private readonly CreateDoctor _createDoctors;
        private readonly CreatePatientCard _createPatientCard;
        private readonly CreateExamination _createExamination;

        public StubRepository()
        {
            _createEquipment = new CreateEquipment();
            _createEquipmentType = new CreateEquipmentType();
            _createEquipmentInRoom = new CreateEquipmentInRoom();
            _createRooms = new CreateRoom();
            _createDoctors = new CreateDoctor(_createRooms);
            _createPatientCard = new CreatePatientCard();
            _createExamination = new CreateExamination();
        }

       /* public IExaminationRepository CreateExaminationRepository() {
            var examinationStubRepository = new Mock<IExaminationRepository>();         
        
        }*/

        public IEquipmentRepository CreateEquipmentStubRepository() {
            var equipmentStubRepository = new Mock<IEquipmentRepository>();
            var equipment = new List<Equipment>();
            equipment.Add(_createEquipment.CreateValidTestObject());
            equipment.Add(_createEquipment.CreateValidTestObject(29));
            equipmentStubRepository.Setup(m => m.GetEquipmentById(30)).Returns(equipment[0]);
            equipmentStubRepository.Setup(m => m.GetEquipmentById(29)).Returns(equipment[1]);

            equipmentStubRepository.Setup(m => m.GetEquipmentById(1)).Returns(_createEquipment.CreateBedTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(2)).Returns(_createEquipment.CreateMaskTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(3)).Returns(_createEquipment.CreateNeedleTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(4)).Returns(_createEquipment.CreateComputerTestObject());
            equipmentStubRepository.Setup(m => m.GetEquipmentById(5)).Returns(_createEquipment.CreateBendTestObject());

            equipmentStubRepository.Setup(m => m.GetAllEquipment()).Returns(equipment);
            return equipmentStubRepository.Object;
        }

        public IEquipmentInRoomsRepository CreateEquipmentInRoomStubRepository()
        {
            var equipmentInRoomStubRepository = new Mock<IEquipmentInRoomsRepository>();
            var equipmentInRooms = new List<EquipmentInRooms>();
            equipmentInRooms.Add(_createEquipmentInRoom.CreateValidTestObject());
            equipmentInRooms.Add(_createEquipmentInRoom.CreateValidTestObject(30));
            equipmentInRoomStubRepository.Setup(m => m.GetEquipmentInRoomsByRoomNumber(56)).Returns(equipmentInRooms);
            equipmentInRoomStubRepository.Setup(m => m.GetEquipmentInRoomsByRoomNumber(-1)).Returns(new List<EquipmentInRooms>());

            equipmentInRoomStubRepository.Setup(x => x.GetEquipmentInRoomsByRoomNumber(0)).Returns(_createEquipmentInRoom.CreateEIRForFirstRoom());
            equipmentInRoomStubRepository.Setup(x => x.GetEquipmentInRoomsByRoomNumber(1)).Returns(_createEquipmentInRoom.CreateEIRForSecondRoom());
            equipmentInRoomStubRepository.Setup(x => x.GetEquipmentInRoomsByRoomNumber(2)).Returns(_createEquipmentInRoom.CreateEIRForThirdRoom());

            return equipmentInRoomStubRepository.Object;
        }

        public IEquipmentTypeRepository CreateEquipmentTypeStubRepository()
        {
            var equipmentTypeStubRepository = new Mock<IEquipmentTypeRepository>();
            var equipmentInRooms = new List<EquipmentType>();
            equipmentInRooms.Add(_createEquipmentType.CreateValidTestObject());
            equipmentInRooms.Add(_createEquipmentType.CreateValidTestObject());
            equipmentTypeStubRepository.Setup(m => m.GetAllEquipmentTypes()).Returns(equipmentInRooms);
            return equipmentTypeStubRepository.Object;
        }

        public IRoomRepository CreateRoomStubRepository()
        {
            var roomStubRepository = new Mock<IRoomRepository>();
            roomStubRepository.Setup(x => x.GetAllRooms()).Returns(_createRooms.CreateRooms());
            roomStubRepository.Setup(m => m.CheckIfRoomExists(0)).Returns(true);
            roomStubRepository.Setup(m => m.GetRoomByNumber(1)).Returns(_createRooms.CreateRooms()[0]);
            return roomStubRepository.Object;
        }

        public IRenovationPeriodRepository CreateRenovationStubRepository()
        {
            var renovationStubRepository = new Mock<IRenovationPeriodRepository>();

            return renovationStubRepository.Object;
        }

        public IExaminationRepository CreateExaminationStubRepository()
        {
            var examinationStubRepository = new Mock<IExaminationRepository>();
               
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(1, It.IsAny<DateTime>())).Returns(new List<Examination>());
            examinationStubRepository.Setup(
                m => 
                m.GetExaminationsByDoctorAndDateTime("0909965768767",
                It.Is<DateTime>( x => !x.Equals(new DateTime(2020, 12, 5, 7, 0, 0)) 
                                && !x.Equals(new DateTime(2020, 12, 5, 7, 30, 0))
                                && !x.Equals(new DateTime(2020, 12, 5, 8, 0, 0))))).Returns(new List<Examination>());

            examinationStubRepository.Setup(m => m.GetExaminationsByDoctorAndDateTime("0909965768767", new DateTime(2020, 12, 5, 7, 0, 0))).Returns(_createExamination.CreateInvalidTestObject1);
            examinationStubRepository.Setup(m => m.GetExaminationsByDoctorAndDateTime("0909965768767", new DateTime(2020, 12, 5, 7, 30, 0))).Returns(_createExamination.CreateInvalidTestObject2);
            examinationStubRepository.Setup(m => m.GetExaminationsByDoctorAndDateTime("0909965768767", new DateTime(2020, 12, 5, 8, 0, 0))).Returns(_createExamination.CreateInvalidTestObject3);
            examinationStubRepository.Setup(m => m.GetExaminationsByPatientAndDateTime(1, It.IsAny<DateTime>())).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.AddExamination(_createExamination.CreateValidTestObjectForMakingAnAppointemnt()));
            examinationStubRepository.Setup(m => m.GetExaminationById(2)).Returns(_createExamination.CreateValidTestObjectForMakingAnAppointemnt());
            return examinationStubRepository.Object;
        }
        public IDoctorRepository CreateDoctorStubRepository()
        {
            var doctorStubRepository = new Mock<IDoctorRepository>();
            var doctorInvalidObject = _createDoctors.CreateValidTestObject();
            var doctors = new List<Doctor>();
            doctors.Add(doctorInvalidObject);

            doctorStubRepository.Setup(m => m.GetDoctorByJmbg("0909965768767")).Returns(doctors[0]);
            doctorStubRepository.Setup(m => m.CheckIfDoctorExists("0909965768767")).Returns(true);
            doctorStubRepository.Setup(m => m.GetAllDoctors()).Returns(doctors);

            return doctorStubRepository.Object;
        }
        public IActivePatientCardRepository CreatePatientCardStubRepository()
        {
            var patientCardStubRepository = new Mock<IActivePatientCardRepository>();
            var patientCardValidObject = _createPatientCard.CreateValidTestObject();
            var patientCards = new List<PatientCard>();
            patientCards.Add(patientCardValidObject);

            patientCardStubRepository.Setup(m => m.GetPatientCardByJmbg("1234567891234")).Returns(patientCards[0]);
            patientCardStubRepository.Setup(m => m.CheckIfPatientCardExists(1)).Returns(true);
           
            return patientCardStubRepository.Object;

        }

    }
}
