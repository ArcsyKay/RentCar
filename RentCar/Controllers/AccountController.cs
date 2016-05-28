using RentCar.Models;
using RentCar.Services;
using System.Web.Mvc;
using System.Web.Security;

namespace RentCar.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (_accountService.CheckLogin(user))
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("LoggedIn");
            }
            ModelState.AddModelError("", "Login lub hasło jest niepoprawne");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoggedIn()
        {
            if (_accountService.CheckLoggedIn())
            {
                return View();
            }
            RedirectToAction("Login");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid && !_accountService.CheckRegister(user))
            {
                _accountService.Register(user);
                ModelState.Clear();
                ViewBag.Message = "Successfully Registration Done";
            }
            return View(user);
        }
    }
}