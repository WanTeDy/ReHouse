namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avatars", "UserId", "dbo.Users");
            DropIndex("dbo.Avatars", new[] { "UserId" });
            AddColumn("dbo.Users", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "ImageId");
            AddForeignKey("dbo.Users", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
            DropTable("dbo.Avatars");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Users", "ImageId", "dbo.Images");
            DropIndex("dbo.Users", new[] { "ImageId" });
            DropColumn("dbo.Users", "ImageId");
            CreateIndex("dbo.Avatars", "UserId");
            AddForeignKey("dbo.Avatars", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
