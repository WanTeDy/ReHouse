namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vac : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vacancies");
        }
    }
}
