using Model.Enums;
using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository.BackendMapper
{
    internal static class BloodTypeMapper
    {
        internal static BloodType ToBloodType(this Backend.Model.Enums.BloodType bloodType)
        {
            switch (bloodType)
            {
                case Backend.Model.Enums.BloodType.A:
                    return BloodType.A;
                case Backend.Model.Enums.BloodType.B:
                    return BloodType.B;
                case Backend.Model.Enums.BloodType.AB:
                    return BloodType.AB;
                case Backend.Model.Enums.BloodType.O:
                    return BloodType.O;
                default:
                    return BloodType.Unknown;
            }
        }

        internal static Backend.Model.Enums.BloodType ToBackendBloodType(this BloodType bloodType)
        {
            switch (bloodType)
            {
                case BloodType.A:
                    return Backend.Model.Enums.BloodType.A;
                case BloodType.B:
                    return Backend.Model.Enums.BloodType.B;
                case BloodType.AB:
                    return Backend.Model.Enums.BloodType.AB;
                case BloodType.O:
                    return Backend.Model.Enums.BloodType.O;
                default:
                    return Backend.Model.Enums.BloodType.UNKNOWN;
            }
        }
    }
}
