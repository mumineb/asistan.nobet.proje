using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Randevu")]
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }

        public int AsistanID { get; set; }
        [ForeignKey("AsistanID")]
        public Asistan Asistan { get; set; }

        public int OgretimUyesiID { get; set; }
        [ForeignKey("OgretimUyesiID")]
        public OgretimUyesi OgretimUyesi { get; set; }

        public int RandevuDurumID { get; set; }
        [ForeignKey("RandevuDurumID")]
        public RandevuDurum RandevuDurum { get; set; }

        [Required]
        public DateTime RandevuTarihi { get; set; }
        [StringLength(500)]
        public string Aciklama { get; set; }
        public int MusaitlikID { get; set; }
        [ForeignKey("MusaitlikID")]
        public Musaitlik Musaitlik { get; set; }

    }

}