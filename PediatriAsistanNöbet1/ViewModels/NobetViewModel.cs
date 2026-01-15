using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.ViewModels
{
    public class NobetViewModel
    {
        public int NobetID { get; set; }
        [DisplayName("Asistan Ad Soyad")]
        public string AsistanAdiSoyadi { get; set; } // Ad + Soyad birleşik hali
        [DisplayName("Bölüm")]
        public string BolumAdi { get; set; }
        [DisplayName("Nöbet Tarihi")]
        public DateTime NobetTarihi { get; set; }
    }
}
