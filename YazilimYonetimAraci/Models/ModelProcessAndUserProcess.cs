using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelProcessAndUserProcess
    {
        [DisplayName("Process ID: ")]
        [Editable(false)]
        public int ProcessID { get; set; }


        [DisplayName("Process Name: ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Lütfen Süreç adını giriniz!")]
        [RegularExpression("^([a-zA-Z/ /s/ç/Ç/ö/Ö/ş/Ş/ı/İ/ğ/Ğ/ü/Ü]*)$")]
        public string ProcessName { get; set; }

        [DisplayName("Project ID: ")]
        [Editable(false)]
        public Nullable<int> ProjectID { get; set; }

        [DisplayName("Başlangıç Tarihi: ")]
        [DataType(DataType.Date)]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yy}")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [DisplayName("Bitiş Tarihi: ")]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yy}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FinishDate { get; set; }

        [DisplayName("Süre")]
        [DataType(DataType.Duration)]
        public Nullable<int> Duration { get; set; }

        [DisplayName("Tamamlanma Yüzdesi: ")]
        [RegularExpression("^0*(?:[1-9][0-9]?|100)$", ErrorMessage = "0-100 arasında bir sayi giriniz!")]
        public Nullable<int> CompleteRate { get; set; }

        [DisplayName("Öncelik: ")]
        public Nullable<int> Priority { get; set; }

        [DisplayName("Parent ID: ")]
        public Nullable<int> ParentID { get; set; }

        [DisplayName("Notlar: ")]
        [DataType(DataType.Text)]
        public string Notes { get; set; }

        [DisplayName("Tanımlama: ")]
        [DataType(DataType.Text)]
        public string Descriptions { get; set; }

        [DisplayName("Proje Yönetici ID: ")]
        public Nullable<int> ProjectManagerID { get; set; }

        public string ProcessType { get; set; }

        //
        [DisplayName("Kullanıcı Process ID: ")]
        [Editable(false)]
        public int UserProcessID { get; set; }

        [DisplayName("Kullanıcı ID: ")]
        [Editable(false)]
        public Nullable<int> UsersID { get; set; }

        [DisplayName("Kullanıcı Rol ID: ")]
        [Editable(false)]
        public Nullable<int> UserRolesID { get; set; }
    }
}