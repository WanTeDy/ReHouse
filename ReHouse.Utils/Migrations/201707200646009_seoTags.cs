namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seoTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        ShortName = c.String(),
                        TagPageType = c.Int(nullable: false),
                        AdvertsType = c.Int(nullable: false),
                        SeoText = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Category_Id = c.Int(),
                        District_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Districts", t => t.District_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.District_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPages", "District_Id", "dbo.Districts");
            DropForeignKey("dbo.TagPages", "Category_Id", "dbo.Categories");
            DropIndex("dbo.TagPages", new[] { "District_Id" });
            DropIndex("dbo.TagPages", new[] { "Category_Id" });
            DropTable("dbo.TagPages");
        }
    }
}
