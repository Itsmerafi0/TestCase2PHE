using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Services;

namespace TestCase2PHE.Controllers
{
    public class ApprovalController : Controller
    {
        private readonly ApprovalServices _approvalServices;
        public ApprovalController()
        {
            PHEDbContext dbContext = new PHEDbContext();
            _approvalServices = new ApprovalServices(dbContext);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // CREATE
        [HttpPost]
        public ActionResult Create(Approval ApprovalDto)
        {
            if (ModelState.IsValid)
            {
                var createdApprovalDto = _approvalServices.CreateApproval(ApprovalDto);

                if (createdApprovalDto != null)
                {
                    return RedirectToAction("Index"); // Ganti dengan action yang sesuai
                }
            }

            // Handle validation errors
            return View(ApprovalDto); // Ganti dengan view yang sesuai
        }

        [HttpGet]
        public ActionResult TableApproval()
        {
            var tableApproval = _approvalServices.GetTableApproval();
            return View(tableApproval);
        }
    }
}