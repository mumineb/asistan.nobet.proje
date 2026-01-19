using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Musaitlik")]
  
        public class Musaitlik
        {
            [Key]
            public int MusaitlikID { get; set; }
            public int? OgretimUyesiID { get; set; }
            public OgretimUyesi OgretimUyesi { get; set; }

            [Required]
            public DateTime Tarih { get; set; }

            [Required]
            public TimeSpan Saat { get; set; }
        }

    }