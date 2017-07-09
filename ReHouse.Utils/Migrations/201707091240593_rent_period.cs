namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rent_period : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adverts", "RentPeriodType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adverts", "RentPeriodType");
        }
    }
}
