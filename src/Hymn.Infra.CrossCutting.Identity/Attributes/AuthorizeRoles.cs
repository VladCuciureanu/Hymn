using System;
using System.Linq;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Microsoft.AspNetCore.Authorization;

namespace Hymn.Infra.CrossCutting.Identity.Attributes
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles(params Roles[] roles)
        {
            var allowedRolesAsStrings = roles.Select(x => Enum.GetName(typeof(Roles), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}