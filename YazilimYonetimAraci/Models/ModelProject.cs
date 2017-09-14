using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelProject
    {
        [Editable(false)]
        public int ProjectID { get; set; }

        [DisplayName("Proje Adı: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "*")]
        [StringLength(100,ErrorMessage ="Proje Adı daha uzun olamaz.")]
        public string ProjectName { get; set; }


        [DisplayName("Başlangıç Tarihi: ")]
        [Editable(false)]
        [DataType(DataType.Date, ErrorMessage = "tt")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "*")]
        public Nullable<System.DateTime> StartDate { get; set; }

       
        [DisplayName("Bitiş Tarihi: ")]
        [Editable(true)]
        [DataType(DataType.Date, ErrorMessage = "tt")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]       
        [Required(ErrorMessage = "*")]
        public Nullable<DateTime> FinishDate { get; set; }

        [DisplayName("Projenin Durumu: ")]
        [Required(ErrorMessage = "*")]
        public Nullable<bool> isActive { get; set; }


        public string isActiveString { get; set; }

        //BÜTÇEDE HATA...
        [DisplayName("Bütçe: ")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Currency, ErrorMessage = "?")]
        [RegularExpression("^(([0-9])|([0-9]+[,][0-9]))+$", ErrorMessage = "Harf Girmeyiniz")]
        [Range((typeof(decimal)), "0", "922337203685477", ErrorMessage ="Küçük sayı giriniz")]
        public Nullable<decimal> Budget { get; set; }

        [DisplayName("Müşteri ID: ")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> ProjectOwnerID { get; set; }

        [DisplayName("Yönetici ID:")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> ProjectManagerID { get; set; }
    }
}