using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelUserRoles
    {
        [DisplayName("Kullanıcı Rol ID: ")]
        [Editable(false)]
        public int UserRolesID { get; set; }

        [DisplayName("Kullanıcı ID: ")]
        [Editable(false)]
        public Nullable<int> UserID { get; set; }

        [DisplayName("Rol ID: ")]
        [Editable(false)]
        public Nullable<int> RoleNameID { get; set; }
    }
}