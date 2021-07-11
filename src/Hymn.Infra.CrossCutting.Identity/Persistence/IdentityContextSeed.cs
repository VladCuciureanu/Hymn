using System;
using System.Linq;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hymn.Infra.CrossCutting.Identity.Persistence
{
    public class IdentityContextSeed
    {
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityContextSeed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
            ILogger<IdentityContextSeed> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public void SeedRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                _logger.Log(LogLevel.Information, "Seeding default roles...");
                foreach (var role in Enum.GetValues(typeof(Roles)))
                {
                    var roleObject = new IdentityRole(role.ToString());
                    roleObject.NormalizedName = roleObject.Name.ToUpper();
                    _roleManager.CreateAsync(roleObject).Wait();
                }
            }
        }

        public void SeedDevelopmentUsers()
        {
            if (!_userManager.Users.Any())
            {
                _logger.Log(LogLevel.Information, "Seeding development users...");
                foreach (var role in Enum.GetValues(typeof(Roles)))
                {
                    var user = new IdentityUser
                    {
                        UserName = role.ToString(),
                        Email = role.ToString()?.ToLower() + "@hymn.ro",
                        EmailConfirmed = true
                    };

                    _userManager.CreateAsync(user, "123Password!").Wait();
                    _userManager.AddToRoleAsync(user, role.ToString()).Wait();
                }
            }
        }
    }
}