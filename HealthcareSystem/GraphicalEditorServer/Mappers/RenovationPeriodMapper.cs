using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class RenovationPeriodMapper
    {
        public static RenovationPeriodDTO RenovationPeriodToRenovationPeriodDTO(RenovationPeriod renovationPeriod)
        {
            return new RenovationPeriodDTO(renovationPeriod.BeginDate, renovationPeriod.EndDate);
        }
    }
}
