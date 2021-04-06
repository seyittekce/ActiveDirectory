using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectory.Models
{
    [Serializable]
    public class UserProperties
    {
        public enum AuthReqType
        {
            otokocPCWEB,
            EMailHesabi,
            ObisHesabi,
            OtonetHesabi,
            ObisRacHesabi
        }

        public enum Status
        {
            Waiting,
            Comfirmed
        }

        [Key] public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("User Logon Name")]
        public string LogonName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string TelephoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Office")]
        public string Office { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Web Page")]
        public string WebPage { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Fax Number")]
        public string FaxNumber { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Street")]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Employee Number")]
        public string EmployeNumber { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Other Phone Number")]
        public string OtherPhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Added By")]
        public string AddedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Added By Date")]
        public string AddedByTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Operation By Date")]
        public string OperationByTime { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Operation By")]
        public string OperationBy { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Department")]
        public string Department { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Company")]
        public string Company { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Manager")]
        public string Manager { get; set; }

        public string Group { get; set; }
        public Status Stat { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Durum")]
        public string ReturnStat
        {
            get
            {
                if (Stat == Status.Comfirmed) return "Onaylandı";
                if (Stat == Status.Waiting) return "Bekliyor";
                return "Hatalı";
            }
        }

        [DataType(DataType.Text)]
        [DisplayName("Yetki Talep Tipi")]
        public AuthReqType AuthReq { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Yetki Talep Tipi")]
        public string Auth
        {
            get
            {
                if (AuthReq == AuthReqType.EMailHesabi) return "E-Mail Hesabı";
                if (AuthReq == AuthReqType.ObisHesabi) return "Obis Hesabı";
                if (AuthReq == AuthReqType.ObisRacHesabi) return "Obis RAC Hesabı";
                if (AuthReq == AuthReqType.otokocPCWEB) return "OTOKOÇWEB ve PC Hesabı";
                if (AuthReq == AuthReqType.OtonetHesabi) return "Otonet Hesabı";
                return "Hatalı";
            }
        }

        [DataType(DataType.Text)]
        [DisplayName("Distinguished Name")]
        public string DistinguishedName { get; set; }

        public object Domain { get; set; }

        [DisplayName("Is Disabled")] public object IsDisabled { get; set; }

        public object Lastlogon { get; set; }
        public object Mail { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("OU Location")]
        public string OuLocation { get; set; }

        public string[] Groups { get; set; }
    }
}