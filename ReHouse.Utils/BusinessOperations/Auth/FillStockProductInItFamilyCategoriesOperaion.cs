using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.Auth
{
    public class FillStockProductInItFamilyCategoriesOperaion : BaseEntityDapperOperation
    {
        protected override void InTransaction()
        {
            var random = new Random();
            var cats = Context.ItFamilyCategories.Where(x => !x.Deleted).ToList();

            var catit = new List<ItFamilyCategory>();
            catit.AddRange(cats.Where(x => x.ItFamilyParentId == null).Select(x => new ItFamilyCategory
            {
                Id = x.Id,
                ItFamilyParentId = x.ItFamilyParentId,
                CategoryId = x.CategoryId,
                HasRule = x.HasRule,
                Name = x.Name,
                ParentId = x.ParentId,
                BrainProduct_Id = x.BrainProduct_Id,
                FromWhatProvider = x.FromWhatProvider,
                Categories = cats.Where(y => y.ItFamilyParentId == x.Id).Select(d => new ItFamilyCategory
                {
                    BrainProduct_Id = d.BrainProduct_Id,
                    CategoryId = d.CategoryId,
                    HasRule = d.HasRule,
                    Id = d.Id,
                    Name = d.Name,
                    ParentId = d.ParentId,
                    ItFamilyParentId = d.ItFamilyParentId,
                    FromWhatProvider = d.FromWhatProvider,
                    //BrainProductModel = IsSite ? helper.FormBrainProductModel(Context, brModels.FirstOrDefault(brMod => brMod.Id == d.BrainProduct_Id), cont) : null,
                    Categories = cats.Where(q => q.ItFamilyParentId == d.Id).Select(q => new ItFamilyCategory
                    {
                        Id = q.Id,
                        ParentId = q.ParentId,
                        BrainProduct_Id = q.BrainProduct_Id,
                        CategoryId = q.CategoryId,
                        HasRule = q.HasRule,
                        ItFamilyParentId = q.ItFamilyParentId,
                        Name = q.Name,
                        FromWhatProvider = q.FromWhatProvider,
                        Categories = cats.Where(a => a.ItFamilyParentId == q.Id).Select(a => new ItFamilyCategory
                        {
                            Id = a.Id,
                            ParentId = a.ParentId,
                            BrainProduct_Id = a.BrainProduct_Id,
                            CategoryId = a.CategoryId,
                            HasRule = a.HasRule,
                            ItFamilyParentId = a.ItFamilyParentId,
                            Name = a.Name,
                            FromWhatProvider = a.FromWhatProvider
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList());

            foreach (var itFamilyCategory in catit)
            {
                foreach (var familyCategory in itFamilyCategory.Categories)
                {
                    if (!familyCategory.Categories.Any())
                    {
                        var prods = Gateway.GetStockProductsFromCategory(familyCategory.Id, 0, 1, "Name", "ASC", " AND st.IsAvailable = 1");
                        var products = prods.Read<StockProduct>().ToList();
                        var count = prods.Read<int>().Single();
                        if (count <= 1) continue;
                        var skip = random.Next(0, count - 1);
                        prods = Gateway.GetStockProductsFromCategory(familyCategory.Id, skip, 1, "Name", "ASC", " AND st.IsAvailable = 1");
                        products = prods.Read<StockProduct>().ToList();
                        count = prods.Read<int>().Single();
                        if (products.Any())
                        {
                            var id = products[0].Id;
                            var prod = Context.StockProducts.FirstOrDefault(x => x.Id == id);
                            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == familyCategory.Id);
                            if (cat != null && prod != null)
                                cat.BrainProduct = prod;
                        }
                    }
                }
            }
            Context.SaveChanges();
        }
    }
}