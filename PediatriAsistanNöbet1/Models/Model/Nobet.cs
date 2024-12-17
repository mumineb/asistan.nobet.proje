using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Nobet")]
    public class Nobet
    {
        [Key]
        public int NobetID { get; set; }
        public int AsistanID { get; set; }
        [ForeignKey("AsistanID")]
        [DisplayName("Asistan Ad")]
        [Required, StringLength(50)]
        public Asistan Asistan { get; set; }

        public int BolumID { get; set; }
        [ForeignKey("BolumID")]
        [DisplayName("Bölüm")]
        [Required, StringLength(50)]
        public Bolum Bolum { get; set; }

        [Required]
        [DisplayName("Nöbet Tarihi")]
        public DateTime NobetTarihi { get; set; }
    }

}