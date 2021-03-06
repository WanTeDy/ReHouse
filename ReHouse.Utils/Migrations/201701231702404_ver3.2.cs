namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Url = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AdvertPropertyValues", "PropertiesValue", c => c.String());
            AddColumn("dbo.AdvertPropertyValues", "AdvertId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Image_Id", c => c.Int());
            CreateIndex("dbo.AdvertPropertyValues", "AdvertId");
            CreateIndex("dbo.Users", "UserImageId");
            CreateIndex("dbo.Users", "Image_Id");
            AddForeignKey("dbo.AdvertPropertyValues", "AdvertId", "dbo.Adverts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "UserImageId", "dbo.UserImages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "Image_Id", "dbo.Images", "Id");
            DropColumn("dbo.AdvertPropertyValues", "RussianName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvertPropertyValues", "RussianName", c => c.String());
            DropForeignKey("dbo.Users", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserImageId", "dbo.UserImages");
            DropForeignKey("dbo.Articles", "ImageId", "dbo.Images");
            DropForeignKey("dbo.AdvertPropertyValues", "AdvertId", "dbo.Adverts");
            DropIndex("dbo.Users", new[] { "Image_Id" });
            DropIndex("dbo.Users", new[] { "UserImageId" });
            DropIndex("dbo.Articles", new[] { "ImageId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.AdvertPropertyValues", new[] { "AdvertId" });
            DropColumn("dbo.Users", "Image_Id");
            DropColumn("dbo.Users", "UserImageId");
            DropColumn("dbo.AdvertPropertyValues", "AdvertId");
            DropColumn("dbo.AdvertPropertyValues", "PropertiesValue");
            DropTable("dbo.UserImages");
            DropTable("dbo.Articles");
        }
    }
}
