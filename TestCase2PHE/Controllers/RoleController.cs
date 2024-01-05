using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Services;

namespace TestCase2PHE.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleServices _roleService;

        public RoleController()
        {
            PHEDbContext dbContext = new PHEDbContext(); // Ganti dengan cara mendapatkan instance yang sesuai
            _roleService = new RoleServices(dbContext);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // CREATE
        [HttpPost]
        public ActionResult Create(Role roleDto)
        {
            if (ModelState.IsValid)
            {
                var createdRoleDto = _roleService.CreateRole(roleDto);

                if (createdRoleDto != null)
                {
                    return RedirectToAction("Index"); // Ganti dengan action yang sesuai
                }
            }

            // Handle validation errors
            return View(roleDto); // Ganti dengan view yang sesuai
        }

        [HttpGet]
        public ActionResult Edit(string guid)
        {
            var role = _roleService.GetRoleByGuid(guid);

            if (role != null)
            {
                return View(role); // Ganti dengan view yang sesuai
            }

            return HttpNotFound();
        }

        public ActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return View(roles); // Ganti dengan view yang sesuai
        }

        // UPDATE
        [HttpPost]
        public ActionResult Edit(Role updatedRole)
        {
            if (ModelState.IsValid)
            {
                _roleService.UpdateRole(updatedRole);
                return RedirectToAction("Index"); // Ganti dengan action yang sesuai
            }

            // Handle validation errors
            return View(updatedRole); // Ganti dengan view yang sesuai
        }

        [HttpGet]
        public ActionResult Delete(string guid)
        {
            var role = _roleService.GetRoleByGuid(guid);

            if (role != null)
            {
                return View(role); // Ganti dengan view yang sesuai
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Deletes(string guid)
        {
            _roleService.DeleteRole(guid);
            return RedirectToAction("Index"); // Ganti dengan action yang sesuai
        }


    }
}