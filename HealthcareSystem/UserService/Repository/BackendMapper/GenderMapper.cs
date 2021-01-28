using Backend.Model.Enums;
using UserService.Model;

namespace UserService.Repository
{
    static class GenderMapper
    {
        internal static Gender ToGender(this GenderType? gender)
        {
            if (gender is null)
                return Gender.Unknown;
            switch (gender.Value)
            {
                case GenderType.F:
                    return Gender.Female;
                default:
                    return Gender.Male;
            }
        }

        internal static GenderType? ToBackendGender(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return GenderType.F;
                case Gender.Male:
                    return GenderType.M;
                default:
                    return null;
            }
        }

    }
}