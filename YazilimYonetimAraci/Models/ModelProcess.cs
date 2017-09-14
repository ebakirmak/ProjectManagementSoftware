using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelProcess
    {
        [DisplayName("Süreç ID: ")]
        [Editable(false)]
        public int ProcessID { get; set; }


        [DisplayName("Süreç Adı: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="*")]
        //[RegularExpression("^([a-zA-Z/ /s/ç/Ç/ö/Ö/ş/Ş/ı/İ/ğ/Ğ/ü/Ü]*)$")]
        [MinLength(1, ErrorMessage = "En az 1 karakterden oluşmalıdır.")]
        [MaxLength(50, ErrorMessage = "En fazla 50 karakterden oluşmalıdır.")]
        public string ProcessName { get; set; }

        [DisplayName("Proje ID: ")]
        [Editable(false)]
        public Nullable<int> ProjectID { get; set; }

        [DisplayName("Başlangıç Tarihi: ")]
        [DataType(DataType.Date)]
        [Editable(false)]
        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [DisplayName("Bitiş Tarihi: ")]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "*")]
        public Nullable<System.DateTime> FinishDate { get; set; }

        [DisplayName("Süre")]
        [DataType(DataType.Duration)]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Gün sayısı giriniz")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Süre bilgisi yanlış")]
        [Required(ErrorMessage ="*")]
        public Nullable<int> Duration { get; set; }

        [DisplayName("Tamamlanma Yüzdesi: ")]
        [RegularExpression("^0*(?:[1-9][0-9]?|100)$", ErrorMessage ="0-100 arasında bir sayi giriniz!")]
        public Nullable<int> CompleteRate { get; set; }

        [DisplayName("Öncelik: ")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> Priority { get; set; }

        public string PriorityString { get; set; }


        [DisplayName("Parent ID: ")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> ParentID { get; set; }

        [DisplayName("Notlar: ")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "En az 2 karakterden oluşmalıdır.")]
        [MaxLength(1000, ErrorMessage = "En fazla 1000 karakterden oluşmalıdır.")]
        public string Notes { get; set; }

        [DisplayName("Tanımlama: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "*")]
        [MinLength(2, ErrorMessage = "En az 2 karakterden oluşmalıdır.")]
        [MaxLength(1000, ErrorMessage = "En fazla 1000 karakterden oluşmalıdır.")]
        public string Descriptions { get; set; }

        [DisplayName("Süreç Sorumlusu: ")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]        
        public Nullable<int> ProcessManagerID { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Süreç Tipi: ")]
        public string ProcessType { get; set; }
    }
}