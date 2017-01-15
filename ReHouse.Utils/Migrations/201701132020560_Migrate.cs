namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoleAuthorities", newName: "AuthorityRoles");
            DropPrimaryKey("dbo.AuthorityRoles");
            CreateTable(
                "dbo.AdvertProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdvertPropertyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        AdvertPropertyId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvertProperties", t => t.AdvertPropertyId, cascadeDelete: true)
                .Index(t => t.AdvertPropertyId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Adverts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        TitleId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        Street = c.String(),
                        Price = c.Int(nullable: false),
                        PriceFilterId = c.Int(nullable: false),
                        Description = c.String(),
                        ExpireDate = c.DateTime(nullable: false),
                        YouTubeUrl = c.String(),
                        IsHot = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.PriceFilters", t => t.PriceFilterId, cascadeDelete: true)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.TitleId)
                .Index(t => t.DistrictId)
                .Index(t => t.PriceFilterId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        CityId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Url = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceFilters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryAdvertProperties",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        AdvertProperty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.AdvertProperty_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.AdvertProperties", t => t.AdvertProperty_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.AdvertProperty_Id);
            
            CreateTable(
                "dbo.ImageAdverts",
                c => new
                    {
                        Image_Id = c.Int(nullable: false),
                        Advert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Image_Id, t.Advert_Id })
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .ForeignKey("dbo.Adverts", t => t.Advert_Id, cascadeDelete: true)
                .Index(t => t.Image_Id)
                .Index(t => t.Advert_Id);
            
            AddPrimaryKey("dbo.AuthorityRoles", new[] { "Authority_Id", "Role_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Adverts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Adverts", "TitleId", "dbo.Titles");
            DropForeignKey("dbo.Adverts", "PriceFilterId", "dbo.PriceFilters");
            DropForeignKey("dbo.ImageAdverts", "Advert_Id", "dbo.Adverts");
            DropForeignKey("dbo.ImageAdverts", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Districts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Adverts", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Adverts", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategoryAdvertProperties", "AdvertProperty_Id", "dbo.AdvertProperties");
            DropForeignKey("dbo.CategoryAdvertProperties", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.AdvertPropertyValues", "AdvertPropertyId", "dbo.AdvertProperties");
            DropIndex("dbo.ImageAdverts", new[] { "Advert_Id" });
            DropIndex("dbo.ImageAdverts", new[] { "Image_Id" });
            DropIndex("dbo.CategoryAdvertProperties", new[] { "AdvertProperty_Id" });
            DropIndex("dbo.CategoryAdvertProperties", new[] { "Category_Id" });
            DropIndex("dbo.Districts", new[] { "CityId" });
            DropIndex("dbo.Adverts", new[] { "PriceFilterId" });
            DropIndex("dbo.Adverts", new[] { "DistrictId" });
            DropIndex("dbo.Adverts", new[] { "TitleId" });
            DropIndex("dbo.Adverts", new[] { "CategoryId" });
            DropIndex("dbo.Adverts", new[] { "UserId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.AdvertPropertyValues", new[] { "AdvertPropertyId" });
            DropPrimaryKey("dbo.AuthorityRoles");
            DropTable("dbo.ImageAdverts");
            DropTable("dbo.CategoryAdvertProperties");
            DropTable("dbo.Titles");
            DropTable("dbo.PriceFilters");
            DropTable("dbo.Images");
            DropTable("dbo.Cities");
            DropTable("dbo.Districts");
            DropTable("dbo.Adverts");
            DropTable("dbo.Categories");
            DropTable("dbo.AdvertPropertyValues");
            DropTable("dbo.AdvertProperties");
            AddPrimaryKey("dbo.AuthorityRoles", new[] { "Role_Id", "Authority_Id" });
            RenameTable(name: "dbo.AuthorityRoles", newName: "RoleAuthorities");
        }
    }
}
