using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Services;

namespace TestCase2PHE.Controllers
{
    public class UserController : Controller
    {

        private UserServices _userServices;

        public UserController()
        {
            PHEDbContext dbContext = new PHEDbContext(); // Ganti dengan cara mendapatkan instance yang sesuai
            _userServices = new UserServices(dbContext);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        // CREATE
        [HttpPost]
        public ActionResult Register(User registerDto)
        {
            if (ModelState.IsValid)
            {
                _userServices.RegisterAccount(registerDto);
                return RedirectToAction("Index", "User"); // Redirect to employee list or any other action
            }

            // Handle validation errors
            return View(); // Return to the form with errors
        }


        [HttpGet]
        public ActionResult Index()
        {
            var employees = _userServices.GetAllUsers();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                if (_userServices.ValidatePassword(username, password))
                {
                    // Get the user by username
                    var user = _userServices.GetUserByUsername(username);

                    // Get the user's roles
                    var roles = _userServices.GetRoles(user.Guid);

                    if (roles.Any())
                    {
                        // Assuming you want to use the first role in the list for the JWT token
                        var role = roles.First();

                        // Generate JWT token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes("dfgjdfhgndjfhgbdfhjgbhdfbghdfghdfgh23g4y23g4y12gy3ug12h3g21uy3g21ug32167gydfsgfhjsdgfuyhsdgf76t2316712yu3g12hgdwfhjwsgfsdf"); // Replace with your secret key
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, role)  // Use the retrieved role directly
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(15), // Token expiration time
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);

                        // Send JWT token to the client
                        Response.Cookies.Add(new HttpCookie("JWTToken", jwtToken));

                        // Redirect to the dashboard or any other authenticated page
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Handle the case when no roles are found for the user
                        ModelState.AddModelError("", "No roles found for the user.");
                    }
                }
                else
                {
                    // Handle invalid login
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // Return to the login form with errors
            return View("Login");
        }

        public ActionResult Logout()
        {
            // Redirect to the login page or any other desired page
            return RedirectToAction("Login", "User");
        }
    }
}