using System.Linq;
using System.Net;
using System.Net.Mail;
using ActiveDirectory.Models;

namespace ActiveDirectory.Actions
{
    public class SendMail
    {
        public static void MailSender(string body, string Mail)
        {
            DBContext db = new DBContext();
            var Settings = db.Settings.FirstOrDefault();
            string htmlString = @"<html>
                      <body>" + body + "</body></html>";
            var fromAddress = new MailAddress(Settings.User_Name, "Otokoç Otomotiv Sistem Alt Yapı Hesap Bilgileri");
            const string subject = "Kullanıcı Oturum Bilgileri";
            using (var smtp = new SmtpClient
            {
                Host = Settings.Host,
                Port = Settings.Port,
                EnableSsl = Settings.Ssl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Settings.User_Name, Settings.User_password)
            })
            {
                var toAddress = new MailAddress(Mail);
                using (var message = new MailMessage(fromAddress, toAddress) {Subject = subject, Body = body})
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
        }
    }
}