using System.Linq;
using System.Web.Mvc;
using ActiveDirectory.Actions;
using ActiveDirectory.Models;
namespace ActiveDirectory.Controllers
{
    [Compress]
    public class DomainUserController : Controller
    {
        private readonly DBContext _db = new DBContext();
        private readonly DomainUser _domainUser = new DomainUser();
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;
        //    Logger.AddLog(filterContext.Exception);
        //    filterContext.Result = RedirectToAction("Index", "ErrorHandler");
        //    filterContext.Result = new ViewResult
        //    {
        //        ViewName = "~/Views/ErrorHandler/Index.cshtml"
        //    };
        //}
        public DomainUserController()
        {
            Login.IsUserLogin();
        }
        [Compress]
        public ActionResult Index(int? number)
        {
            if (number != null || number != 0) TempData["number"] = number;
            if (Request.IsAjaxRequest()) return PartialView();
            return View();
        }
        [Compress]
        public ActionResult UserDetails(string id)
        {
            if (Request.IsAjaxRequest())
            {
                var findUser = UserFinder.FindUser(id, true);
                return PartialView(findUser);
            }
            return RedirectToAction("Index");
        }
        [Compress]
        public ActionResult PendingUser()
        {
            Login.IsAdmin(true);
            if (Request.IsAjaxRequest()) return PartialView();
            return View();
        }
        public ActionResult Create(int? id)
        {
            ViewBag.company = _domainUser.GetCompany();
            if (Request.IsAjaxRequest())
            {
                if (Login.IsAdmin())
                {
                    if (id != null)
                        return PartialView(UserFinder.FindUser(id));
                    return PartialView();
                }
            }
            else
            {
                if (Login.IsAdmin())
                {
                    if (id != null)
                        return View(UserFinder.FindUser(id));
                    return View();
                }
            }
            TempData["layout"] = "sss";
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserProperties user)
        {
            if (user.ID == 0)
            {
                if (Login.IsAdmin())
                {
                    _domainUser.CreateIfIsAdmin(user);
                    return Json("Done", JsonRequestBehavior.AllowGet);
                }
                _domainUser.CreateIfIsNotAdmin(user);
                TempData["olusturuldu"] = "asdasdsa";
                ViewBag.company = _domainUser.GetCompany();
                TempData["layout"] = "sss";
                return RedirectToAction("Create");
            }
            _domainUser.ApproveUser(user);
            TempData["done"] = "done";
            return RedirectToAction("DoneResult", new { user.ID});
        }
        public PartialViewResult Islem(int id)
        {
            Login.IsAdmin(true);
            var ss = UserFinder.FindUser(id);
            return PartialView(ss);
        }
        public ActionResult DoneResult(string id)
        {
            var ss = _db.UserInformations.FirstOrDefault(x => x.EmployeNumber == id);
            TempData["11"] = Request.IsAjaxRequest();
            return PartialView(ss);
        }
    }
}