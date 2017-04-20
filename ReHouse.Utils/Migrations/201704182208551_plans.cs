namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Url = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlanImageAdverts",
                c => new
                    {
                        PlanImage_Id = c.Int(nullable: false),
                        Advert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanImage_Id, t.Advert_Id })
                .ForeignKey("dbo.PlanImages", t => t.PlanImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Adverts", t => t.Advert_Id, cascadeDelete: true)
                .Index(t => t.PlanImage_Id)
                .Index(t => t.Advert_Id);
            
            CreateTable(
                "dbo.PlanImageNewBuildings",
                c => new
                    {
                        PlanImage_Id = c.Int(nullable: false),
                        NewBuilding_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanImage_Id, t.NewBuilding_Id })
                .ForeignKey("dbo.PlanImages", t => t.PlanImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.NewBuildings", t => t.NewBuilding_Id, cascadeDelete: true)
                .Index(t => t.PlanImage_Id)
                .Index(t => t.NewBuilding_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanImageNewBuildings", "NewBuilding_Id", "dbo.NewBuildings");
            DropForeignKey("dbo.PlanImageNewBuildings", "PlanImage_Id", "dbo.PlanImages");
            DropForeignKey("dbo.PlanImageAdverts", "Advert_Id", "dbo.Adverts");
            DropForeignKey("dbo.PlanImageAdverts", "PlanImage_Id", "dbo.PlanImages");
            DropIndex("dbo.PlanImageNewBuildings", new[] { "NewBuilding_Id" });
            DropIndex("dbo.PlanImageNewBuildings", new[] { "PlanImage_Id" });
            DropIndex("dbo.PlanImageAdverts", new[] { "Advert_Id" });
            DropIndex("dbo.PlanImageAdverts", new[] { "PlanImage_Id" });
            DropTable("dbo.PlanImageNewBuildings");
            DropTable("dbo.PlanImageAdverts");
            DropTable("dbo.PlanImages");
        }
    }
}
