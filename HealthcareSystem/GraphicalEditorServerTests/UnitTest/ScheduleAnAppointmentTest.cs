using Backend.Service.ExaminationAndPatientCard;
using GraphicalEditorServerTests.DataFactory;
using Xunit;
namespace GraphicalEditorServerTests.UnitTest
{
    public class ScheduleAnAppointmentTest
    {
        private readonly StubRepository _stubRepository;
        private readonly CreateExamination _createExamination;

        public ScheduleAnAppointmentTest()
        {
            _stubRepository = new StubRepository();
            _createExamination = new CreateExamination();

        }

        public ScheduleAppointmentService SetupScheduleAppointmentService()
        {
            return new ScheduleAppointmentService(_stubRepository.CreateExaminationStubRepository());
        }

        [Fact]
        public void Schedule_an_appointment()
        {
            ScheduleAppointmentService scheduleAppointmentService = SetupScheduleAppointmentService();
            scheduleAppointmentService.ScheduleAnAppointmentByDoctor(_createExamination.CreateValidTestObjectForMakingAnAppointemnt());
            Assert.True(scheduleAppointmentService.GetExaminationById(2).Id == 2);
        }
    }
}
