using Backend.Model.Enums;
using Backend.Model.PerformingExamination;
using System;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateAppointment
    {
        public Examination CreateCalidTestObject(int id, DateTime dateTime, string doctorJmbg, int roomId, int patientCardId)
        {
            return new Examination
            {
                Id = id,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = dateTime,
                Anamnesis = "Bol u grlu",
                DoctorJmbg = doctorJmbg,
                IdRoom = roomId,
                IdPatientCard = patientCardId,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };


        }


    }
}
