using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using ActiveDirectory.Actions;
using ActiveDirectory.Models;

namespace ActiveDirectory.Controllers
{
    public class SettingsController : Controller
    {
        private readonly DBContext db = new DBContext();

        public SettingsController()
        {
            Login.IsUserLogin();
            Login.IsAdmin();
        }

        // GET: Settings
        public ActionResult Settings()
        {
            return View(db.Settings.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveSettings(Settings Settings)
        {
            db.Set<Settings>().AddOrUpdate(Settings);
            db.SaveChanges();
            return Json("Tamam");
        }
    }
}