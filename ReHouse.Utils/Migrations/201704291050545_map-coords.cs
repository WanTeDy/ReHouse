namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapcoords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adverts", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adverts", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Adverts", "IsExclusive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Adverts", "IsModerated", c => c.Boolean(nullable: false));
            AddColumn("dbo.NewBuildings", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.NewBuildings", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.NewBuildings", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.NewBuildings", "IsExclusive", c => c.Boolean(nullable: false));
            AddColumn("dbo.NewBuildings", "IsModerated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewBuildings", "IsModerated");
            DropColumn("dbo.NewBuildings", "IsExclusive");
            DropColumn("dbo.NewBuildings", "IsHot");
            DropColumn("dbo.NewBuildings", "Longitude");
            DropColumn("dbo.NewBuildings", "Latitude");
            DropColumn("dbo.Adverts", "IsModerated");
            DropColumn("dbo.Adverts", "IsExclusive");
            DropColumn("dbo.Adverts", "Longitude");
            DropColumn("dbo.Adverts", "Latitude");
        }
    }
}
