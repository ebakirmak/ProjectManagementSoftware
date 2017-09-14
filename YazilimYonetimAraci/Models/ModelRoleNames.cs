using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelRoleNames
    {
        [DisplayName("Rol adının ID'si: ")]
        [Editable(false)]
        public int RoleNameID { get; set; }

        [DisplayName("Rol Adi: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Lütfen rol adını giriniz: ")]

        public string RoleName { get; set; }
    }
}