using System;
using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public static class Logger
    {
        private static readonly DBContext db = new DBContext();

        public static void AddLog(string Action)
        {
            if (!string.IsNullOrEmpty(Action))
            {
                StandartLog s = new StandartLog
                {
                    Action = Action,
                    Kim = Login.GetLoginUser.GivenName + " " + Login.GetLoginUser.Surname + " ||" +
                          Login.GetLoginUser.SamAccountName,
                    Date = DateTime.Now
                };
                db.StandartLogs.Add(s);
                db.SaveChanges();
            }
        }

        public static void AddLog(Exception Ex)
        {
            var settings = db.Settings.FirstOrDefault();
            if (settings != null)
                if (settings.Debug)
                {
                    var getError = Error(Ex);
                    var aa = db.ErrorLogs.Add(Error(Ex));
                    db.SaveChanges();
                    WhenError.ThrowError(aa.ID);
                }
        }

        private static ErrorLog Error(Exception exception)
        {
            ErrorLog logger = new ErrorLog
            {
                Message = string.IsNullOrEmpty(Convert.ToString(exception.Message))
                    ? " "
                    : Convert.ToString(exception.Message),
                StackTrace = string.IsNullOrEmpty(Convert.ToString(exception.StackTrace))
                    ? " "
                    : Convert.ToString(exception.StackTrace),
                InnerException = string.IsNullOrEmpty(Convert.ToString(exception.InnerException))
                    ? " "
                    : Convert.ToString(exception.InnerException),
                Source = string.IsNullOrEmpty(Convert.ToString(exception.Source))
                    ? " "
                    : Convert.ToString(exception.Source),
                Data = string.IsNullOrEmpty(Convert.ToString(exception.Data)) ? " " : Convert.ToString(exception.Data),
                HelpLink = string.IsNullOrEmpty(Convert.ToString(exception.HelpLink))
                    ? " "
                    : Convert.ToString(exception.HelpLink),
                HResult = string.IsNullOrEmpty(Convert.ToString(exception.HResult))
                    ? " "
                    : Convert.ToString(exception.HResult),
                TargetSite = string.IsNullOrEmpty(Convert.ToString(exception.TargetSite))
                    ? " "
                    : Convert.ToString(exception.TargetSite),
                Date = DateTime.Now
            };
            return logger;
        }
    }
}