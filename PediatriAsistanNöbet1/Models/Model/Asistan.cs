using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Asistan")]
    public class Asistan
    {
        [Key]
        public int AsistanID { get; set; }
        [DisplayName("Asistan Ad")]
        [Required, StringLength(50)]

        public string Ad { get; set; }
        [DisplayName("Soyad")]
        [Required, StringLength(50)]
        public string Soyad { get; set; }
        [DisplayName("Telefon")]
        [StringLength(20)]
        public string Telefon { get; set; }
        [DisplayName("Email")]

        [StringLength(50)]
        public string Email { get; set; }
        [DisplayName("Adres")]
        [StringLength(200)]
        public string Adres { get; set; }

    }

}

