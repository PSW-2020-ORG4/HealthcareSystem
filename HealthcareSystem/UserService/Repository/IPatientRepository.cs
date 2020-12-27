﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository.CRUD;

namespace UserService.Repository
{
    public interface IPatientRepository : IRead<Patient, string>, ICreate<Patient>, IUpdate<Patient>
    {
    }
}
