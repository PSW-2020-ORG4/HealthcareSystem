﻿using Backend.Model.Users;
using Backend.Repository.DoctorSpecialtyRepository;
using System.Collections.Generic;

namespace Backend.Service.UsersAndWorkingTime
{
    public class DoctorSpecialtyService : IDoctorSpecialtyService
    {
        private readonly IDoctorSpecialtyRepository _doctorSpecialtyRepository;
        public DoctorSpecialtyService(IDoctorSpecialtyRepository doctorSpecialtyRepository)
        {
            _doctorSpecialtyRepository = doctorSpecialtyRepository;
        }
        public List<DoctorSpecialty> GetDoctorSpecialtyBySpecialtyId(int id)
        {
            return _doctorSpecialtyRepository.GetDoctorSpecialtyBySpecialtyId(id);
        }
    }
}
