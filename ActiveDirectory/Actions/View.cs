namespace ActiveDirectory.Actions
{
    public static class ReturnView
    {
        public static string Name => Login.GetLoginUser.GivenName;
        public static string LastName => Login.GetLoginUser.secondname;

        public static string SubName
        {
            get
            {
                if (Login.GetLoginUser.GivenName != null) return Login.GetLoginUser.GivenName.Substring(0, 1);
                return string.Empty;
            }
        }
    }
}