namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminfb1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ImageNewBuildings", newName: "NewBuildingImages");
            RenameTable(name: "dbo.ImageAdverts", newName: "AdvertImages");
            RenameTable(name: "dbo.AdvertImages", newName: "ImageAdverts");
            RenameTable(name: "dbo.NewBuildingImages", newName: "ImageNewBuildings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AdvertImages", newName: "ImageAdverts");
            RenameTable(name: "dbo.NewBuildingImages", newName: "ImageNewBuildings");
            RenameTable(name: "dbo.ImageNewBuildings", newName: "NewBuildingImages");
            RenameTable(name: "dbo.ImageAdverts", newName: "AdvertImages");
        }
    }
}
