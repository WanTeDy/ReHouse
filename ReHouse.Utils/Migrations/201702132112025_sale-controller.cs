namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salecontroller : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrimConditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Adverts", "TrimConditionId", c => c.Int(nullable: false));
            AddColumn("dbo.Adverts", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.PriceFilters", "AdvertType", c => c.Int(nullable: false));
            CreateIndex("dbo.Adverts", "TrimConditionId");
            AddForeignKey("dbo.Adverts", "TrimConditionId", "dbo.TrimConditions", "Id", cascadeDelete: true);
            DropTable("dbo.PriceFilterNewBuildings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceFilterNewBuildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Adverts", "TrimConditionId", "dbo.TrimConditions");
            DropIndex("dbo.Adverts", new[] { "TrimConditionId" });
            DropColumn("dbo.PriceFilters", "AdvertType");
            DropColumn("dbo.Adverts", "Type");
            DropColumn("dbo.Adverts", "TrimConditionId");
            DropTable("dbo.TrimConditions");
        }
    }
}
