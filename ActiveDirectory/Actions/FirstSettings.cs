using System.Linq;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class FirstSettings
    {
        private readonly DBContext db = new DBContext();

        public FirstSettings()
        {
            if (db.Settings.Count() == 0)
            {
                Settings st = new Settings();
                st.Debug = true;
                st.Host = "Host";
                st.Port = 123;
                st.Ssl = true;
                st.User_Name = "username";
                st.User_password = "password";
                db.Settings.Add(st);
                db.SaveChanges();
            }
        }
    }
}