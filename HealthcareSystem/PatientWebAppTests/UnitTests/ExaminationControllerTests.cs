using PatientWebApp.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using PatientWebAppTests.CreateObjectsForTests;
using Service.ExaminationAndPatientCard;
using Model.PerformingExamination;
using System.Collections.Generic;
using Backend.Model.Exceptions;

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

        private ExaminationController SetupExaminationController()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            ExaminationController examinationController = new ExaminationController(examinationService);

            return examinationController;
        }

        private ExaminationService SetupExaminationService()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());
            return examinationService;
        }


        [Fact]
        public void Get_existent_examination_by_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetExaminationsByPatient("1309998775018");

            Assert.True(result is List<Examination>);
        }


      /*  [Fact]
        public void Get_existent_examination_by_non_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetExaminationsByPatient("0000000000000");

            Assert.True(result is NotFoundObjectResult);
        } */

      /*  [Fact]
        public void Advanced_search_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var examinationSearchDTOValidObject = _objectFactory.GetExaminationSearchDTO().CreateValidTestObject();
            var result = examinationController.AdvanceSearchExaminations(examinationSearchDTOValidObject);

            Assert.True(result is OkObjectResult); 
        } */

        /*[Fact]
        public void Advanced_search_non_exitent_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var examinationSearchDTOInvalidObject = _objectFactory.GetExaminationSearchDTO().CreateInvalidTestObject();
            var result = examinationController.AdvanceSearchExaminations(examinationSearchDTOInvalidObject);

            Assert.True(result is BadRequestObjectResult);
        } */

        [Fact]
        public void Get_canceled_examination_by_valid_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetCanceledExaminationsByPatient("1309998775018");

            Assert.True(result is List<Examination>);
        }

        [Fact]
        public void Get_canceled_examination_by_invalid_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetCanceledExaminationsByPatient(null);

            Assert.True(result is null);
        }

        [Fact]
        public void Get_previous_examination_by_valid_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetPreviousExaminationsByPatient("1309998775018");

            Assert.True(result is List<Examination>);
        }

        [Fact]
        public void Get_previous_examination_by_invalid_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetPreviousExaminationsByPatient(null);

            Assert.True(result is null);
        }

        [Fact]
        public void Get_following_examination_by_valid_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetFollowingExaminationsByPatient("1309998775018");

            Assert.True(result is List<Examination>);
        }

        [Fact]
        public void Get_following_examination_by_invalid_patient_jmbg()
        {
            ExaminationService examinationService = SetupExaminationService();

            var result = examinationService.GetFollowingExaminationsByPatient(null);

            Assert.True(result is null);
        }

       /* [Fact]
        public void Cancel_following_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var result = examinationController.CancelExamination(1);

            Assert.True(result is OkResult);
        } */

        /*[Fact]
        public void Cancel_already_canceled_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var result = examinationController.CancelExamination(2);

            Assert.True(result is BadRequestObjectResult);
        } */

      /*  [Fact]
        public void Add_valid_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var examinationDTOValidObject = _objectFactory.GetExaminationDTO().CreateValidTestObject();
            var result = examinationController.AddExamination(examinationDTOValidObject);

            Assert.True(result is StatusCodeResult);
        } */

       /* [Fact]
        public void Add_invalid_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var examinationDTOInvalidObject = _objectFactory.GetExaminationDTO().CreateInvalidTestObject();
            var result = examinationController.AddExamination(examinationDTOInvalidObject);

            Assert.True(result is BadRequestObjectResult);
        } */

    }
}
