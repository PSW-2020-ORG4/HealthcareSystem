using Backend.Model.Manager;
using Backend.Service.RenovationService;
using GraphicalEditorServerTests.DataFactory;
using Service.ExaminationAndPatientCard;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GraphicalEditorServerTests.UnitTest
{
   public class ScheduleRenovationTests
    {
        private readonly StubRepository _stubRepository;
        private readonly CreateRenovation _createRenovaton;

        public ScheduleRenovationTests()
        {
            _stubRepository = new StubRepository();
            _createRenovaton = new CreateRenovation();
        }

        private RenovationService SetupRenovationService()
        {
            RenovationService renovationService = new RenovationService(_stubRepository.CreateRenovationStubRepository(),
                _stubRepository.CreateExaminationStubRepository(),
                new ExaminationService(_stubRepository.CreateExaminationStubRepository()),
                _stubRepository.CreateEquipmentTransferStubRepository());

            return renovationService;
        }

        [Fact]
        public void Schedule_basic_renovation_return_null()
        {
            RenovationService renovationService = SetupRenovationService();
            BaseRenovation result = renovationService.AddBaseRenovation(_createRenovaton.CreateInvalidTestObjectForSchedulingBaseRenovation());
            Assert.Null(result);
        }
       
        [Fact]
        public void Schedule_merge_renovation_return_null()
        {
            RenovationService renovationService = SetupRenovationService();
            MergeRenovation result = renovationService.AddMergeRenovation(_createRenovaton.CreateInvalidTestObjectForSchedulingMergeRenovation());
            Assert.Null(result);
        }
        [Fact]
        public void Schedule_merge_renovation_valid()
        {
            RenovationService renovationService = SetupRenovationService();
            MergeRenovation result = renovationService.AddMergeRenovation(_createRenovaton.CreateValidTestObjectForSchedulingMergeRenovation());
            Assert.Equal(result.Description, _createRenovaton.CreateValidTestObjectForSchedulingMergeRenovation().Description);
        }
        [Fact]
        public void Schedule_divide_renovation_return_null()
        {
            RenovationService renovationService = SetupRenovationService();
            DivideRenovation result = renovationService.AddDivideRenovation(_createRenovaton.CreateInvalidTestObjectForSchedulingDivideRenovation());
            Assert.Null(result);
        }
        [Fact]
        public void Schedule_divide_renovation_valid()
        {
            RenovationService renovationService = SetupRenovationService();
            DivideRenovation result = renovationService.AddDivideRenovation(_createRenovaton.CreateValidTestObjectForSchedulingDivideRenovation());
            Assert.Equal(result.Description, _createRenovaton.CreateValidTestObjectForSchedulingDivideRenovation().Description);
        }

    }
}
