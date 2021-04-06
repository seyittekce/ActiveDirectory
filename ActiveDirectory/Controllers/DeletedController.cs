using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveDirectory.Models;
using WebGrease.Css.Extensions;

namespace ActiveDirectory.Controllers
{
    public class DeletedController : Controller
    {
        DBContext _db = new DBContext();
        // GET: Deleted
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            List<UserProperties> Prop=new List<UserProperties>();
            var ListOfDeleted = _db.DeletedPersons.ToList();
            foreach (var item in ListOfDeleted)
            {
                byte[] bytes = item.Data.Split('-').ToArray();

                byte us = Convert.ToByte();
                    DeletedPerson.BinaryDeserialize();
            }
        }
    }
}