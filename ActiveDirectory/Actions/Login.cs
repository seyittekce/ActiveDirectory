using System;
using System.DirectoryServices.AccountManagement;
using System.Web;
using ActiveDirectory.Models;
namespace ActiveDirectory.Actions
{
    public static class Login
    {
        public static UserPrincipalEx GetLoginUser
        {
            get
            {
                UserPrincipalEx GetLoginUser = (UserPrincipalEx) HttpContext.Current.Session["AdminLogin"];
                if (GetLoginUser == null)
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                        // Will not be called
                        HttpContext.Current.Response.Redirect("~/Login/Login", true);
                }
                else
                {
                    GetLoginUser.Password = HttpContext.Current.Session["pass"].ToString();
                    GetLoginUser.Domain = HttpContext.Current.Session["Domain"].ToString();
                }
                return GetLoginUser;
            }
        }
        public static bool IsUserLogin()
        {
            UserPrincipalEx isLogin = (UserPrincipalEx) HttpContext.Current.Session["AdminLogin"];
            if (isLogin == null)
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    HttpContext.Current.Response.Redirect("~/Login/Login", true);
                return false;
            }
            return true;
        }
        public static bool IsAdmin()
        {
            var IsIt = false;
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                if (GetLoginUser!=null)
                {
                    IsIt = GetLoginUser.IsMemberOf(context, IdentityType.SamAccountName, "Administrators");
                }
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    // Will not be called
                    HttpContext.Current.Response.Redirect("~/Login/Login", true);
                IsIt = false;
            }
            return IsIt;
        }
        public static bool IsAdmin(bool response)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                var grp = GetLoginUser.IsMemberOf(context, IdentityType.SamAccountName, "Administrators");
                if (!grp)
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                        // Will not be called
                        HttpContext.Current.Response.Redirect("~/Login/Login", true);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.AddLog(e);
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    // Will not be called
                    HttpContext.Current.Response.Redirect("~/Login/Login", true);
                return false;
            }
        }
        public static bool LoginUser(string userName, string password, string domain)
        {
            var ret = false;
            PrincipalContext context = new PrincipalContext(ContextType.Domain);
            var IsExist = context.ValidateCredentials(userName, password);
            if (IsExist)
            {
                UserPrincipalEx user = UserPrincipalEx.FindByIdentity(context, userName);
                if (user != null)
                {
                    HttpContext.Current.Session["AdminLogin"] = user;
                    HttpContext.Current.Session["Domain"] = domain;
                    HttpContext.Current.Session["pass"] = password;
                    Logger.AddLog(user.SamAccountName + " adlı kullanıcı giriş yaptı");
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        if (IsAdmin())
                            HttpContext.Current.Response.Redirect("~/Home/Index", false);
                        else
                            HttpContext.Current.Response.Redirect("~/DomainUser/Create", false);
                    }
                    ret = true;
                }
            }
            return ret;
        }
        public static void UserSignOut()
        {
            Logger.AddLog("Çıkış Yaptı");
            HttpContext.Current.Session["AdminLogin"] = 0;
            HttpContext.Current.Session.Remove("AdminLogin");
            HttpContext.Current.Session.RemoveAll();
            if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                // Will not be called
                HttpContext.Current.Response.Redirect("~/Login/Login", true);
        }
    }
}