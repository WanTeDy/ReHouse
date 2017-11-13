namespace ReHouse.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using DataBase;
    using System.Linq;

    public partial class summorypatch : DbMigration
    {
        public override void Up()
        {
            using (var db = new DbReHouse())
            {
                var buildings = db.NewBuildings.ToList();
                foreach (var build in buildings)
                {
                    var date = db.ExpluatationDates.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id <= build.ExpluatationDateId && x.Year != 0);
                    if (date != null)
                        build.ExpluatationDate = date;
                }
                db.SaveChanges();
                var dates = db.ExpluatationDates.Where(x => x.Year == 0);
                db.ExpluatationDates.RemoveRange(dates);
                db.SaveChanges();

                db.SliderParams.Add(new DataBase.Common.SliderParam());
                db.SliderParams.Add(new DataBase.Common.SliderParam());
                db.SliderParams.Add(new DataBase.Common.SliderParam());
                db.SliderParams.Add(new DataBase.Common.SliderParam());
                db.SaveChanges();
            }
        }

        public override void Down()
        {
        }
    }
}
