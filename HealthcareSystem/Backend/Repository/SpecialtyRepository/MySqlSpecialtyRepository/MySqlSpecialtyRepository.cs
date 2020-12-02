﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Model;
using Backend.Model.Users;

namespace Backend.Repository.SpecialtyRepository.MySqlSpecialtyRepository
{
    public class MySqlSpecialtyRepository : ISpecialtyRepository
    {
        private readonly MyDbContext _context;
        public MySqlSpecialtyRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Specialty> GetSpecialties()
        {
            return _context.Specialties.ToList();
        }
    }
}
