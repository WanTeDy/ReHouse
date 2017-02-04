namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filters3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adverts", "PriceFilterId", "dbo.PriceFilters");
            DropIndex("dbo.Adverts", new[] { "PriceFilterId" });
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
            
            AddColumn("dbo.PriceFilters", "Min", c => c.Int(nullable: false));
            AddColumn("dbo.PriceFilters", "Max", c => c.Int(nullable: false));
            DropColumn("dbo.Adverts", "PriceFilterId");
            DropColumn("dbo.PriceFilters", "RussianName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceFilters", "RussianName", c => c.String());
            AddColumn("dbo.Adverts", "PriceFilterId", c => c.Int(nullable: false));
            DropColumn("dbo.PriceFilters", "Max");
            DropColumn("dbo.PriceFilters", "Min");
            DropTable("dbo.PriceFilterNewBuildings");
            CreateIndex("dbo.Adverts", "PriceFilterId");
            AddForeignKey("dbo.Adverts", "PriceFilterId", "dbo.PriceFilters", "Id", cascadeDelete: true);
        }
    }
}
