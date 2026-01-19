namespace PediatriAsistanNöbet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SifreUzunlukArtir : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin", "Sifre", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "Sifre", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
