namespace PediatriAsistanNöbet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMusaitlikTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Musaitlik",
                c => new
                    {
                        MusaitlikID = c.Int(nullable: false, identity: true),
                        OgretimUyesiID = c.Int(nullable: false),
                        Tarih = c.DateTime(nullable: false),
                        Saat = c.String(),
                        MusaitMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MusaitlikID)
                .ForeignKey("dbo.OgretimUyesi", t => t.OgretimUyesiID, cascadeDelete: true)
                .Index(t => t.OgretimUyesiID);
            
            AddColumn("dbo.Randevu", "MusaitlikID", c => c.Int(nullable: false));
            CreateIndex("dbo.Randevu", "MusaitlikID");
            AddForeignKey("dbo.Randevu", "MusaitlikID", "dbo.Musaitlik", "MusaitlikID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Randevu", "MusaitlikID", "dbo.Musaitlik");
            DropForeignKey("dbo.Musaitlik", "OgretimUyesiID", "dbo.OgretimUyesi");
            DropIndex("dbo.Randevu", new[] { "MusaitlikID" });
            DropIndex("dbo.Musaitlik", new[] { "OgretimUyesiID" });
            DropColumn("dbo.Randevu", "MusaitlikID");
            DropTable("dbo.Musaitlik");
        }
    }
}
