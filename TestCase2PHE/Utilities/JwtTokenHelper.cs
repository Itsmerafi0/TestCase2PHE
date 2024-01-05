using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCase2PHE.Utilities
{
    public static class JwtTokenHelper
    {
        public static string GetJwtTokenFromSession(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            return httpContext.Session["JWToken"]?.ToString();
        }

        public static void SetJwtTokenInSession(HttpContextBase httpContext, string jwtToken)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            httpContext.Session["JWToken"] = jwtToken;
        }
    }
}
