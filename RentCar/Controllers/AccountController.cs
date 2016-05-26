using RentCar.Models;
using RentCar.Services;
using System.Web.Mvc;

namespace RentCar.Controllers
{
    public class AccountController : Controller
    {
        readonly AccountService _service = new AccountService();

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (_service.CheckLogin(user))
            {
                return RedirectToAction("LoggedIn");
            }
            ModelState.AddModelError("", "Login lub hasło jest niepoprawne");
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (_service.CheckLoggedIn())
            {
                return View();
            }
            RedirectToAction("Login");
            return View();
        }

        // GET: Registration
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid && _service.CheckRegister(user))
            {
                _service.Register(user);
                ModelState.Clear();
                ViewBag.Message = "Successfully Registration Done";
            }
            return View(user);
        }
    }
}