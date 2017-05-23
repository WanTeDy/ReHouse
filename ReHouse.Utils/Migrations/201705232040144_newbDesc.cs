namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newbDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewBuildings", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewBuildings", "Description");
        }
    }
}
