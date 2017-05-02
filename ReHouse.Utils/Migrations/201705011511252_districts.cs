namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class districts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Districts", "ParrentId", c => c.Int());
            CreateIndex("dbo.Districts", "ParrentId");
            AddForeignKey("dbo.Districts", "ParrentId", "dbo.Districts", "Id");
            DropColumn("dbo.Articles", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ImageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Districts", "ParrentId", "dbo.Districts");
            DropIndex("dbo.Districts", new[] { "ParrentId" });
            DropColumn("dbo.Districts", "ParrentId");
        }
    }
}
