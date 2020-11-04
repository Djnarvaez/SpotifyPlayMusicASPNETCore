using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlayMusic.Models.DTO;

namespace PlayMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserDTO userDTO)
        {
            var user = new IdentityUser { UserName = userDTO.Email, Email = userDTO.Email };
            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Administrador"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Administrador"));
                }
                if (!await roleManager.RoleExistsAsync("Invitado"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Invitado"));
                }
                await userManager.AddToRoleAsync(user, "Invitado");

                return Ok();
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserDTO userDTO)
        {
            var result = await signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuario = await userManager.FindByEmailAsync(userDTO.Email);
                var roles = await userManager.GetRolesAsync(usuario);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration["JWT:key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new UserTokenDTO
                {
                    Token = tokenHandler.WriteToken(token),
                    Roles = roles,
                    UserName = usuario.UserName
                };
            }
            else
            {
                return StatusCode(404, ModelState);
            }
        }
    }
}
