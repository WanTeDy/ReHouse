namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpluatationDates", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "OrderByField", c => c.Int(nullable: false));
            RenameColumn("dbo.Adverts", "PublicationDate", "CreationDate");
            RenameColumn("dbo.NewBuildings", "PublicationDate", "CreationDate");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "OrderByField");
            DropColumn("dbo.ExpluatationDates", "Year");
            RenameColumn("dbo.NewBuildings", "CreationDate", "PublicationDate");
            RenameColumn("dbo.Adverts", "CreationDate", "PublicationDate");
        }
    }
}
