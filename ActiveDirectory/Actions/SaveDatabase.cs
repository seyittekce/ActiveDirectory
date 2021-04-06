using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class SaveDatabase
    {
        private readonly AddDomainUser _addDomainUser = new AddDomainUser();
        private readonly DBContext _db = new DBContext();
        private readonly UserActions _userActions = new UserActions();

        public SaveDatabase()
        {
            Login.IsUserLogin();
        }

        public string OnayaGonder(UserProperties userInfo, bool ss)
        {
            try
            {
                var Ex = UserFinder.FindUser(userInfo.Group);
                userInfo.Stat = UserProperties.Status.Waiting;
                userInfo.AddedBy = Login.GetLoginUser.SamAccountName;
                userInfo.AddedByTime = DateTime.Now.ToString();
                userInfo.Password = _userActions.RandomPassword();
                userInfo.LogonName = _userActions.LogonName(userInfo.FirstName, userInfo.LastName);
                userInfo.DisplayName = _userActions.FullDisplayName(userInfo.FirstName, userInfo.LastName);
                userInfo.Name = _userActions.CheckUserName(userInfo);
                userInfo.JobTitle = Ex.JobTitle;
                userInfo.Office = Ex.Office;
                userInfo.Street = Ex.Street;
                userInfo.ZipCode = Ex.ZipCode;
                userInfo.Department = Ex.Department;
                userInfo.Company = Ex.Company;
                userInfo.City = Ex.City;
                userInfo.ZipCode = Ex.ZipCode;
                userInfo.Manager = Ex.Manager;
                userInfo.FaxNumber = Ex.FaxNumber;
                userInfo.WebPage = Ex.WebPage;
                userInfo.TelephoneNumber = Ex.VoiceTelephoneNumber;
                if (ss)
                {
                    userInfo.Stat = UserProperties.Status.Comfirmed;
                    _db.Set<UserProperties>().AddOrUpdate(userInfo);
                    _db.SaveChanges();
                }
                else
                {
                    userInfo.Stat = UserProperties.Status.Waiting;
                    _db.UserInformations.Add(userInfo);
                    _db.SaveChanges();
                }

                Logger.AddLog(userInfo.LogonName + " adlı kullanıcı onaya gönderildi.");
                return "Başarılı";
            }
            catch (DbEntityValidationException ex)
            {
                _ = ex.EntityValidationErrors;
                Logger.AddLog(ex);
                return "Hata";
            }
        }

        public string OnayRed(int ID)
        {
            try
            {
                UserProperties Find = _db.UserInformations.Find(ID);
                if (Find != null)
                {
                    _db.UserInformations.Remove(Find);
                    _db.SaveChanges();
                    Logger.AddLog(Find.LogonName + " adlı kullanıcı red edildi ve silindi.");
                }

                return "Başarılı";
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                return "Hata";
            }
        }

        public string DurumHatalı(int ID)
        {
            try
            {
                UserProperties Find = _db.UserInformations.Find(ID);
                if (Find != null)
                {
                    var Ex = UserFinder.FindUser(Find.Group);
                    Find.Stat = UserProperties.Status.Waiting;
                    Find.OperationBy = Login.GetLoginUser.SamAccountName;
                    Find.OperationByTime = DateTime.Now.ToString();
                    Find.JobTitle = Ex.JobTitle;
                    Find.Office = Ex.Office;
                    _db.SaveChanges();
                    Logger.AddLog(Find.LogonName + " adlı kullanıcı hatalı.");
                    return "Tamam";
                }

                return "Hatalı";
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                return "Hatalı";
            }
        }

        public void Onaylandı(int ID)
        {
            try
            {
                UserProperties Find = _db.UserInformations.Find(ID);
                if (Find != null)
                {
                    _addDomainUser.CreateUser(Find.ID);
                    Find.Stat = UserProperties.Status.Comfirmed;
                    Find.OperationBy = Login.GetLoginUser.SamAccountName;
                    Find.OperationByTime = DateTime.Now.ToString();
                    _db.Set<UserProperties>().AddOrUpdate(Find);
                    _db.SaveChanges();
                    Logger.AddLog(Find.LogonName + " adlı kullanıcı onaylandı.");
                }
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
            }
        }
    }
}