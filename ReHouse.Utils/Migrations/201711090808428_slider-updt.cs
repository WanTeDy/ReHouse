namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sliderupdt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MessengerName", c => c.String());
            AddColumn("dbo.SliderParams", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SliderParams", "Text");
            DropColumn("dbo.Users", "MessengerName");
        }
    }
}
