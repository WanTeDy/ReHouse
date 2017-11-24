namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkvideo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SliderParams", "VideoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SliderParams", "VideoUrl");
        }
    }
}
