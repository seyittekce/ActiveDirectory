using System;
using System.DirectoryServices.AccountManagement;
using System.Reflection;

namespace ActiveDirectory.Models
{
    [DirectoryObjectClass("user")]
    [DirectoryRdnPrefix("CN")]
    [Serializable]
    public class UserPrincipalEx : UserPrincipal
    {
        public UserPrincipalEx(PrincipalContext context) : base(context)
        {
        }

        public UserPrincipalEx(PrincipalContext
                context,
            string samAccountName,
            string password,
            bool enabled)
            : base(context, samAccountName, password, enabled)
        {
        }

        [DirectoryProperty("company")]
        public string Company
        {
            get
            {
                if (ExtensionGet("company").Length != 1) return string.Empty;
                return (string) ExtensionGet("company")[0];
            }
            set => ExtensionSet("company", value);
        }

        [DirectoryProperty("physicaldeliveryofficename")]
        public string Office
        {
            get
            {
                if (ExtensionGet("physicaldeliveryofficename").Length != 1) return string.Empty;
                return (string) ExtensionGet("physicaldeliveryofficename")[0];
            }
            set => ExtensionSet("physicaldeliveryofficename", value);
        }

        [DirectoryProperty("wwwhomepage")]
        public string WebPage
        {
            get
            {
                if (ExtensionGet("wwwhomepage").Length != 1) return string.Empty;
                return (string) ExtensionGet("wwwhomepage")[0];
            }
            set => ExtensionSet("wwwhomepage", value);
        }

        [DirectoryProperty("facsimileTelephoneNumber")]
        public string FaxNumber
        {
            get
            {
                if (ExtensionGet("facsimileTelephoneNumber").Length != 1) return string.Empty;
                return (string) ExtensionGet("facsimileTelephoneNumber")[0];
            }
            set => ExtensionSet("facsimileTelephoneNumber", value);
        }

        [DirectoryProperty("streetaddress")]
        public string Street
        {
            get
            {
                if (ExtensionGet("streetaddress").Length != 1) return string.Empty;
                return (string) ExtensionGet("streetaddress")[0];
            }
            set => ExtensionSet("streetaddress", value);
        }

        [DirectoryProperty("postalcode")]
        public string ZipCode
        {
            get
            {
                if (ExtensionGet("postalcode").Length != 1) return string.Empty;
                return (string) ExtensionGet("postalcode")[0];
            }
            set => ExtensionSet("postalcode", value);
        }

        [DirectoryProperty("l")]
        public string City
        {
            get
            {
                if (ExtensionGet("l").Length != 1) return string.Empty;
                return (string) ExtensionGet("l")[0];
            }
            set => ExtensionSet("l", value);
        }

        [DirectoryProperty("serialnumber")]
        public string Serialnumber
        {
            get
            {
                if (ExtensionGet("serialnumber").Length != 1) return string.Empty;
                return (string) ExtensionGet("serialnumber")[0];
            }
            set => ExtensionSet("serialnumber", value);
        }

        [DirectoryProperty("otherMobile")]
        public string otherTelephoneNumber
        {
            get
            {
                if (ExtensionGet("otherMobile").Length != 1) return string.Empty;
                return (string) ExtensionGet("otherMobile")[0];
            }
            set => ExtensionSet("otherMobile", value);
        }

        [DirectoryProperty("department")]
        public string Department
        {
            get
            {
                if (ExtensionGet("department").Length != 1) return string.Empty;
                return (string) ExtensionGet("department")[0];
            }
            set => ExtensionSet("department", value);
        }

        [DirectoryProperty("manager")]
        public string Manager
        {
            get
            {
                if (ExtensionGet("manager").Length != 1) return string.Empty;
                return (string) ExtensionGet("manager")[0];
            }
            set => ExtensionSet("manager", value);
        }

        [DirectoryProperty("title")]
        public string JobTitle
        {
            get
            {
                if (ExtensionGet("title").Length != 1) return string.Empty;
                return (string) ExtensionGet("title")[0];
            }
            set => ExtensionSet("title", value);
        }

        [DirectoryProperty("givenname")]
        public string Givennames
        {
            get
            {
                if (ExtensionGet("givenname").Length != 1) return string.Empty;
                return (string) ExtensionGet("givenname")[0];
            }
            set => ExtensionSet("givenname", value);
        }

        [DirectoryProperty("sn")]
        public string secondname
        {
            get
            {
                if (ExtensionGet("sn").Length != 1) return string.Empty;
                return (string) ExtensionGet("sn")[0];
            }
            set => ExtensionSet("sn", value);
        }

        [DirectoryProperty("EmployeeNumber")]
        public object EmployeeNumber
        {
            get
            {
                if (ExtensionGet("EmployeeNumber").Length != 1) return string.Empty;
                return ExtensionGet("EmployeeNumber")[0];
            }
            set => ExtensionSet("EmployeeNumber", value);
        }

        [DirectoryProperty("RealLastLogon")]
        public DateTime? RealLastLogons
        {
            get
            {
                if (ExtensionGet("LastLogon").Length > 0)
                {
                    var lastLogonDate = ExtensionGet("LastLogon")[0];
                    var lastLogonDateType = lastLogonDate.GetType();
                    var highPart = (int) lastLogonDateType.InvokeMember("HighPart",
                        BindingFlags.GetProperty, null, lastLogonDate, null);
                    var lowPart = (int) lastLogonDateType.InvokeMember("LowPart",
                        BindingFlags.GetProperty | BindingFlags.Public, null, lastLogonDate, null);
                    var longDate = ((long) highPart << 32) | (uint) lowPart;
                    return longDate > 0 ? (DateTime?) DateTime.FromFileTime(longDate) : null;
                }

                return null;
            }
        }

        public string Domain { get; set; }
        public string Password { get; set; }

        public new static UserPrincipalEx FindByIdentity(PrincipalContext context, string identityValue)
        {
            return (UserPrincipalEx) FindByIdentityWithType(context, typeof(UserPrincipalEx), identityValue);
        }

        public new static UserPrincipalEx FindByIdentity(PrincipalContext context, IdentityType identityType,
            string identityValue)
        {
            return (UserPrincipalEx) FindByIdentityWithType(context, typeof(UserPrincipalEx), identityType,
                identityValue);
        }
    }
}