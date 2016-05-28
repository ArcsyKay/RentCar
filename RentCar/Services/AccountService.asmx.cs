using System.Linq;
using System.Web.Services;
using RentCar.Models;

namespace RentCar.Services
{
    /// <summary>
    /// Summary description for AccountService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AccountService : WebService
    {
        private readonly MyContext _myContext = new MyContext();

        [WebMethod]
        public bool CheckLogin(User user)
        {
            if (_myContext.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password) != null)
            {
                Session["UserId"] = user.UserId.ToString();
                Session["Name"] = user.Name;
                return true;
            }
            return false;
        }

        [WebMethod]

        public bool CheckLoggedIn()
        {
            return Session["UserId"] != null;
        }

        [WebMethod]

        public bool CheckRegister(User user)
        {
            var myBool = _myContext.Users.Any(u => u.Email == user.Email || u.Email == user.Email);
            return myBool;
        }

        [WebMethod]

        public void Register(User user)
        {
            _myContext.Users.Add(user);
            _myContext.SaveChanges();
        }
    }
}
