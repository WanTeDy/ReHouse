namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class advert_markettype1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adverts", "MarketTypeId", "dbo.MarketTypes");
            DropIndex("dbo.Adverts", new[] { "MarketTypeId" });
            AlterColumn("dbo.Adverts", "MarketTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Adverts", "MarketTypeId");
            AddForeignKey("dbo.Adverts", "MarketTypeId", "dbo.MarketTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adverts", "MarketTypeId", "dbo.MarketTypes");
            DropIndex("dbo.Adverts", new[] { "MarketTypeId" });
            AlterColumn("dbo.Adverts", "MarketTypeId", c => c.Int());
            CreateIndex("dbo.Adverts", "MarketTypeId");
            AddForeignKey("dbo.Adverts", "MarketTypeId", "dbo.MarketTypes", "Id");
        }
    }
}
