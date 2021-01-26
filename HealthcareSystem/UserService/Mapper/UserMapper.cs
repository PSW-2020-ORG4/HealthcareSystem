using UserService.DTO;
using UserService.Model;

namespace UserService.Mapper
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this UserAccount user)
        {
            var memento = user.GetMemento();
            return new UserDTO()
            {
                Email = memento.Email,
                Jmbg = memento.Jmbg,
                Type = memento.UserType.ToString(),
                CanLogIn = user.CanLogIn()
            };
        }
    }
}
