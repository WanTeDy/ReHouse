namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameBusinessOperation = c.String(),
                        RussianNameOperation = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        RussianName = c.String(maxLength: 30),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        FatherName = c.String(),
                        Adress = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        TokenHash = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TelePhone = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RoleAuthorities",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Authority_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Authority_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authorities", t => t.Authority_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Authority_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Phones", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleAuthorities", "Authority_Id", "dbo.Authorities");
            DropForeignKey("dbo.RoleAuthorities", "Role_Id", "dbo.Roles");
            DropIndex("dbo.RoleAuthorities", new[] { "Authority_Id" });
            DropIndex("dbo.RoleAuthorities", new[] { "Role_Id" });
            DropIndex("dbo.Phones", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropTable("dbo.RoleAuthorities");
            DropTable("dbo.Phones");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Authorities");
        }
    }
}
