namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pageactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagPages", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TagPages", "IsActive");
        }
    }
}
