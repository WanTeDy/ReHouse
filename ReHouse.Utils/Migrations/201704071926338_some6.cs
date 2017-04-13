namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "ImageId");
            AddForeignKey("dbo.Users", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ImageId", "dbo.Images");
            DropIndex("dbo.Users", new[] { "ImageId" });
            DropColumn("dbo.Users", "ImageId");
        }
    }
}
