using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace TestCase2PHE
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configure authentication
            ConfigureAuth();
        }

        private void ConfigureAuth()
        {
            // Enable cookie authentication
            FormsAuthentication.Initialize();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var identity = HttpContext.Current.User.Identity;

                if (identity is FormsIdentity formsIdentity)
                {
                    // Get roles and roleGuid from your application logic
                    var roles = GetRolesForUser(formsIdentity.Name);
                    var roleGuid = GetRoleGuidForUser(formsIdentity.Name);

                    // Debug statements
                    System.Diagnostics.Debug.WriteLine($"Roles: {string.Join(",", roles)}");
                    System.Diagnostics.Debug.WriteLine($"RoleGuid: {roleGuid}");

                    // Concatenate roles and roleGuid into a single string
                    var userData = $"{string.Join(",", roles)}|{roleGuid}";

                    var ticket = new FormsAuthenticationTicket(
                        1,                        // Version, must be 1
                        formsIdentity.Name,       // User identity name
                        DateTime.Now,             // Issue time
                        DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),  // Expiration time
                        false,                    // Persistent cookie
                        userData,                 // Additional user data (roles and roleGuid)
                        FormsAuthentication.FormsCookiePath
                    );

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL,
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        Expires = ticket.Expiration
                    };

                    HttpContext.Current.Response.Cookies.Add(cookie);

                    var principal = new GenericPrincipal(formsIdentity, roles);
                    HttpContext.Current.User = principal;
                }
            }
        }



        private string[] GetRolesForUser(string username)
        {
            // Implement logic to get roles for the user based on username
            // Replace this with your actual logic to fetch roles from your data source

            if (username == "admin")
            {
                return new string[] { "Admin" };
            }
            else if (username == "user")
            {
                return new string[] { "User" };
            }
            else if (username == "manager")
            {
                return new string[] { "Manager" };
            }
            else
            {
                return new string[] { };
            }
        }

        private string GetRoleGuidForUser(string username)
        {
            // Implement logic to get roleGuid for the user based on username
            // Replace this with your actual logic to fetch roleGuid from your data source

            if (username == "admin")
            {
                return "Admin";
            }
            else if (username == "user")
            {
                return "User";
            }
            else if (username == "manager")
            {
                return "Manager";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}