namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filters5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewBuildings", "DistrictId", c => c.Int(nullable: false));
            CreateIndex("dbo.NewBuildings", "DistrictId");
            AddForeignKey("dbo.NewBuildings", "DistrictId", "dbo.Districts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewBuildings", "DistrictId", "dbo.Districts");
            DropIndex("dbo.NewBuildings", new[] { "DistrictId" });
            DropColumn("dbo.NewBuildings", "DistrictId");
        }
    }
}
