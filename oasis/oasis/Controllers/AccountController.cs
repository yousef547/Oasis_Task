using oasis.Data;
using oasis.DTOs;
using oasis.Entities;
using oasis.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oasis.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _itokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService TokenService)
        {
            _itokenService = TokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        // Register

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username))
            {
                return Ok(new ApiResponseMessageDto()
                {
                    Date = { },
                    StatusCode = 400,
                    IsSuccess = false,
                    Messages = BadRequest("Username is taken")
                });
            }
            
            var user = new AppUser
            {
                UserName = registerDTO.Username.ToLower(),
        
            };
            var result = await _userManager.CreateAsync(user,registerDTO.Password);
            if (!result.Succeeded) return Ok(new ApiResponseMessageDto()       {
                Date = { },
                StatusCode = 400,
                IsSuccess = false,
                Messages = BadRequest(ModelState)
            }); 
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if(!roleResult.Succeeded) return Ok(new ApiResponseMessageDto()
            {
                Date = { },
                StatusCode = 400,
                IsSuccess = false,
                Messages = BadRequest(ModelState)
            }); 
            var userDto  = new UserDto
            {
                Username = user.UserName,
                Token = await _itokenService.CreateToken(user),
            };
            return Ok(new ApiResponseMessageDto()
            {
                Date = userDto,
                StatusCode = 200,
                IsSuccess = true,
            });
        }

        // Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) return Ok(new ApiResponseMessageDto()
            {
                Date = { },
                StatusCode = 400,
                IsSuccess = false,
                Messages = Unauthorized("Invalid username")
            });
            var result = await _signInManager
                .CheckPasswordSignInAsync(user,loginDto.Password,false);
            if(!result.Succeeded) return Ok(new ApiResponseMessageDto()
            {
                Date = { },
                StatusCode = 400,
                IsSuccess = false,
                Messages = Unauthorized()
            });


            var userDto = new UserDto
            {
                Username = user.UserName,
                Token = await _itokenService.CreateToken(user),
            };
            return Ok(new ApiResponseMessageDto()
            {
                Date = userDto,
                StatusCode = 200,
                IsSuccess = true,
            });
        }
        // The user exists
        private async Task<bool> UserExists(string userName)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}
