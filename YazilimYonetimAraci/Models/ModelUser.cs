using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelUser
    {
        //GEREKLİ KONTROLLERİN YAPILACAĞI KISIM

        //DisplayName: Labellerde ne yazıcak...
        //Required: Boş Geçilemez...
        //Editable: Düzenlenebilirliği
        //Datatype: Textboxlara hangi veri tipi girilebilir
        //DisplayFormat: Nasıl gözüksün...
         
        [DisplayName("ID: ")]
        [Editable(false)]
        public int UsersID { get; set; }

        [DisplayName("Adı: ")]
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Text)]
        [MinLength(2,ErrorMessage ="En az 2 karakterden oluşmalıdır.")]
        [MaxLength(50, ErrorMessage = "En fazla 50 karakterden oluşmalıdır.")]
        [RegularExpression("^([a-zA-Z/ /s/ç/Ç/ö/Ö/ş/Ş/ı/İ/ğ/Ğ/ü/Ü]*)$", ErrorMessage = "Lütfen text ifadelerden başka karakter girmeyiniz.")]
        public string UserName { get; set; }

        [DisplayName("Soyadı: ")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "En az 2 karakterden oluşmalıdır.")]
        [MaxLength(50, ErrorMessage = "En fazla 50 karakterden oluşmalıdır.")]
        [RegularExpression("^([a-zA-Z/ /s/ç/Ç/ö/Ö/ş/Ş/ı/İ/ğ/Ğ/ü/Ü]*)$", ErrorMessage = "Lütfen text ifadelerden başka karakter girmeyiniz.")]
        public string UserSurname { get; set; }

        [DisplayName("Nickname: ")]
        [Required(ErrorMessage = "*")]
        [Editable(false)]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "En az 2 karakterden oluşmalıdır.")]
        [MaxLength(50, ErrorMessage = "En fazla 50 karakterden oluşmalıdır.")]
        [RegularExpression("^([a-zA-Z/ /s/ç/Ç/ö/Ö/ş/Ş/ı/İ/ğ/Ğ/ü/Ü]*)$", ErrorMessage = "Lütfen text ifadelerden başka karakter girmeyiniz.")]
        public string UserNickname { get; set; }

        [DisplayName("Parola: ")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "En az 6 karakterden oluşmalıdır.")]
        [MaxLength(50, ErrorMessage = "En fazla 50 karakterden oluşmalıdır.")]
        public string UserPassword { get; set; }

        [DisplayName("Oluşturulma Tarihi: ")]     
        [Editable(false)]
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        [DataType(DataType.Date)]
        public System.DateTime UserCreatedDate { get; set; }

        [DisplayName("Çalışma Durumu: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Lütfen Durumu Giriniz.")]
        public bool isActive { get; set; }
       
       
    }
}