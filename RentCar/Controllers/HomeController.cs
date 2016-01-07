using System.Web.Mvc;

namespace RentCar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "RentCar";
            return View();
        }        
    }
}