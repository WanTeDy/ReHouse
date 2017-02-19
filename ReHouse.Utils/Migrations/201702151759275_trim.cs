namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trim : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adverts", "TrimConditionId", "dbo.TrimConditions");
            DropIndex("dbo.Adverts", new[] { "TrimConditionId" });
            AlterColumn("dbo.Adverts", "TrimConditionId", c => c.Int());
            CreateIndex("dbo.Adverts", "TrimConditionId");
            AddForeignKey("dbo.Adverts", "TrimConditionId", "dbo.TrimConditions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adverts", "TrimConditionId", "dbo.TrimConditions");
            DropIndex("dbo.Adverts", new[] { "TrimConditionId" });
            AlterColumn("dbo.Adverts", "TrimConditionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Adverts", "TrimConditionId");
            AddForeignKey("dbo.Adverts", "TrimConditionId", "dbo.TrimConditions", "Id", cascadeDelete: true);
        }
    }
}
