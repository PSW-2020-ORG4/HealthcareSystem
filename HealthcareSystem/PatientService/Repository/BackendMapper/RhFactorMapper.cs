using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository.BackendMapper
{
    public static class RhFactorMapper
    {
        public static RhFactor ToRhFactor(this Backend.Model.Enums.RhFactorType rhFactor)
        {
            switch (rhFactor)
            {
                case Backend.Model.Enums.RhFactorType.NEGATIVE:
                    return RhFactor.Negative;
                case Backend.Model.Enums.RhFactorType.POSITIVE:
                    return RhFactor.Positive;
                default:
                    return RhFactor.Unknown;
            }
        }
    }
}
