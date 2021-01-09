using Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Repository
{
    static class GenderMapper
    {
        internal static Gender ToGender(this GenderType gender)
        {
            switch (gender)
            {
                case GenderType.F:
                    return Gender.Female;
                default:
                    return Gender.Male;
            }
        }

        internal static GenderType ToBackendGender(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return GenderType.F;
                default:
                    return GenderType.M;
            }
        }

    }
}