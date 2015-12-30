using RentCar.Models;
using System.Linq;
using System.Web.Mvc;

namespace RentCar.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext = new MyContext();

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var checkUser = myContext.Users.Single(u => u.UserLogin == user.UserLogin && u.Password == user.Password);
            if (checkUser != null)
            {
                Session["UserId"] = user.UserId.ToString();
                Session["Name"] = user.Name;
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
            if (Session["UserId"] != null)
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
            if (ModelState.IsValid)
            {
                //Jeszcze trza tu spradzic czy kajsik pieron nie je już we bazie 
                myContext.Users.Add(user);
                myContext.SaveChanges();
                ModelState.Clear();
                user = null;
                ViewBag.Message = "Successfully Registration Done";
            }
            return View(user);
        }
    }
}