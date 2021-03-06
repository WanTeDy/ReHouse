﻿using System.Data.Entity;
//using ReHouse.Utils.DataBase.CreditInformation;
//using ReHouse.Utils.DataBase.Currencies;
//using ReHouse.Utils.DataBase.Filters;
//using ReHouse.Utils.DataBase.ModelForUI;
//using ReHouse.Utils.DataBase.OtherOurDataForDb;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Vacancies;

namespace ReHouse.Utils.DataBase
{
    public class DbReHouse : DbContext
    {
        public DbReHouse()
        //:base("Local")
        : base("smarterAsp")
        { }
        //    public DbSet<UnitOfCommodity> UnitOfCommodities { get; set; }
        //    public DbSet<OrderCities> OrderCities { get; set; }
        //    public DbSet<PathImages> PathImageses { get; set; }

        public DbSet<AdminFeedback> AdminFeedbacks { get; set; }
        public DbSet<ExpluatationDate> ExpluatationDates { get; set; }
        public DbSet<TagPage> TagPages { get; set; }
        public DbSet<SliderParam> SliderParams { get; set; }
        public DbSet<PageText> PageTexts { get; set; }
        public DbSet<SeoParam> SeoParams { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<UserEmailMessage> Emails { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<NewBuilding> NewBuildings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MarketType> MarketTypes { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<TrimCondition> TrimConditions { get; set; }
        //public DbSet<PriceFilterNewBuilding> PriceFilterNewBuildings { get; set; }
        public DbSet<PriceFilter> PriceFilters { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PlanImage> PlanImages { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdvertProperty> AdvertProperties { get; set; }
        public DbSet<AdvertPropertyValue> AdvertPropertyValues { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Fluent API EF
            //        modelBuilder.Entity<OrdersItemForBrain>().Property(m => m.UnitOfCommodityId).IsOptional(); //for nulable


            //        modelBuilder.Entity<Currency>().Property(x => x.Value).HasPrecision(7, 4);
            //        modelBuilder.Entity<OurStocks.StockProduct>().Property(x => x.Price).HasPrecision(8, 2);
            //        modelBuilder.Entity<OurStocks.StockProduct>().Property(x => x.PriceUah).HasPrecision(11, 2);
            //        modelBuilder.Entity<OurStocks.StockProduct>().Property(x => x.PriceUsdForManager).HasPrecision(8, 2);
            //        modelBuilder.Entity<OurStocks.StockProduct>().Property(x => x.PriceUsdForPartner).HasPrecision(8, 2);
            //        modelBuilder.Entity<OurStocks.StockProduct>().Property(x => x.PriceUsdForClients).HasPrecision(8, 2);

            //        modelBuilder.Entity<BrainProduct>().Property(x => x.price).HasPrecision(8, 2);
            //        modelBuilder.Entity<BrainProduct>().Property(x => x.price_uah).HasPrecision(11, 2);

            //        //one-to-many
            //        modelBuilder.Entity<Contractor>().HasRequired<Role>(s => s.Role)
            //        .WithMany(s => s.Contractors).HasForeignKey(s => s.RoleId);


            //        //one-to-many
            //        modelBuilder.Entity<RuleForPrice>().HasRequired<Role>(s => s.ForWhom)
            //        .WithMany(s => s.RulesForPrice).HasForeignKey(s => s.ForWhomId);
            //        modelBuilder.Entity<Role>().HasMany(s => s.Authorities).WithMany(x => x.Roles);

            //        //one-to-many
            //        modelBuilder.Entity<CustomerCard>().HasRequired<Contractor>(s => s.Contractor)
            //        .WithMany(s => s.CustomerCards).HasForeignKey(s => s.ContractorId);
            //        //modelBuilder.Entity<CustomerCard>().HasRequired<Entrepreneur>(s => s.Entrepreneur)
            //        //.WithMany(s => s.CustomerCards).HasForeignKey(s => s.PartnerId);


            //        //modelBuilder.Entity<CustomerCard>()
            //        //    .HasOptional(a => a.OrderComes)
            //        //    .WithMany()
            //        //    .HasForeignKey(u => u.OrderComesId).WillCascadeOnDelete(false);
            //        //modelBuilder.Entity<CustomerCard>()
            //        //    .HasOptional(a => a.ComesMoney)
            //        //    .WithMany()
            //        //    .HasForeignKey(u => u.ComesMoneyId).WillCascadeOnDelete(false);
            //        modelBuilder.Entity<CustomerCard>()
            //        .HasOptional<OrderComes>(u => u.OrderComes)
            //        .WithOptionalDependent(c => c.CustomerCard).Map(p => p.MapKey("OrderComesId"));
            //        
            modelBuilder.Entity<User>()
                    .HasOptional<Avatar>(u => u.Avatar)
                    .WithRequired(c => c.User).Map(p => p.MapKey("UserId"));
            //        //modelBuilder.Entity<CustomerCard>().HasOptional(s=>s.OrderComes).WithRequired(ad=>ad.CustomerCard);
            //        //modelBuilder.Entity<CustomerCard>().HasOptional(s=>s.ComesMoney).WithRequired(ad=>ad.CustomerCard);

            //        //one-to-many 
            //        //modelBuilder.Entity<Client>().HasRequired<Role>(s => s.Role)
            //        //.WithMany(s => s.Clients).HasForeignKey(s => s.RoleId);

            //        //hierarchy
            //        modelBuilder.Entity<BrainCategory>()
            //            .HasMany(d => d.Categories)
            //            .WithOptional(x => x.Parent).HasForeignKey(y => y.BrainParentID);

            //        //.Map(y => y.MapKey("BrainParentID_foreign"));
            //            //.HasOptional(d => d.Parent).WithMany(x => x.Categories).
            //        //Map(y => y.MapKey("BrainParentID")).WillCascadeOnDelete(false);

            //        //one-to-many 
            //        modelBuilder.Entity<Services>().HasRequired<FrequencyPayment>(s => s.FrequencyPayment).WithMany(s => s.Serviceses).HasForeignKey(s => s.FrequencyPaymentId);

            //        //one-to-many 
            //        modelBuilder.Entity<Specifications>().HasRequired<BrainProduct>(s => s.BrainProduct).WithMany(s => s.options).HasForeignKey(s => s.BrainProductId);

            //        //one-to-many 
            //        modelBuilder.Entity<BrainProduct>().HasRequired<BrainCategory>(s => s.BrainCategory).WithMany(s => s.BrainProducts).HasForeignKey(s => s.BrainCategoryID);

            //        //one-to-many 
            //        modelBuilder.Entity<BrainProduct>().HasRequired<Vendor>(s => s.Vendor).WithMany(s => s.BrainProducts).HasForeignKey(s => s.BrainVendorID);

            //        //many-to-many
            modelBuilder.Entity<NewBuilding>()
                .HasMany(s => s.Images)
                .WithMany(x => x.NewBuildings)
                .Map(m =>
                {
                    m.MapLeftKey("NewBuilding_Id");
                    m.MapRightKey("Image_Id");
                    m.ToTable("ImageNewBuildings");
                });

            modelBuilder.Entity<Advert>()
               .HasMany(s => s.Images)
               .WithMany(x => x.Adverts)
               .Map(m =>
               {
                   m.MapLeftKey("Advert_Id");
                   m.MapRightKey("Image_Id");
                   m.ToTable("ImageAdverts");
               });
            //HasRequired<BrainCategory>(s => s.BrainCategories).WithMany(s => s.Vendors).HasForeignKey(s => s.categoryID);

            //        //many-to-many
            //        modelBuilder.Entity<BrainStocks>().HasMany(x => x.BrainProducts).WithMany(p => p.BrainStockses);

            //        //hierarchy
            //        modelBuilder.Entity<ReHouseCategory>()
            //            .HasMany(d => d.Categories)
            //            .WithOptional(x => x.Parent).HasForeignKey(y => y.ReHouseParentId);
            //        //many-to-many
            //        modelBuilder.Entity<ReHouseVendor>().HasMany(s => s.ReHouseCategories).WithMany(x => x.ReHouseVendors);
            //        //one-to-many 
            //        modelBuilder.Entity<OurStocks.StockProduct>().HasRequired<ReHouseVendor>(s => s.ReHouseVendor)
            //            .WithMany(s => s.StockProducts).HasForeignKey(s => s.ReHouseVendorId);
            //        //one-to-many 
            //        modelBuilder.Entity<OurStocks.StockProduct>().HasRequired<ReHouseCategory>(s => s.ReHouseCategory)
            //            .WithMany(s => s.StockProducts).HasForeignKey(s => s.ReHouseCategoryId);


            //        //one-to-many 
            //        //modelBuilder.Entity<RuleForPrice>().HasRequired<RulesForCategory>(s => s.RulesForCategory)
            //        //    .WithMany(s => s.RulesForPrice).HasForeignKey(s => s.RulesForCategoryId);
            //        //one-to-many 
            //        //modelBuilder.Entity<Currency>().HasRequired<CollectionCurrency>(s => s.CollectionCurrency)
            //        //    .WithMany(s => s.Currencies).HasForeignKey(s => s.CollectionCurrencyId);


            //        modelBuilder.Entity<RuleForPrice>().HasOptional<ReHouseCategory>(x => x.Category)
            //            .WithMany(s => s.RulesForPrice).HasForeignKey(s => s.OurCategoryId).WillCascadeOnDelete(false);

            //        //one-to-many 
            //        modelBuilder.Entity<UnitOfCommodity>().HasRequired<OurStocks.OurStockRoom>(s => s.OurStockRoom)
            //            .WithMany(s => s.UnitOfCommodities).HasForeignKey(s => s.OurStockRoomId);
            //        //many-to-many
            //        //modelBuilder.Entity<OurStocks.UnitOfCommodity>().HasMany(s => s.OurStockRooms).WithMany(x => x.UnitOfCommodities);
            //        //one-to-many 
            //        modelBuilder.Entity<ProductPropertyValues>().HasRequired<OurStocks.StockProduct>(s => s.StockProduct)
            //            .WithMany(s => s.PropertyValueses).HasForeignKey(s => s.StockProductId);
            //        //one-to-many 
            //        modelBuilder.Entity<ProductPropertyValues>().HasRequired<ProductProperty>(s => s.ProductProperty)
            //            .WithMany(s => s.ProductPropertyValues).HasForeignKey(s => s.PropertyId);
            //        //one-to-many 
            //        modelBuilder.Entity<UnitOfCommodity>().HasRequired<OurStocks.StockProduct>(s => s.StockProduct)
            //            .WithMany(s => s.UnitOfCommodities).HasForeignKey(s => s.StockProductId);
            //        //one-to-many 
            //        modelBuilder.Entity<ProductProperty>().HasRequired<ReHouseCategory>(s => s.ReHouseCategory)
            //            .WithMany(s => s.ProductProperties).HasForeignKey(s => s.CategoryId);

            //        modelBuilder.Entity<AdditionalStockProductData>()
            //        .HasOptional<StockProduct>(u => u.StockProduct)
            //        .WithOptionalDependent(c => c.AdditionalData).Map(p => p.MapKey("StockProduct_Id"));

            //        //one-to-many 
            //        modelBuilder.Entity<PathImages>().HasRequired<AdditionalStockProductData>(s => s.AdditionalData)
            //            .WithMany(s => s.PathImageses).HasForeignKey(s => s.AdditionalDataId);
            //        //one-to-many
            //        modelBuilder.Entity<PathImages>().HasRequired<BrainProduct>(s => s.BrainProduct)
            //        .WithMany(s => s.PathImageses).HasForeignKey(s => s.BrainProductId);
        }
    }
}