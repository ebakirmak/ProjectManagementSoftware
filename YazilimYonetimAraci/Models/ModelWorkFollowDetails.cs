using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelWorkFollowDetails
    {
        [DisplayName("İş akışı detay ID'si: ")]
        [Editable(false)]
        public int WorkFollowDetailsID { get; set; }

        [DisplayName("İş akışı adı: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Lütfen is akışı adını giriniz: ")]
        public string WorkFollowName { get; set; }
    }
}