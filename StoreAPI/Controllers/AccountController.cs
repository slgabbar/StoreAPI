using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities;
using StoreAPI.ViewModels;

namespace StoreAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly StoreDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly Guid _userKey;

        public AccountController(StoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _userKey = new Guid("1b6ae95c-b028-46ae-9555-71a8458afa2a");
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();
            
            return user;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName= registerDto.UserName,
                Email= registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            await _userManager.AddToRoleAsync(user, "member");

            return StatusCode(201);
        }
    }
}