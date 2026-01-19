using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PediatriAsistanNöbet1.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int KullaniciID { get; set; }
        [Required, StringLength(50)]
        public string Eposta { get; set; }
        [Required, StringLength(100)]
        public string Sifre { get; set; }
    }

}