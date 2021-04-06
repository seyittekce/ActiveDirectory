using System.Web;

namespace ActiveDirectory.Actions
{
    public class WhenError
    {
        public static void ThrowError(int Ex)
        {
            if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                HttpContext.Current.Response.Redirect("~/Error/Index?Ex=" + Ex, false);
        }
    }
}