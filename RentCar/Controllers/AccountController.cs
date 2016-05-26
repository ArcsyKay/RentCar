using RentCar.Models;
using RentCar.Service;
using System.Linq;
using System.Web.Mvc;

namespace RentCar.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext = new MyContext();
        AccountService service = new AccountService();

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (service.CheckLogin(user))
            {
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Login lub hasło jest niepoprawne");
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (service.CheckLoggedIn())
            {
                return View();
            }
            else
            {
                RedirectToAction("Login");
            }
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
            if (ModelState.IsValid && !service.CheckRegister(user))
            {
                service.Register(user);
                ModelState.Clear();
                ViewBag.Message = "Successfully Registration Done";
            }
            return View(user);
        }
    }
}