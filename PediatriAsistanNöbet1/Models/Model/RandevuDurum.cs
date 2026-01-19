using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("RandevuDurum")]
    public class RandevuDurum
    {
        [Key]
        public int RandevuDurumID { get; set; }
        [Required, StringLength(50)]
        public string DurumAdi { get; set; }
    }

}