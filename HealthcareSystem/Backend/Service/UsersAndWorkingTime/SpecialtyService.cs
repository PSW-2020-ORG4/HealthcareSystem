﻿using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Repository.SpecialtyRepository;

namespace Backend.Service.UsersAndWorkingTime
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }
        public List<Specialty> GetSpecialties()
        {
            return _specialtyRepository.GetSpecialties();   
        }
    }
}