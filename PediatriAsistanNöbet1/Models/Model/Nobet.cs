using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Nobet")]
    public class Nobet
    {
        [Key]
        public int NobetID { get; set; }
        public int AsistanID { get; set; }
        [ForeignKey("AsistanID")]
        public Asistan Asistan { get; set; }

        public int BolumID { get; set; }
        [ForeignKey("BolumID")]
        public Bolum Bolum { get; set; }

        [Required]
        public DateTime NobetTarihi { get; set; }
    }

}