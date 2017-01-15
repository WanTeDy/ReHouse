namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "Name", c => c.String(maxLength: 30));
        }
    }
}
