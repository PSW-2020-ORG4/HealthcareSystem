using Backend.Model.Enums;
using Backend.Model.Manager;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
   public class CreateRenovation
    {
        public BaseRenovation CreateInvalidTestObjectForSchedulingBaseRenovation()
        {
            return new BaseRenovation(9, new RenovationPeriod(new DateTime(2021, 1, 24, 8, 0, 0, DateTimeKind.Utc), new DateTime(2021, 1, 24, 13, 0, 0, DateTimeKind.Utc)),"krecenje",TypeOfRenovation.REGULAR_RENOVATION);              
        }
        public BaseRenovation CreateValidTestObjectForSchedulingBaseRenovation()
        {
            return new BaseRenovation(30, new RenovationPeriod(new DateTime(2021, 1, 24, 8, 0, 0, DateTimeKind.Utc), new DateTime(2021, 1, 24, 8, 30, 0, DateTimeKind.Utc)), "krecenje", TypeOfRenovation.REGULAR_RENOVATION);
        }
        public MergeRenovation CreateInvalidTestObjectForSchedulingMergeRenovation()
        {
            return new MergeRenovation(new RenovationPeriod(new DateTime(2021, 1, 24, 8, 0, 0, DateTimeKind.Utc), new DateTime(2021, 1, 24, 9, 0, 0, DateTimeKind.Utc)), "spajanje prostorija", TypeOfRenovation.MERGE_RENOVATION,9,20,"",TypeOfUsage.CONSULTING_ROOM);
        }
        public MergeRenovation CreateValidTestObjectForSchedulingMergeRenovation()
        {
            return new MergeRenovation(new RenovationPeriod(new DateTime(2021, 2, 24, 8, 0, 0, DateTimeKind.Utc), new DateTime(2021, 2, 24, 8, 30, 0, DateTimeKind.Utc)), "spajanje prostorija", TypeOfRenovation.MERGE_RENOVATION, 30, 31, "", TypeOfUsage.CONSULTING_ROOM);
        }

        public DivideRenovation CreateInvalidTestObjectForSchedulingDivideRenovation()
        {
            return new DivideRenovation(new RenovationPeriod(new DateTime(2021, 1, 24, 8, 0, 0, DateTimeKind.Utc), new DateTime(2021, 1, 24, 8, 30, 0, DateTimeKind.Utc)), "razdvajanje prostorija", TypeOfRenovation.DIVIDE_RENOVATION, 12,"","", TypeOfUsage.CONSULTING_ROOM, TypeOfUsage.OPERATION_ROOM);
        }
        public DivideRenovation CreateValidTestObjectForSchedulingDivideRenovation()
        {
            return new DivideRenovation(new RenovationPeriod(new DateTime(2021, 3, 24, 8, 0, 0, DateTimeKind.Utc), new DateTime(2021, 3, 24, 8, 30, 0, DateTimeKind.Utc)), "razdvajanje prostorija", TypeOfRenovation.DIVIDE_RENOVATION, 30, "", "", TypeOfUsage.CONSULTING_ROOM, TypeOfUsage.OPERATION_ROOM);
        }
    }
}
