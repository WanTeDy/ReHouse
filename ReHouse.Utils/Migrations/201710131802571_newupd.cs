namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adverts", "PublicationDate", c => c.DateTime());
            AddColumn("dbo.NewBuildings", "PublicationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewBuildings", "PublicationDate");
            DropColumn("dbo.Adverts", "PublicationDate");
        }
    }
}
