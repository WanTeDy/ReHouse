namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpluatationDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.NewBuildings", "ExpluatationDateId", c => c.Int(nullable: false));
            CreateIndex("dbo.NewBuildings", "ExpluatationDateId");
            AddForeignKey("dbo.NewBuildings", "ExpluatationDateId", "dbo.ExpluatationDates", "Id", cascadeDelete: true);
            DropColumn("dbo.NewBuildings", "ExpluatationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewBuildings", "ExpluatationDate", c => c.String());
            DropForeignKey("dbo.NewBuildings", "ExpluatationDateId", "dbo.ExpluatationDates");
            DropIndex("dbo.NewBuildings", new[] { "ExpluatationDateId" });
            DropColumn("dbo.NewBuildings", "ExpluatationDateId");
            DropTable("dbo.ExpluatationDates");
        }
    }
}
