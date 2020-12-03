﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Enums;
using Model.PerformingExamination;

namespace PatientWebApp.DTOs
{
    public class ExaminationDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DateAndTime { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public int IdRoom { get; set; }
        public string Anamnesis { get; set; }
        public string PatientJmbg { get; set; }
        public ExaminationStatus ExaminationStatus { get; set; }
        public bool IsSurveyCompleted { get; set; }

        public ExaminationDTO()
        {
        }
        public ExaminationDTO(int id,string type,string dateAndTime,string doctorJmbg,string doctorName,string doctorSurname,
            int idRoom,string anamnesis,string patientJmbg, bool isSurveyCompleted = false) {

            Id = id;
            Type = type;
            DateAndTime = dateAndTime;
            DoctorJmbg = doctorJmbg;
            DoctorName = doctorName;
            DoctorSurname = doctorSurname;
            IdRoom = idRoom;
            Anamnesis = anamnesis;
            PatientJmbg = patientJmbg;
            ExaminationStatus = ExaminationStatus.CREATED;
            IsSurveyCompleted = isSurveyCompleted;
        }

    }
}
