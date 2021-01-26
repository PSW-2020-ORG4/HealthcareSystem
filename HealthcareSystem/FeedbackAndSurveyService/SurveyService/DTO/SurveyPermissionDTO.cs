﻿namespace FeedbackAndSurveyService.SurveyService.DTO
{
    public class SurveyPermissionDTO
    {
        public int Id { get; set; }
        public string DoctorJmbg { get; set; }

        public SurveyPermissionDTO(int id, string doctorJmbg)
        {
            Id = id;
            DoctorJmbg = doctorJmbg;
        }

        public SurveyPermissionDTO()
        {
        }
    }
}
