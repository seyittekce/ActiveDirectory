using System.DirectoryServices.AccountManagement;
using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public static class UserFinder
    {
        private static readonly DBContext db = new DBContext();

        public static UserPrincipalEx FindUser(string LogonName)
        {
            PrincipalContext AD = new PrincipalContext(ContextType.Domain);
            UserPrincipalEx result = UserPrincipalEx.FindByIdentity(AD, LogonName);
            if (result != null)
                return result;
            return null;
        }

        public static UserProperties FindUser(int? ID)
        {
            return db.UserInformations.Find(ID);
        }

        public static UserProperties FindUser(string Value, bool Finder)
        {
            PrincipalContext AD = new PrincipalContext(ContextType.Domain);
            UserPrincipalEx result = UserPrincipalEx.FindByIdentity(AD, Value);
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
            user.Groups = result.GetGroups().ToList().Select(x => x.Name).ToArray();
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
            return user;
        }
    }
}