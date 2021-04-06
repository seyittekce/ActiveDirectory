using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class UserActions
    {
        private readonly DBContext _db = new DBContext();

        public string RandomPassword()
        {
            Random rastgele = new Random();
            string harfler = "ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyz123456789_!?*+/{}.";
            string uret = "";
            Label:
            for (var i = 0; i < 8; i++) uret += harfler[rastgele.Next(harfler.Length)];
            UserProperties ss = _db.UserInformations.Where(x => x.Password == uret).FirstOrDefault();
            if (ss == null) return uret + ".";
            goto Label;
        }

        public string FullDisplayName(string FirstName, string LastName)
        {
            return FirstName + " " + LastName;
        }

        private string StringReplace(string ReplaceText)
        {
            string Tam = string.Empty;
            ReplaceText = ReplaceText.ToLower();
            foreach (var uk in ReplaceText.ToCharArray())
            {
                string text = uk.ToString();
                text = text.Replace("İ", "I");
                text = text.Replace("ı", "i");
                text = text.Replace("Ğ", "G");
                text = text.Replace("ğ", "g");
                text = text.Replace("Ö", "O");
                text = text.Replace("ö", "o");
                text = text.Replace("Ü", "U");
                text = text.Replace("ü", "u");
                text = text.Replace("Ş", "S");
                text = text.Replace("ş", "s");
                text = text.Replace("Ç", "C");
                text = text.Replace("ç", "c");
                text = text.Replace(" ", "");
                Tam += text;
            }

            return Tam;
        }

        private bool isExistDB(string value)
        {
            var jj = _db.UserInformations.Where(x => x.LogonName == value).FirstOrDefault();
            if (jj != null) return true;

            return false;
        }

        private bool isExistAD(string value)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            var aa = UserPrincipal.FindByIdentity(ctx, value);
            if (aa != null) return true;

            return false;
        }

        public string LogonName(string FirstName, string LastName)
        {
            var Count = 1;
            var a = 1;
            LastName = StringReplace(LastName);
            FirstName = FirstName.Split(' ').ToArray()[0];
            FirstName = StringReplace(FirstName);
            string LogonName = FirstName + LastName.Substring(0, Count);
            Label:
            var AD = isExistAD(LogonName);
            var DB = isExistDB(LogonName);
            if (AD && DB)
            {
                if (Count == LastName.Length)
                {
                    LogonName = FirstName + LastName.Substring(0, Count) + "" + a;
                    a++;
                }

                if (Count <= LastName.Length)
                {
                    LogonName = FirstName + LastName.Substring(0, Count);
                    Count++;
                    goto Label;
                }
            }

            if (AD && !DB)
            {
                if (Count == LastName.Length)
                {
                    LogonName = FirstName + LastName.Substring(0, Count) + "" + a;
                    a++;
                }

                if (Count <= LastName.Length)
                {
                    LogonName = FirstName + LastName.Substring(0, Count);
                    Count++;
                    goto Label;
                }
            }

            return LogonName;
        }

        public string CheckUserName(UserProperties kul)
        {
            var aa = isExistAD(kul.DisplayName);
            if (aa) return kul.DisplayName + "(" + kul.Company + ")";

            return kul.DisplayName;
        }
    }
}