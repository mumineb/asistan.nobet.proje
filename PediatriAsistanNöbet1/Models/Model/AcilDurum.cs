using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("AcilDurum")]
    public class AcilDurum
    {
        [Key]
        public int AcilDurumID { get; set; }
        [Required, StringLength(100)]
        public string Baslik { get; set; }
        [Required, StringLength(500)]
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
    }

}