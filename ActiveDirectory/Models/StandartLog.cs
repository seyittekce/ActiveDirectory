using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectory.Models
{
    public class StandartLog
    {
        [Key] public int ID { get; set; }

        [Required]
        [DisplayName("Olay")]
        [DataType(DataType.Text)]
        public string Action { get; set; }

        [Required]
        [DisplayName("Kim")]
        [DataType(DataType.Text)]
        public string Kim { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Tarih")]
        public DateTime Date { get; set; }
    }
}