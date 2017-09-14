using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelUserLogs
    {
        [DisplayName("Kullanıcı log ID: ")]
        [Editable(false)]
        public int UserLogsID { get; set; }

        [DisplayName("Kullanıcı Log Detay ID: ")]
        [Editable(false)]
        public string UserLogDetailsID { get; set; }

        [DisplayName("Kullanıcı ID: ")]
        [Editable(false)]
        public Nullable<int> UserID { get; set; }
    }
}