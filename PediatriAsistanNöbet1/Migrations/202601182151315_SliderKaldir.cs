namespace PediatriAsistanNöbet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SliderKaldir : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Slider");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        SliderId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 30),
                        Aciklama = c.String(maxLength: 150),
                        ResimURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.SliderId);
            
        }
    }
}
