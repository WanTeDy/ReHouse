namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partners : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partners", "Image_Id", "dbo.Images");
            DropIndex("dbo.Partners", new[] { "Image_Id" });
            DropTable("dbo.Partners");
        }
    }
}
