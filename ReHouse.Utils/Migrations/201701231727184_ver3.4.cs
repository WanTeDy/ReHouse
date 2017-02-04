namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver34 : DbMigration
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
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Url = c.String(),
                        UserId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AdvertPropertyValues", "PropertiesValue", c => c.String());
            AddColumn("dbo.AdvertPropertyValues", "AdvertId", c => c.Int(nullable: false));
            CreateIndex("dbo.AdvertPropertyValues", "AdvertId");
            AddForeignKey("dbo.AdvertPropertyValues", "AdvertId", "dbo.Adverts", "Id", cascadeDelete: true);
            DropColumn("dbo.AdvertPropertyValues", "RussianName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvertPropertyValues", "RussianName", c => c.String());
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Avatars", "UserId", "dbo.Users");
            DropForeignKey("dbo.Articles", "ImageId", "dbo.Images");
            DropForeignKey("dbo.AdvertPropertyValues", "AdvertId", "dbo.Adverts");
            DropIndex("dbo.Avatars", new[] { "UserId" });
            DropIndex("dbo.Articles", new[] { "ImageId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.AdvertPropertyValues", new[] { "AdvertId" });
            DropColumn("dbo.AdvertPropertyValues", "AdvertId");
            DropColumn("dbo.AdvertPropertyValues", "PropertiesValue");
            DropTable("dbo.Avatars");
            DropTable("dbo.Articles");
        }
    }
}
