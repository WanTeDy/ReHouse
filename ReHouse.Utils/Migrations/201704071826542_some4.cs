namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageAdverts", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.ImageAdverts", "Advert_Id", "dbo.Adverts");
            DropIndex("dbo.ImageAdverts", new[] { "Image_Id" });
            DropIndex("dbo.ImageAdverts", new[] { "Advert_Id" });
            AddColumn("dbo.Images", "Advert_Id", c => c.Int());
            CreateIndex("dbo.Images", "Advert_Id");
            AddForeignKey("dbo.Images", "Advert_Id", "dbo.Adverts", "Id");
            DropTable("dbo.ImageAdverts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageAdverts",
                c => new
                    {
                        Image_Id = c.Int(nullable: false),
                        Advert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Image_Id, t.Advert_Id });
            
            DropForeignKey("dbo.Images", "Advert_Id", "dbo.Adverts");
            DropIndex("dbo.Images", new[] { "Advert_Id" });
            DropColumn("dbo.Images", "Advert_Id");
            CreateIndex("dbo.ImageAdverts", "Advert_Id");
            CreateIndex("dbo.ImageAdverts", "Image_Id");
            AddForeignKey("dbo.ImageAdverts", "Advert_Id", "dbo.Adverts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ImageAdverts", "Image_Id", "dbo.Images", "Id", cascadeDelete: true);
        }
    }
}
