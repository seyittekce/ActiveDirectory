using System.Web.Mvc;

namespace ActiveDirectory.Controllers
{
    [RoutePrefix("Login")]
    public class LoginController : Controller
    {
        // GET: Login
        [Route("~")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Name, string Password, string domain)
        {
            Actions.Login.LoginUser(Name, Password, domain);
            TempData["hata"] = "Hata";
            return View();
        }

        public ActionResult SignOut()
        {
            Actions.Login.UserSignOut();
            return View();
        }
    }
}