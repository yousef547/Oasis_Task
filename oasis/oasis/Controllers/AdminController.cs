using oasis.DTOs;
using oasis.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace oasis.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // get all user with roles
        [HttpGet("users-with-roles")]
        [Authorize(Policy = "RequireModeratorRole")]
        public async Task<ActionResult> GetUserWithRoles()
        {
            var user = await _userManager.Users.Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();
            return Ok(new ApiResponseMessageDto()
            {
                Date = user,
                StatusCode = 200,
                IsSuccess = true,
            });

        }

    }
}
