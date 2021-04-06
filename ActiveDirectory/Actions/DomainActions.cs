using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class DomainActions
    {
        public static List<string> Domains()
        {
            List<string> aa = new List<string>();
            using (Forest forest = Forest.GetCurrentForest())
            {
                foreach (Domain Domains in forest.Domains)
                {
                    aa.Add(Domains.Name);
                    Domains.Dispose();
                }
            }

            return aa;
        }

        public static string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            return "LDAP://" + de.Properties["defaultNamingContext"][0];
        }

        public static ArrayList Paths()
        {
            DirectoryEntry startingPoint = new DirectoryEntry(GetCurrentDomainPath());
            DirectorySearcher searcher = new DirectorySearcher(startingPoint)
            {
                Filter = "(objectCategory=organizationalUnit)",
                SearchScope = SearchScope.Subtree
            };
            ArrayList Paths = new ArrayList();
            foreach (SearchResult res in searcher.FindAll())
            {
                string a = res.Path.Remove(0, 7);
                Paths.Add(a);
            }

            return Paths;
        }

        public static UserPrincipalEx GetUser(string Identification)
        {
            var Glui = Login.GetLoginUser;
            string[] aa = Glui.Domain.Split('.');
            PrincipalContext context =
                new PrincipalContext(ContextType.Domain, aa[0], Glui.SamAccountName, Glui.Password);
            UserPrincipalEx UP = UserPrincipalEx.FindByIdentity(context, Identification);
            if (UP != null)
                return UP;
            return null;
        }

        private void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}