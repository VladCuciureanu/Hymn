using System;
using Hymn.Infra.CrossCutting.Identity.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hymn.Infra.CrossCutting.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IdentityBuilder AddIdentityConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(nameof(services));

            return services.AddDefaultIdentity<IdentityUser>(options => { options.User.RequireUniqueEmail = true; })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddIdentityEntityFrameworkContextConfiguration(
            this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            if (services == null) throw new ArgumentException(nameof(services));
            if (options == null) throw new ArgumentException(nameof(options));
            return services.AddDbContext<IdentityContext>(options);
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentException(nameof(app));

            return app.UseAuthentication()
                .UseAuthorization();
        }
    }
}