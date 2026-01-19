using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("OgretimUyesi")]
    public class OgretimUyesi
    {
        [Key]
        public int OgretimUyesiID { get; set; }
        public int BolumID { get; set; }
        [ForeignKey("BolumID")]
        [DisplayName("Bölüm")]
        public Bolum Bolum { get; set; }
        [Required, StringLength(50)]
        [DisplayName("Ad")]
        public string Ad { get; set; }
        [Required, StringLength(50)]
        [DisplayName("Soyad")]
        public string Soyad { get; set; }
        [StringLength(20)]
        [DisplayName("Telefon")]
        public string Telefon { get; set; }
        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [StringLength(200)]
        [DisplayName("Adres")]
        public string Adres { get; set; }
        public ICollection<Musaitlik> Musaitlikler { get; set; } = new List<Musaitlik>();

        public ICollection<Randevu> Randevular { get; set; }
    }

}