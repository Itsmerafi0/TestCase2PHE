using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Services;

namespace TestCase2PHE.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyServices _companyServices;

        public CompanyController()
        {
            PHEDbContext dbContext = new PHEDbContext();
            _companyServices = new CompanyServices(dbContext);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                var createdCompanyDto = _companyServices.CreateCompany(company);
                if (createdCompanyDto != null)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(company);
        }

        public ActionResult Index()
        {

            var companies = _companyServices.GetAllCompanies();
            return View(companies);
        }

        [HttpGet]
        public ActionResult PendingCompanies()
        {
            var pendingCompanies = _companyServices.GetPendingCompanies();
            return View(pendingCompanies);
        }

        [HttpGet]
        public ActionResult ProccessCompanies()
        {
            var pendingCompanies = _companyServices.GetProccessCompanies();
            return View(pendingCompanies);
        }

        [HttpPost]
        public ActionResult CompanyAccept(string guid)
        {
            var result = _companyServices.CompanyAccept(guid);

            // Handle the result, for example, redirect to Index
            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle other results, e.g., display an error message
                ViewBag.ErrorMessage = "Failed to accept vendor.";
                return View("Error"); // Create an "Error" view or use a standard error view
            }
        }

        [HttpPost]
        public ActionResult CompanyApprove(string guid)
        {
            var result = _companyServices.CompanyApprove(guid);

            // Handle the result, for example, redirect to Index
            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle other results, e.g., display an error message
                ViewBag.ErrorMessage = "Failed to accept vendor.";
                return View("Error"); // Create an "Error" view or use a standard error view
            }
        }
        private string GetRoleGuidFromCookie()
        {
            // Mendapatkan nilai dari cookie "UserRole"
            var userRoleCookie = HttpContext.Request.Cookies["UserRole"];
            return userRoleCookie?.Value;
        }

        public ActionResult GetCompanies()
        {
            var companies = _companyServices.GetCompanies();
            return Json(companies, JsonRequestBehavior.AllowGet);
        }

    }
}