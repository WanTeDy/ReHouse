namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Images", "NewBuilding_Id", "dbo.NewBuildings");
            DropIndex("dbo.Images", new[] { "NewBuilding_Id" });
            DropIndex("dbo.Articles", new[] { "ImageId" });
            CreateTable(
                "dbo.ArticleImages",
                c => new
                    {
                        Article_Id = c.Int(nullable: false),
                        Image_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_Id, t.Image_Id })
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .Index(t => t.Article_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.ImageNewBuildings",
                c => new
                    {
                        Image_Id = c.Int(nullable: false),
                        NewBuilding_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Image_Id, t.NewBuilding_Id })
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .ForeignKey("dbo.NewBuildings", t => t.NewBuilding_Id, cascadeDelete: true)
                .Index(t => t.Image_Id)
                .Index(t => t.NewBuilding_Id);
            
            DropColumn("dbo.Images", "NewBuilding_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "NewBuilding_Id", c => c.Int());
            DropForeignKey("dbo.ImageNewBuildings", "NewBuilding_Id", "dbo.NewBuildings");
            DropForeignKey("dbo.ImageNewBuildings", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.ArticleImages", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.ArticleImages", "Article_Id", "dbo.Articles");
            DropIndex("dbo.ImageNewBuildings", new[] { "NewBuilding_Id" });
            DropIndex("dbo.ImageNewBuildings", new[] { "Image_Id" });
            DropIndex("dbo.ArticleImages", new[] { "Image_Id" });
            DropIndex("dbo.ArticleImages", new[] { "Article_Id" });
            DropTable("dbo.ImageNewBuildings");
            DropTable("dbo.ArticleImages");
            CreateIndex("dbo.Articles", "ImageId");
            CreateIndex("dbo.Images", "NewBuilding_Id");
            AddForeignKey("dbo.Images", "NewBuilding_Id", "dbo.NewBuildings", "Id");
            AddForeignKey("dbo.Articles", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
        }
    }
}
