using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectory.Models
{
    public class Settings
    {
        public int ID { get; set; }
        [DisplayName("Host")] public string Host { get; set; }
        [DisplayName("Port")] public int Port { get; set; }
        [DisplayName("Kullanıcı Adı")] public string User_Name { get; set; }

        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string User_password { get; set; }

        [DisplayName("SSL")] public bool Ssl { get; set; }
        [DisplayName("Debug")] public bool Debug { get; set; }
    }
}