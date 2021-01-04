using Model.Enums;
using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository.BackendMapper
{
    public static class BloodTypeMapper
    {
        public static BloodType ToBloodType(this Backend.Model.Enums.BloodType bloodType)
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
    }
}
