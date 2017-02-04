namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newbuildings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Builders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewBuildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Adress = c.String(),
                        Price = c.Int(nullable: false),
                        HouseQuantity = c.Int(nullable: false),
                        SectionQuantity = c.Int(nullable: false),
                        FloatQuantity = c.Int(nullable: false),
                        FloorQuantity = c.Int(nullable: false),
                        ExpluatationDate = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        Construct = c.String(),
                        WallMaterial = c.String(),
                        WallHeight = c.Double(nullable: false),
                        Heating = c.String(),
                        Parking = c.String(),
                        YouTubeUrl = c.String(),
                        Url = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NewBuildingBuilders",
                c => new
                    {
                        NewBuilding_Id = c.Int(nullable: false),
                        Builder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewBuilding_Id, t.Builder_Id })
                .ForeignKey("dbo.NewBuildings", t => t.NewBuilding_Id, cascadeDelete: true)
                .ForeignKey("dbo.Builders", t => t.Builder_Id, cascadeDelete: true)
                .Index(t => t.NewBuilding_Id)
                .Index(t => t.Builder_Id);
            
            AddColumn("dbo.Adverts", "PublicationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Images", "NewBuilding_Id", c => c.Int());
            AddColumn("dbo.Phones", "NewBuilding_Id", c => c.Int());
            AddColumn("dbo.Phones", "Builder_Id", c => c.Int());
            CreateIndex("dbo.Images", "NewBuilding_Id");
            CreateIndex("dbo.Phones", "NewBuilding_Id");
            CreateIndex("dbo.Phones", "Builder_Id");
            AddForeignKey("dbo.Images", "NewBuilding_Id", "dbo.NewBuildings", "Id");
            AddForeignKey("dbo.Phones", "NewBuilding_Id", "dbo.NewBuildings", "Id");
            AddForeignKey("dbo.Phones", "Builder_Id", "dbo.Builders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "Builder_Id", "dbo.Builders");
            DropForeignKey("dbo.NewBuildings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Phones", "NewBuilding_Id", "dbo.NewBuildings");
            DropForeignKey("dbo.Images", "NewBuilding_Id", "dbo.NewBuildings");
            DropForeignKey("dbo.NewBuildingBuilders", "Builder_Id", "dbo.Builders");
            DropForeignKey("dbo.NewBuildingBuilders", "NewBuilding_Id", "dbo.NewBuildings");
            DropIndex("dbo.NewBuildingBuilders", new[] { "Builder_Id" });
            DropIndex("dbo.NewBuildingBuilders", new[] { "NewBuilding_Id" });
            DropIndex("dbo.NewBuildings", new[] { "UserId" });
            DropIndex("dbo.Phones", new[] { "Builder_Id" });
            DropIndex("dbo.Phones", new[] { "NewBuilding_Id" });
            DropIndex("dbo.Images", new[] { "NewBuilding_Id" });
            DropColumn("dbo.Phones", "Builder_Id");
            DropColumn("dbo.Phones", "NewBuilding_Id");
            DropColumn("dbo.Images", "NewBuilding_Id");
            DropColumn("dbo.Adverts", "PublicationDate");
            DropTable("dbo.NewBuildingBuilders");
            DropTable("dbo.NewBuildings");
            DropTable("dbo.Builders");
        }
    }
}
