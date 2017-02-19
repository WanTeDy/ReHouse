namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class advert_markettype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RussianName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Adverts", "MarketTypeId", c => c.Int());
            CreateIndex("dbo.Adverts", "MarketTypeId");
            AddForeignKey("dbo.Adverts", "MarketTypeId", "dbo.MarketTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adverts", "MarketTypeId", "dbo.MarketTypes");
            DropIndex("dbo.Adverts", new[] { "MarketTypeId" });
            DropColumn("dbo.Adverts", "MarketTypeId");
            DropTable("dbo.MarketTypes");
        }
    }
}
