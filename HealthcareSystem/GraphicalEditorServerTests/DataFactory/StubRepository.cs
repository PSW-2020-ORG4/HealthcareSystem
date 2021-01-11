using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Model.Users;
using Backend.Repository;
using Backend.Repository.EquipmentInExaminationRepository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.EquipmentTransferRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Model.Manager;
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
        private readonly CreateTransferEqupmentDTO _createTransferEqupmentDTO;
        private readonly CreateEquipmentInExamiantion _createEquipmentInExamiantion;

        public StubRepository()
        {
            _createEquipment = new CreateEquipment();
            _createEquipmentType = new CreateEquipmentType();
            _createEquipmentInRoom = new CreateEquipmentInRoom();
            _createRooms = new CreateRoom();
            _createDoctors = new CreateDoctor(_createRooms);
            _createPatientCard = new CreatePatientCard();
            _createExamination = new CreateExamination();
            _createTransferEqupmentDTO = new CreateTransferEqupmentDTO();
            _createEquipmentInExamiantion = new CreateEquipmentInExamiantion();


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

        public IEquipmentTransferRepository CreateEquipmentTransferStubRepository()
        {
            var equipmentTransferStubRepository = new Mock<IEquipmentTransferRepository>();
            equipmentTransferStubRepository.Setup(m => m.GetEquipmentTransferByRoomNumberAndDate(5, It.IsAny<DateTime>())).Returns((EquipmentTransfer)null);
            equipmentTransferStubRepository.Setup(m => m.GetEquipmentTransferByRoomNumberAndDate(15, It.IsAny<DateTime>())).Returns((EquipmentTransfer)null);
            equipmentTransferStubRepository.Setup(m => m.GetEquipmentTransferByRoomNumberAndDate(9, It.IsAny<DateTime>())).Returns((EquipmentTransfer)null);
            equipmentTransferStubRepository.Setup(m => m.GetEquipmentTransferByRoomNumberAndDate(28, It.IsAny<DateTime>())).Returns((EquipmentTransfer)null);
            equipmentTransferStubRepository.Setup(m => m.GetEquipmentTransferByRoomNumberAndDate(11, It.IsAny<DateTime>())).Returns((EquipmentTransfer)null);
            equipmentTransferStubRepository.Setup(m => m.GetEquipmentTransferByRoomNumberAndDate(10, It.IsAny<DateTime>())).Returns((EquipmentTransfer)null);
            return equipmentTransferStubRepository.Object;
        }

        public IRoomRepository CreateRoomStubRepository()
        {
            var roomStubRepository = new Mock<IRoomRepository>();
            roomStubRepository.Setup(x => x.GetAllRooms()).Returns(_createRooms.CreateRooms());
            roomStubRepository.Setup(m => m.CheckIfRoomExists(0)).Returns(true);
            roomStubRepository.Setup(m => m.GetRoomByNumber(0)).Returns(_createRooms.CreateRooms()[0]);
            roomStubRepository.Setup(m => m.GetRoomByNumber(1)).Returns(_createRooms.CreateRooms()[1]);
            roomStubRepository.Setup(m => m.GetRoomByNumber(2)).Returns(_createRooms.CreateRooms()[2]);
            return roomStubRepository.Object;
        }

        public IRenovationPeriodRepository CreateRenovationStubRepository()
        {
            var renovationStubRepository = new Mock<IRenovationPeriodRepository>();

            return renovationStubRepository.Object;
        }

        public IEquipmentInExaminationRepository CreateEquipmentInExaminationRepository()
        {
            var equipmentInExaminationRepository = new Mock<IEquipmentInExaminationRepository>();
            equipmentInExaminationRepository.Setup(m => m.GetEquipmentInExaminationByExaminationId(5)).Returns(_createEquipmentInExamiantion.CreateValidTestObjectForInitializingEquipmentTransfer());

            return equipmentInExaminationRepository.Object;
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

            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc))).Returns(_createExamination.CreateTestObjectForEquipmentTransfer1());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(5, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(15, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc))).Returns(_createExamination.CreateTestObjectForEquipmentTransfer2());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(11, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(10, new DateTime(2020, 12, 30, 8, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 8, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 8, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 9, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 9, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 9, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 9, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 10, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 10, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 10, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 10, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 11, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 11, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 11, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 11, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 12, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 12, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 12, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 12, 30, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(9, new DateTime(2020, 12, 30, 13, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(28, new DateTime(2020, 12, 30, 13, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());

            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(20, new DateTime(2020, 12, 15, 8, 0, 0,DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(10, new DateTime(2020, 12, 15, 8, 0, 0, DateTimeKind.Utc))).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetFollowingExaminationsByRoom(11)).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetFollowingExaminationsByRoom(9)).Returns(_createExamination.CreateTestObjectForEquipmentTransfer1());;
            examinationStubRepository.Setup(m => m.GetFollowingExaminationsByRoom(20)).Returns(_createExamination.CreateTestObjectForEquipmentTransfer3());
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
