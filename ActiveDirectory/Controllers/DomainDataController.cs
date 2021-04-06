using System.Web.Mvc;
using ActiveDirectory.Actions;
using ActiveDirectory.Models;
namespace ActiveDirectory.Controllers
{
    public class DomainDataController : Controller
    {
        private readonly DBContext _db = new DBContext();
        private readonly DomainUser _domainUser = new DomainUser();
        public DomainDataController()
        {
            //if (!Request.IsAjaxRequest())
            //{
            //    //Hata
            //}
        }
        [Compress]
        public JsonResult GetUserList()
        {
            var number = 0;
            if (TempData["number"] != null) number = (int)TempData["number"];
            return Json(number != 0 ? _domainUser.ListOfUsers(number) : _domainUser.ListOfUsers(), JsonRequestBehavior.AllowGet);
        }
        [Compress]
        public JsonResult PendingUserList()
        {
            Login.IsAdmin(true);
            if (Request.IsAjaxRequest())
                return Json(_domainUser.ApproveList(), JsonRequestBehavior.AllowGet);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MakeDisable(string Value)
        {
            Login.IsAdmin(true);
            if (!Request.IsAjaxRequest()) return Json(false, JsonRequestBehavior.AllowGet);
            _domainUser.DisableUser(Value);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string Value)
        {
            Login.IsAdmin(true);
            if (!Request.IsAjaxRequest()) return Json(false, JsonRequestBehavior.AllowGet);
            _domainUser.Delete(Value);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [Compress]
        public JsonResult GetCity(string ou)
        {
            return Json(_domainUser.GetCity(ou), JsonRequestBehavior.AllowGet);
        }
        [Compress]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DoneResults(string Mail, string Content)
        {
            SendMail.MailSender(Content, Mail);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [Compress]
        public JsonResult GetUser(int id)
        {
            Login.IsAdmin(true);
            return Json(UserFinder.FindUser(id), JsonRequestBehavior.AllowGet);
        }
        [Compress]
        public JsonResult IslemYap(int id, int durum)
        {
            Login.IsAdmin(true);
            return Json(_domainUser.IslemYap(id, durum), JsonRequestBehavior.AllowGet);
        }
        [Compress]
        public JsonResult Manager(string City, string Company)
        {
            return Json(UserList.Manager(Company, City), JsonRequestBehavior.AllowGet);
        }
    }
}