namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminfb : DbMigration
    {
        public override void Up()
        {            
            CreateTable(
                "dbo.AdminFeedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Adress = c.String(),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);            
        }

        public override void Down()
        {
            DropTable("dbo.AdminFeedbacks");
        }
    }
}
