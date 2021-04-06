using System.Web.Mvc;
using ActiveDirectory.Models;

namespace ActiveDirectory.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int Ex)
        {
            DBContext _db = new ();
            return View(_db.ErrorLogs.Find(Ex));
        }
    }
}