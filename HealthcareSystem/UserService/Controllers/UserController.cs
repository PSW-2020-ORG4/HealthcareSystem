using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Mapper;
using UserService.Service;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult GetByEmailAndPassword(UserInfoDTO userInfo)
        {
            var user = _userService.GetByEmailAndPassword(userInfo.Email, userInfo.Password);
            return Ok(user.ToUserDTO());
        }
    }
}