﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class ObjectFactory
    {
        public ICreateObject GetObject(string objectType)
        {
            if (objectType.Equals("PatientDTO"))
            {
                return new CreatePatientDTO();
            }
            if (objectType.Equals("Patient"))
            {
                return new CreatePatient();
            }
            if (objectType.Equals("PatientCard"))
            {
                return new CreatePatientCard();
            }

            return null;
        }
    }
}
