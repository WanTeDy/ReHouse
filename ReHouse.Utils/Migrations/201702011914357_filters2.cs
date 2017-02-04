namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filters2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Districts", "CityId", "dbo.Cities");
            DropIndex("dbo.Districts", new[] { "CityId" });
            DropColumn("dbo.Districts", "CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Districts", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Districts", "CityId");
            AddForeignKey("dbo.Districts", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
