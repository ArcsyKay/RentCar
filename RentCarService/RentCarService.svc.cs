using RentCarService.Models;
using System.Linq;
using System.Web.Services;

namespace RentCarService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RentCarService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RentCarService.svc or RentCarService.svc.cs at the Solution Explorer and start debugging.
    public class RentCarService : WebService, IRentCarService
    {
        private readonly MyContext _myContext = new MyContext();
        public bool CheckLogin(User user)
        {
            if (_myContext.Users.Single(u => u.UserLogin == user.UserLogin && u.Password == user.Password) != null)
            {
                Session["UserId"] = user.UserId.ToString();
                Session["Name"] = user.Name;
                return true;
            }
            return false;
        }

        public bool CheckLoggedIn()
        {
            return Session["UserId"] != null;
        }

        public bool CheckRegister(User user)
        {
            return _myContext.Users.Single(u => u.UserLogin == user.UserLogin || u.Email == user.Email) != null;
        }

        public void Register(User user)
        {
            _myContext.Users.Add(user);
            _myContext.SaveChanges();
        }
    }
}
