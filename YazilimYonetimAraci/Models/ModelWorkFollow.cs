using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelWorkFollow
    {
        [DisplayName("İş akışının ID'si: ")]
        public int WorkFollowID { get; set; }

        [DisplayName("Process ID: ")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> ProcessID { get; set; }

        [DisplayName("Başlangıç Tarihi: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]      
        [Editable(false)]
        public Nullable<System.DateTime> StartDate { get; set; }

        [DisplayName("Bitiş Tarihi: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Editable(false)]
        public Nullable<System.DateTime> FinishDate { get; set; }

        [DisplayName("İş akışı detay ID: ")]
        [Editable(false)]
        [Required(ErrorMessage = "*")]
        public Nullable<int> WorkFollowDetailsID { get; set; }

        [DisplayName("Kullanıcı ID: ")]
        [Editable(false)]
        [Required(ErrorMessage = "*")]
        public Nullable<int> UserID { get; set; }

        [DisplayName("Tamamlanma Oranı: ")]
        [RegularExpression("^0*(?:[1-9][0-9]?|100)$", ErrorMessage = "0-100 arasında bir sayi giriniz!")]
        public Nullable<int> CompleteRate { get; set; }

        [MinLength(1, ErrorMessage = "En az 1 karakterden oluşmalıdır.")]
        [MaxLength(200, ErrorMessage = "En fazla 200 karakterden oluşmalıdır.")]
        [DisplayName("İş Detayı: ")]
        public string WorkName { get; set; }
    }
}