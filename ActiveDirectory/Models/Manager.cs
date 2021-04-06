using ActiveDirectory.Actions;

namespace ActiveDirectory.Models
{
    public class Manager
    {
        public int ID { get; set; }
        public string? Display { get; set; }
        public string? DName { get; set; }
    }

    public static class GetManager
    {
        public static Manager Get(string Value)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                var aa = UserFinder.FindUser(Value);
                return new Manager {Display = aa.DisplayName, DName = aa.DistinguishedName};
            }

            return new Manager {Display = "", DName = ""};
        }
    }
}