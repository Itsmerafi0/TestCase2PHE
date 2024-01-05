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
    public class VendorController : Controller
    {
        private readonly VendorServices _vendorService;

        public VendorController()
        {
            PHEDbContext dbContext = new PHEDbContext();
            _vendorService = new VendorServices(dbContext);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (Vendor vendorDto)
        {
            if(ModelState.IsValid)
            {
                var createdVendorDto = _vendorService.CreateVendor(vendorDto);

                if(createdVendorDto != null)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(vendorDto);
        }

        public ActionResult Index()
        {
            var vendors = _vendorService.GetAllVendor();
            return View(vendors); // Ganti dengan view yang sesuai
        }
    }
}