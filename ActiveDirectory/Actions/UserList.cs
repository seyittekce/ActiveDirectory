using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class UserList
    {
        public static List<Manager> Manager(string Company, string City)
        {
            List<Manager> mngr = new List<Manager>();
            foreach (var item in ListofUser())
                if (item.DistinguishedName.Contains(Company) && item.DistinguishedName.Contains(City))
                {
                    Manager mn = new Manager();
                    mn.Display = item.DisplayName + " | " + item.JobTitle;
                    mn.DName = item.DistinguishedName;
                    mngr.Add(mn);
                }

            return mngr;
        }

        /// <summary>
        ///     Raw data is going on there
        /// </summary>
        /// <returns></returns>
        public static List<UserProperties> ListofUser()
        {
            try
            {
                List<UserProperties> Bilgiler = new List<UserProperties>();
                PrincipalContext AD = new PrincipalContext(ContextType.Domain);
                UserPrincipalEx u = new UserPrincipalEx(AD);
                PrincipalSearcher search = new PrincipalSearcher(u);
                foreach (UserPrincipalEx result in search.FindAll())
                {
                    UserProperties user = new UserProperties();
                    user.City = result.City;
                    user.Company = result.Company;
                    user.Department = result.Department;
                    user.DisplayName = result.DisplayName;
                    user.DistinguishedName = result.DistinguishedName;
                    user.EmployeNumber = (string) result.EmployeeNumber;
                    user.FaxNumber = result.FaxNumber;
                    user.FirstName = result.GivenName;
                    user.LastName = result.secondname;
                    user.LogonName = result.SamAccountName;
                    user.Name = result.Name;
                    user.Groups = result.GetGroups().Select(x => x.Name).ToArray();
                    user.IsDisabled = result.Enabled;
                    user.JobTitle = result.JobTitle;
                    user.Lastlogon = result.RealLastLogons;
                    user.Mail = result.EmailAddress;
                    user.Manager = result.Manager;
                    user.Office = result.Office;
                    user.OtherPhoneNumber = result.otherTelephoneNumber;
                    user.Street = result.Street;
                    user.WebPage = result.WebPage;
                    user.TelephoneNumber = result.VoiceTelephoneNumber;
                    user.ZipCode = result.ZipCode;
                    Bilgiler.Add(user);
                }

                return Bilgiler;
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
            }

            return null;
        }
    }
}