using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiAdvance.Entities.Auth;
using WebApiAdvance.Entities.DTOs;

namespace WebApiAdvance.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly UserManager<AppUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;

        public AuthsController(UserManager<AppUser<Guid>> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var user = _mapper.Map<AppUser<Guid>>(register);
            var resultUser = await _userManager.CreateAsync(user, register.Password);
            if (!resultUser.Succeeded)
            {
                return BadRequest(new
                {
                    Errors = resultUser.Errors,
                    Code = 400
                });                
            }

            await _roleManager.CreateAsync(new IdentityRole("User"));
            var resultRole = await _userManager.AddToRoleAsync(user, "User");
            if (!resultRole.Succeeded)
            {
                return BadRequest(new
                {
                    Errors = resultRole.Errors,
                    Code = 400
                });
            }

            return Ok(new
            {
                Message = "User registered successfully",
                Code = 200
            });
        }
    }
}
