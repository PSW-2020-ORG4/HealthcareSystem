﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;

namespace UserService.Model
{
    public class Specialty
    {
        private int Id { get; }
        private string Name { get; }

        public Specialty(int id, string name)
        {
            Id = id;
            Name = name;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ValidationException("Specialty does not exist!");
        }
    }
}
