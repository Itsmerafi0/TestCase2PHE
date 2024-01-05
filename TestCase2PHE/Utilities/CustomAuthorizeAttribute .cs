using System.Web.Mvc;
using System.Web;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        // Implementasi logika otorisasi Anda di sini, berdasarkan GUID atau apa pun yang sesuai
        var userRoleGuid = GetRoleGuidFromCookie();
        return YourCustomAuthorizationLogic(userRoleGuid);
    }

    private string GetRoleGuidFromCookie()
    {
        // Implementasi untuk mendapatkan nilai dari cookie "UserRole"
        var userRoleCookie = HttpContext.Current.Request.Cookies["UserRole"];
        return userRoleCookie?.Value;
    }

    private bool YourCustomAuthorizationLogic(string userRoleGuid)
    {
        // Implementasi logika otorisasi khusus Anda di sini
        // Misalnya, periksa apakah userRoleGuid sesuai dengan peran "Manager"
        return (userRoleGuid == "Guid_Manager");
    }
}
