using System;
using System.DirectoryServices.AccountManagement;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class AddDomainUser
    {
        private readonly DBContext _db = new DBContext();
        private readonly UserActions _userActions = new UserActions();

        public AddDomainUser()
        {
            Login.IsUserLogin();
        }

        public string DisableUser(string Val)
        {
            try
            {
               
                var userP = DomainActions.GetUser(Val);
                if (userP.Enabled==false)
                {
                    userP.Enabled = true;
                }
                else
                {
                    userP.Enabled = false;
                }
                userP.Save();
                Logger.AddLog(userP.Name + " devre dışı bıraktı");
                return "Başarılı";
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                return "Hata";
            }
        }

        public string DeleteUser(string LogonName)
        {
            try
            {
                var userP = DomainActions.GetUser(LogonName);
                var SerialUser = UserFinder.FindUser(LogonName,true);
                var bb=new DeletedPerson();
                bb.Data =string.Join("-", DeletedPerson.BinarySerialize(SerialUser));
                _db.DeletedPersons.Add(bb);
                _db.SaveChanges();
                Logger.AddLog(userP.Name + " Sildi");
                userP.Delete();
                return "Başarılı";
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                return "Hata";
            }
        }

        public void AddUserToGroup(string AddUserID, string DefaultUserID)
        {
            using PrincipalContext pc = new PrincipalContext(ContextType.Domain);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(pc, DefaultUserID);
            foreach (var grps in userPrincipal.GetGroups())
                if (grps.Name != "Domain Users")
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, grps.Name);
                    group.Members.Add(pc, IdentityType.SamAccountName, AddUserID);
                    group.Save();
                    group.Dispose();
                }

            userPrincipal.Dispose();
        }

        public string FindPath(UserProperties usa)
        {
            foreach (string path in DomainActions.Paths())
                if (path != null)
                    if (path.Contains(usa.Company))
                        if (path.Contains(usa.City))
                            return path;
            return string.Empty;
        }

        public string CreateUser(int ID)
        {
            try
            {
                string[] aa = Login.GetLoginUser.Domain.Split('.');
                var kulbil = _db.UserInformations.Find(ID);
                if (kulbil != null)
                    using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, aa[0], FindPath(kulbil),
                        Login.GetLoginUser.SamAccountName, Login.GetLoginUser.Password))
                    {
                        using (UserPrincipalEx up = new UserPrincipalEx(pc))
                        {
                            up.GivenName = kulbil.FirstName;
                            up.Surname = kulbil.LastName;
                            up.Name = _userActions.CheckUserName(kulbil);
                            up.SamAccountName = kulbil.LogonName;
                            up.SetPassword(kulbil.Password);
                            up.Enabled = true;
                            up.VoiceTelephoneNumber = kulbil.TelephoneNumber;
                            up.EmployeeNumber = kulbil.EmployeNumber;
                            up.DisplayName = kulbil.DisplayName;
                            up.UserPrincipalName = kulbil.LogonName + "@" + Login.GetLoginUser.Domain;
                            up.Company = kulbil.Company;
                            up.Office = kulbil.Office;
                            up.WebPage = kulbil.WebPage;
                            up.FaxNumber = kulbil.FaxNumber;
                            up.Street = kulbil.Street;
                            up.ZipCode = kulbil.ZipCode;
                            up.City = kulbil.City;
                            up.Serialnumber = kulbil.EmployeNumber;
                            up.otherTelephoneNumber = kulbil.OtherPhoneNumber;
                            up.Department = kulbil.Department;
                            up.Manager = kulbil.Manager;
                            up.JobTitle = kulbil.JobTitle;
                            up.Save();
                            Logger.AddLog(up.SamAccountName + " adlı kullanıcı oluşturuldu.");
                            AddUserToGroup(up.SamAccountName, kulbil.Group);
                            up.Dispose();
                        }
                    }

                return "Tamamlandı";
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                return "Hata";
            }
        }
    }
}