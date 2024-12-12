using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Bolum")]
    public class Bolum
    {
        [Key]
        public int BolumID { get; set; }
        [Required, StringLength(100)]
        public string BolumAdi { get; set; }
        public int HastaSayisi { get; set; }
        public int BosYatakSayisi { get; set; }
        [StringLength(500)]
        public string Aciklama { get; set; }

        public ICollection<OgretimUyesi> OgretimUyeleri { get; set; }
        public ICollection<Nobet> Nobetler { get; set; }
    }

}