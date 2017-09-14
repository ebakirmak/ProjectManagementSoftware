using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelUserProcess
    {
        [DisplayName("Kullanıcı Process ID: ")]
        [Editable(false)]
        public int UserProcessID { get; set; }

        [DisplayName("Kullanıcı ID: ")]
        [Editable(false)]
        public Nullable<int> UsersID { get; set; }

        [DisplayName("Process ID: ")]
        [Editable(false)]
        public Nullable<int> ProcessID { get; set; }

        [DisplayName("Kullanıcı Rol ID: ")]
        [Editable(false)]
        public Nullable<int> UserRolesID { get; set; }
    }
}