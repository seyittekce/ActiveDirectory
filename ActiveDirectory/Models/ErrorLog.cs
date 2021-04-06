using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectory.Models
{
    public class ErrorLog
    {
        [Key] [DisplayName("Hata Numarası")] public int ID { get; set; }

        [DisplayName("Hata Mesajı")]
        [DataType(DataType.Text)]
        public string? Message { get; set; }

        [DisplayName("Yığın İzleyici")]
        [DataType(DataType.Text)]
        public string? StackTrace { get; set; }

        [DisplayName("Inner Exception")]
        [DataType(DataType.Text)]
        public string? InnerException { get; set; }

        [DisplayName("Kaynak")]
        [DataType(DataType.Text)]
        public string? Source { get; set; }

        [DisplayName("Veri")]
        [DataType(DataType.Text)]
        public string? Data { get; set; }

        [DisplayName("Yardım Linki")]
        [DataType(DataType.Text)]
        public string? HelpLink { get; set; }

        [DisplayName("Hata Kodu")]
        [DataType(DataType.Text)]
        public string? HResult { get; set; }

        [DisplayName("Hedef")]
        [DataType(DataType.Text)]
        public string? TargetSite { get; set; }

        [DisplayName("Oluşan Tarih")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
#nullable enable
    }
}