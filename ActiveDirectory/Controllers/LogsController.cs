using System.Linq;
using System.Web.Mvc;
using ActiveDirectory.Actions;
using ActiveDirectory.Models;

namespace ActiveDirectory.Controllers
{
    public class LogsController : Controller
    {
        private readonly DBContext db = new ();

        public LogsController()
        {
            Login.IsAdmin();
        }

        // GET: Logs
        public ActionResult Logs()
        {
            return View();
        }

        public JsonResult GetLogs()
        {
            return Json(db.StandartLogs.Select(x => new
            {
                x.Action,
                x.Kim,
                Date = x.Date.ToString()
            }).ToList(), JsonRequestBehavior.AllowGet);
            ;
        }

        public ActionResult ErrorLog()
        {
            return View();
        }

        public JsonResult ErrorList()
        {
            if (Request.IsAjaxRequest())
                return Json(
                    db.ErrorLogs.Select(x => new
                    {
                        x.ID,
                        x.Message,
                        Date = x.Date.ToString(),
                        Buttons = "<Button class='btn btn-info' onclick='RenderCrateSS(" + x.ID + ")'>Göster</Button>"
                    }), JsonRequestBehavior.AllowGet);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowErrorLog(int ID)
        {
            if (Request.IsAjaxRequest()) return PartialView(db.ErrorLogs.Find(ID));
            return RedirectToAction("Error", "Error");
        }
    }
}