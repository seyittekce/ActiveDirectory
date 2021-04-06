using System.Security;
namespace ActiveDirectory.Actions
{
    public class Converts
    {
        public static SecureString ToSecureString(string Pass)
        {
            SecureString secure = new SecureString();
            foreach (var t in Pass)
            {
                secure.AppendChar(t);
            }
            return secure;
        }
        public static String ToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
  
}