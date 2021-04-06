using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class DomainUser
    {
        private readonly AddDomainUser ADU = new AddDomainUser();
        private readonly DBContext db = new DBContext();
        private readonly GetOus GetOu = new GetOus();
        private readonly SaveDatabase SD = new SaveDatabase();

        public DomainUser()
        {
            Login.IsUserLogin();
        }

        public object ListOfUsers()
        {
            Login.IsAdmin(true);
            List<UserProperties> aa = UserList.ListofUser();
            var aas = aa.Select(x => new
            {
                x.FirstName,
                x.Mail,
                x.LastName,
                x.LogonName,
                x.JobTitle,
                x.Department,
                x.Manager,
                x.DistinguishedName,
                lastlogon = Convert.ToDateTime(x.Lastlogon).ToString()
            }).ToList().Where(x => x.FirstName != null);
            ;
            return aas;
        }

        public object ListOfUsers(int? Number)
        {
            Login.IsAdmin(true);
            List<UserProperties> aa = new List<UserProperties>();
            foreach (UserProperties item in UserList.ListofUser())
            {
                var Nulltime = Convert.ToDateTime("1.01.0001 00:00:00");
                var dt = Convert.ToDateTime(item.Lastlogon);
                if (dt != Nulltime)
                {
                    var aab = DateTime.Now - Convert.ToDateTime(item.Lastlogon);
                    if (aab.Days >= 30 && aab.Days <= 89 && Number == 30) aa.Add(item);
                    if (aab.Days == 90 && aab.Days <= 179 && Number == 90) aa.Add(item);
                    if (aab.Days >= 180 && Number == 180) aa.Add(item);
                }
            }

            if (aa.Count != 0)
            {
                var aas = aa.Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.LogonName,
                    x.JobTitle,
                    x.Department,
                    x.Manager,
                    x.DistinguishedName,
                    lastlogon = Convert.ToDateTime(x.Lastlogon).ToString(),
                    button = "<a title='Göster' style='cursor:pointer' onclick=Details('" + x.LogonName +
                             "')><i class='fas fa-eye'></i></a>"
                }).ToList();
                return aas;
            }

            return null;
        }

        public object ApproveList()
        {
            Login.IsAdmin(true);
            List<UserProperties> listUser =
                db.UserInformations.Where(x => x.Stat == UserProperties.Status.Comfirmed).ToList();
            var aas = listUser.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Auth,
                x.LogonName,
                x.JobTitle,
                x.Department,
                x.Manager,
                EklenenDate = x.AddedByTime,
                x.ReturnStat
            }).ToList();
            return aas;
        }

        public List<Ou> GetCity(string OUs)
        {
            return GetOu.City(OUs);
        }

        public ArrayList GetCompany()
        {
            return GetOu.Company();
        }

        public void CreateIfIsAdmin(UserProperties userInfo)
        {
            Login.IsAdmin(true);
            userInfo.Stat = UserProperties.Status.Comfirmed;
            SD.OnayaGonder(userInfo, true);
            ADU.CreateUser(userInfo.ID);
        }

        public void CreateIfIsNotAdmin(UserProperties userInfo)
        {
            SD.OnayaGonder(userInfo, false);
        }

        public void ApproveUser(UserProperties userInfo)
        {
            Login.IsAdmin(true);
            userInfo.Stat = UserProperties.Status.Comfirmed;
            SD.OnayaGonder(userInfo, true);
            ADU.CreateUser(userInfo.ID);
        }

        public int[] IslemYap(int ID, int durum)
        {
            Login.IsAdmin(true);
            int[] a = new int[2];
            a[0] = ID;
            if (durum == 1)
            {
                SD.Onaylandı(ID);
                a[1] = 1;
            }

            if (durum == 2)
            {
                SD.OnayRed(ID);
                a[1] = 2;
            }

            if (durum == 3)
            {
                SD.DurumHatalı(ID);
                a[1] = 3;
            }

            return a;
        }

        public void DisableUser(string Value)
        {
            Login.IsAdmin(true);
            ADU.DisableUser(Value);
        }

        public void Delete(string Value)
        {
            ADU.DeleteUser(Value);
        }
    }
}