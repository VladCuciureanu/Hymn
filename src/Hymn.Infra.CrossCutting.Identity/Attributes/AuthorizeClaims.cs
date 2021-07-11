using System.Security.Claims;
using Hymn.Infra.CrossCutting.Identity.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Hymn.Infra.CrossCutting.Identity.Attributes
{
    public class AuthorizeClaims : TypeFilterAttribute
    {
        public AuthorizeClaims(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
        {
            Arguments = new object[] {new Claim(claimName, claimValue)};
        }
    }
}