namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagetext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageTexts", "FullUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageTexts", "FullUrl");
        }
    }
}
