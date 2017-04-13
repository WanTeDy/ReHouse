namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avatars", "UserId", "dbo.Users");
            AddColumn("dbo.Users", "AvatarId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Avatars", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avatars", "UserId", "dbo.Users");
            DropColumn("dbo.Users", "AvatarId");
            AddForeignKey("dbo.Avatars", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
