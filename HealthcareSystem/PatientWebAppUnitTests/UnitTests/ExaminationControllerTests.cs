using Backend.Service;
using Model.Users;
using Moq;
using PatientWebApp.Controllers;
using Repository;
using System;
using Xunit;
using Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Backend;
using System.Collections.Generic;
using PatientWebApp.DTOs;
using PatientWebAppTests.CreateObjectsForTests;
using Backend.Service.SendingMail;
using System.Threading.Tasks;
using Backend.Model.Users;
using Service.ExaminationAndPatientCard;

namespace PatientWebAppTests.UnitTests
{
    public class ExaminationControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public ExaminationControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        [Fact]
        public void Get_existent_examination_by_patient_jmbg()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());
            ExaminationController examinationController = new ExaminationController(examinationService);

            var result = examinationController.GetExaminationsByPatient("1");

            Assert.True(result is OkObjectResult);
        }
        [Fact]
        public void Get_non_existent_examination_by_patient_jmbg()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());
            ExaminationController examinationController = new ExaminationController(examinationService);

            var result = examinationController.GetExaminationsByPatient("2q84793");

            Assert.True(result is NotFoundResult);
        }
       /* [Fact]
        public void Get_existent_examination_by_patient_search_one()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());
            ExaminationController examinationController = new ExaminationController(examinationService);

            var result = examinationController.GetExaminationsByPatientSearch("1","","","","");

            Assert.True(result is OkObjectResult);
        } */

    }
}
