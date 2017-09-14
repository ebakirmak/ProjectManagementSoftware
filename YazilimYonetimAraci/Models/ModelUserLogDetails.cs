using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelUserLogDetails
    {
        [DisplayName("Kullanıcı Log Detay ID:")]
        [Editable(false)]
        public int UserLogDetailsID { get; set; }

        [DisplayName("Kullanıcı Log ID: ")]
        [Editable(false)]
        public Nullable<int> UserLogsID { get; set; }

        [DisplayName("Kullanıcı Log adi: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Kullanıcı Log Adını Giriniz: ")]
        public string UserLogName { get; set; }

        [DisplayName("Log Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:dd/mm/yyyy}")]
        [Editable(false)]
        public Nullable<System.DateTime> LogDate { get; set; }

    }
}