namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewBuildings", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewBuildings", "Name");
        }
    }
}
