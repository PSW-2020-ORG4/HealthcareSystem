﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Service
{
    public interface IPatientService
    {
        void Register();
        void Activate(string jmbg);
        void Block(string jmbg);
        IEnumerable<PatientAccount> GetMalicious();
        IEnumerable<PatientAccount> GetAll();
        PatientAccount GetByJmbg(string jmbg);
    }
}