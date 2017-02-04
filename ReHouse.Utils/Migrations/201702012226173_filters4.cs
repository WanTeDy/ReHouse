namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filters4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BuilderNewBuildings", newName: "NewBuildingBuilders");
            DropPrimaryKey("dbo.NewBuildingBuilders");
            AddPrimaryKey("dbo.NewBuildingBuilders", new[] { "NewBuilding_Id", "Builder_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.NewBuildingBuilders");
            AddPrimaryKey("dbo.NewBuildingBuilders", new[] { "Builder_Id", "NewBuilding_Id" });
            RenameTable(name: "dbo.NewBuildingBuilders", newName: "BuilderNewBuildings");
        }
    }
}
