using System.Threading.Tasks;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Hymn.Infra.CrossCutting.Identity.Jwt;
using Hymn.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hymn.Services.Api.Controllers
{
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly AppJwtSettings _appJwtSettings;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appJwtSettings = appJwtSettings.Value;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Register(RegisterUser createUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = createUser.Username,
                Email = createUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, createUser.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) AddError(error.Description);
                return CustomResponse();
            }

            result = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            if (result.Succeeded) return CustomResponse(GetFullJwt(user.Email));
            
            foreach (var error in result.Errors) AddError(error.Description);
            
            return CustomResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByNameAsync(loginUser.Identifier) ??
                       await _userManager.FindByEmailAsync(loginUser.Identifier);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginUser.Password, false, true);

                if (result.Succeeded)
                {
                    var fullJwt = GetFullJwt(user.Email);
                    return CustomResponse(fullJwt);
                }

                if (result.IsLockedOut)
                {
                    AddError("This user is temporarily blocked");
                    return CustomResponse();
                }
            }

            AddError("Incorrect user or password");
            return CustomResponse();
        }

        private string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildToken();
        }
    }
}