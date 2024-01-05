using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace TestCase2PHE.Utilities
{
    public static class IdentityExtensions
    {
        public static string GetRole(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var roleClaim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            return roleClaim?.Value ?? "N/A";
        }
    }
}