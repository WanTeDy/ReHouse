namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avatars", "UserId", "dbo.Users");
            DropIndex("dbo.Avatars", new[] { "UserId" });
            DropColumn("dbo.Avatars", "Id");
            RenameColumn(table: "dbo.Avatars", name: "UserId", newName: "Id");
            DropPrimaryKey("dbo.Avatars");
            AddColumn("dbo.Users", "AvatarId", c => c.Int(nullable: false));
            AlterColumn("dbo.Avatars", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Avatars", "Id");
            CreateIndex("dbo.Avatars", "Id");
            AddForeignKey("dbo.Avatars", "Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avatars", "Id", "dbo.Users");
            DropIndex("dbo.Avatars", new[] { "Id" });
            DropPrimaryKey("dbo.Avatars");
            AlterColumn("dbo.Avatars", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "AvatarId");
            AddPrimaryKey("dbo.Avatars", "Id");
            RenameColumn(table: "dbo.Avatars", name: "Id", newName: "UserId");
            AddColumn("dbo.Avatars", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Avatars", "UserId");
            AddForeignKey("dbo.Avatars", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
