using PediatriAsistanNöbet1.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace PediatriAsistanNöbet1.Models.DataContext
{
    public class PediatriDBContext : DbContext
    {
        public PediatriDBContext() : base("PediatriDB")
        {

        }
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Asistan> Asistanlar { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<OgretimUyesi> OgretimUyeleri { get; set; }
        public DbSet<Nobet> Nobetler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<RandevuDurum> RandevuDurumlari { get; set; }
        public DbSet<AcilDurum> AcilDurumlar { get; set; }
        public DbSet<Musaitlik> Musaitlikler { get; set; }



    }
}

