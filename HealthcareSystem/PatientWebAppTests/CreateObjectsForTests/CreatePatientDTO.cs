using PatientWebApp.DTOs;
using System;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreatePatientDTO
    {
        public PatientDTO CreateValidTestObject()
        {
            throw new NotImplementedException();
            /*
            return new PatientDTO(name: "Ana", surname: "Anic", jmbg: "1206988452102", gender: GenderType.F,
                                                    dateOfBirth: DateTime.Now, phone: "065432485", countryId: 1, countryName: "Srbija",
                                                    cityZipCode: 21000, cityName: "Novi Sad", homeAddress: "Bulevar Oslobodjenja 10",
                                                    bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, hasInsurance: false, lbo: "",
                                                    alergies: "", medicalHistory: "", email: "ana@gmail.com", password: "12345678", isBlocked: false);
        */
        }
        public PatientDTO CreateInvalidTestObject()
        {
            throw new NotImplementedException();
            /*
            return new PatientDTO(name: "Ana", surname: "Anic", jmbg: null, gender: GenderType.F,
                                                    dateOfBirth: DateTime.Now, phone: "", countryId: 1, countryName: "Srbija",
                                                    cityZipCode: 21000, cityName: "Novi Sad", homeAddress: null,
                                                    bloodType: BloodType.A, rhFactor: RhFactorType.POSITIVE, hasInsurance: false, lbo: "",
                                                   alergies: null, medicalHistory: null, email: null, password: "", isBlocked: false);
        */
        }
    }

}
