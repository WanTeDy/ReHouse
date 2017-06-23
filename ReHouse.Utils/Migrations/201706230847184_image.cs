namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Alt", c => c.String());
            AddColumn("dbo.Images", "Title", c => c.String());
            AddColumn("dbo.PlanImages", "Alt", c => c.String());
            AddColumn("dbo.PlanImages", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanImages", "Title");
            DropColumn("dbo.PlanImages", "Alt");
            DropColumn("dbo.Images", "Title");
            DropColumn("dbo.Images", "Alt");
        }
    }
}
