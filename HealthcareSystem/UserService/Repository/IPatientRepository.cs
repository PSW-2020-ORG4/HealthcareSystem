﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository.CRUD;

namespace UserService.Repository
{
    public interface IPatientRepository : IRead<PatientAccount, string>, ICreate<PatientAccount>, IUpdate<PatientAccount>
    {
        IEnumerable<PatientAccount> GetAll(DateTime earliestMaliciousActionDate);
        PatientAccount Get(string id, DateTime earliestMaliciousActionDate);
    }
}
