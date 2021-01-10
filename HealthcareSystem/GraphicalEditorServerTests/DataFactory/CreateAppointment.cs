using Backend.Model.Enums;
using Model.Enums;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.Text;

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
