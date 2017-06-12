namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionName = c.String(),
                        ControllerName = c.String(),
                        TextBlockName = c.String(),
                        Description = c.String(),
                        Title = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeoParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Keywords = c.String(),
                        ActionName = c.String(),
                        ControllerName = c.String(),
                        UrlParams = c.String(),
                        FullUrl = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SeoParams");
            DropTable("dbo.PageTexts");
        }
    }
}
