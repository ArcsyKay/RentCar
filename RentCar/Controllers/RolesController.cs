using Microsoft.AspNet.Identity;
using RentCar.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RentCar.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                _context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = formCollection["RoleName"]
                });
                _context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(string roleName)
        {
            var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            return View(thisRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(string roleName)
        {
            var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            _context.Roles.Remove(thisRole);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ManageUserRoles()
        {
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            var account = new AccountController();
            account.UserManager.AddToRole(user?.Id, roleName);
            
            ViewBag.ResultMessage = "Role created successfully !";
            
            // prepopulat roles for the view dropdown
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;   

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string userName)
        {            
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
                var account = new AccountController();

                ViewBag.RolesForThisUser = account.UserManager.GetRoles(user?.Id);

                // prepopulat roles for the view dropdown
                var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;            
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            var account = new AccountController();
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));

            if (account.UserManager.IsInRole(user?.Id, roleName))  
            {
                account.UserManager.RemoveFromRole(user?.Id, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
}
