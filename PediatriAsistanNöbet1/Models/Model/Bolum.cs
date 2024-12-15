using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Bolum")]
    public class Bolum
    {
        [Key]
        public int BolumID { get; set; }
        [Required, StringLength(100)]
        [DisplayName("Bölüm Adı")]
        public string BolumAdi { get; set; }
        [DisplayName("Hasta Sayısı")]
        public int HastaSayisi { get; set; }
        [DisplayName("Boş Yatak Sayısı")]
        public int BosYatakSayisi { get; set; }
        [StringLength(500)]

        public string Aciklama { get; set; }

        public ICollection<OgretimUyesi> OgretimUyeleri { get; set; }
        public ICollection<Nobet> Nobetler { get; set; }
    }

}