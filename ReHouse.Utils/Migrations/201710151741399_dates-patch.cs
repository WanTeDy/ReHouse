namespace ReHouse.Utils.Migrations
{
    using DataBase;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class datespatch : DbMigration
    {
        public override void Up()
        {
            using (var db = new DbReHouse())
            {
                var buildings = db.NewBuildings.ToList();
                foreach(var build in buildings)
                {
                    var date = db.ExpluatationDates.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id <= build.ExpluatationDateId && x.Year != 0);
                    if (date != null)
                        build.ExpluatationDate = date;
                }
                db.SaveChanges();
                var dates = db.ExpluatationDates.Where(x => x.Year == 0);
                db.ExpluatationDates.RemoveRange(dates);
                db.SaveChanges();
            }
        }
        
        public override void Down()
        {
        }
    }
}
