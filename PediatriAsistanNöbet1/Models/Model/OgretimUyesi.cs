using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("OgretimUyesi")]
    public class OgretimUyesi
    {
        [Key]
        public int OgretimUyesiID { get; set; }
        public int BolumID { get; set; }
        [ForeignKey("BolumID")]
        public Bolum Bolum { get; set; }

        [Required, StringLength(50)]
        public string Ad { get; set; }
        [Required, StringLength(50)]
        public string Soyad { get; set; }
        [StringLength(20)]
        public string Telefon { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Adres { get; set; }

        public ICollection<Randevu> Randevular { get; set; }
    }

}