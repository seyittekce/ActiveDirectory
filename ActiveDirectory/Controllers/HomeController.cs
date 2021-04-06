using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ActiveDirectory.Actions;
using ActiveDirectory.Models;

namespace ActiveDirectory.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext Db = new DBContext();

        public HomeController()
        {
            Login.IsAdmin();
            Login.IsUserLogin();
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest()) return PartialView();
            return View();
        }
        //public JsonResult LastLogons()
        //{

        //}
        public JsonResult DevreDisi()
        {
            List<UserProperties> aa = UserList.ListofUser();
            var aas = aa.Where(x => (bool) x.IsDisabled == false && x.FirstName != null).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.LogonName,
                x.TelephoneNumber
            }).Reverse().ToList().Take(5);
            return Json(aas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Enson()
        {
            List<UserProperties> aa = UserList.ListofUser();
            var sırala = from Sırala in aa orderby Sırala.Lastlogon select Sırala;
            var aas1 = sırala.Where(x => (bool) x.IsDisabled == false).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.LogonName,
                x.Lastlogon
            }).Reverse().ToList().Take(5);
            return Json(aas1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Chart1()
        {
            int[] NewArray2 = new int[12];
            for (var i = 0; i < NewArray2.Length; i++) NewArray2[i] = 0;

            var GetMont = 0;
            var Count = 0;
            foreach (var item in Db.UserInformations.ToList())
            {
                var aa = Convert.ToDateTime(item.AddedByTime);
                GetMont = aa.Month;
                Label:
                if (GetMont == aa.Month)
                {
                    Count++;
                    NewArray2[GetMont - 1] = Count;
                }
                else
                {
                    NewArray2[GetMont - 1] = Count;
                    GetMont = aa.Month;
                    Count = 0;
                    goto Label;
                }
            }

            return Json(NewArray2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisableUserCount()
        {
            var a = 0;
            var b = 0;
            var c = 0;
            foreach (UserProperties item in UserList.ListofUser())
            {
                var Nulltime = Convert.ToDateTime("1.01.0001 00:00:00");
                var dt = Convert.ToDateTime(item.Lastlogon);
                if (dt != Nulltime)
                {
                    var aa = DateTime.Now - Convert.ToDateTime(item.Lastlogon);
                    if (aa.Days >= 30 && aa.Days <= 89) a++;
                    if (aa.Days == 90 && aa.Days <= 179) b++;
                    if (aa.Days >= 180) c++;
                }
            }

            int[] dd = {a, b, c};
            return Json(dd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OnayBekleyen()
        {
            IQueryable<UserProperties> aa = (from a in Db.UserInformations
                where a.Stat == UserProperties.Status.Waiting
                orderby a.AddedByTime descending
                select a).Take(5);
            var aas = aa.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.LogonName,
                x.TelephoneNumber,
                button =
                    "<button class='btn btn-light-success font-weight-bolder font-size-sm' onclick='RenderCratesss(" +
                    x.ID + ")'" + ">Detaylar</button>"
            }).ToList();
            ;
            return Json(aas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalUser()
        {
            return Json(UserList.ListofUser().Count, JsonRequestBehavior.AllowGet);
        }
    }
}