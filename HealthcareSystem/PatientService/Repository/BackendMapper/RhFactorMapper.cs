using Backend.Model.Enums;
using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository.BackendMapper
{
    internal static class RhFactorMapper
    {
        internal static RhFactor ToRhFactor(this RhFactorType rhFactor)
        {
            switch (rhFactor)
            {
                case RhFactorType.NEGATIVE:
                    return RhFactor.Negative;
                case RhFactorType.POSITIVE:
                    return RhFactor.Positive;
                default:
                    return RhFactor.Unknown;
            }
        }

        internal static RhFactorType ToBackendRhFactor(this RhFactor rhFactor)
        {
            switch (rhFactor)
            {
                case RhFactor.Negative:
                    return RhFactorType.NEGATIVE;
                case RhFactor.Positive:
                    return RhFactorType.POSITIVE;
                default:
                    return RhFactorType.UNKNOWN;
            }
        }
    }
}
