namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class advert_prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvertProperties", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdvertProperties", "Priority");
        }
    }
}
