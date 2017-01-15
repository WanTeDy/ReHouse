using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.BussOpWithDapper
{
    public class LoadItFamilyCategoriesOperation : BaseEntityDapperOperation
    {
        private String TokenHash { get; set; }
        private Int32 OurStockRoomId { get; set; }
        public List<ItFamilyCategory> ItFamilyCategories { get; set; }

        public LoadItFamilyCategoriesOperation(string tokenHash, int ourStockRoomId)
        {
            TokenHash = tokenHash;
            OurStockRoomId = ourStockRoomId;
        }

        private List<ItFamilyCategory> Recurs(List<ItFamilyCategory> outCategory, List<ItFamilyCategory> sourceCategory)
        {
            foreach (var categoriesModel in outCategory)
            {
                var model = categoriesModel;
                var addCategories = sourceCategory.Where(x => x.ParentId == model.CategoryId).ToList();
                if (addCategories.Count > 0)
                {
                    foreach (var category in addCategories)
                    {
                        if (category.StockProducts.Any(x=>!x.Deleted && x.FromWhatProvider == FromWhatProvider.OurProduct) )
                        {
                            categoriesModel.Categories.Add(new ItFamilyCategory
                            {
                                Id = category.Id,
                                ItFamilyParentId = category.ItFamilyParentId,
                                CategoryId = category.CategoryId,
                                Name = category.Name,
                                ParentId = category.ParentId,
                                FromWhatProvider = FromWhatProvider.OurProduct,
                                
                            });
                        }
                        else if (category.Categories.Count > 0)
                        {
                            if (category.Categories.Count(x => x.StockProducts.Any(y=>!y.Deleted && y.FromWhatProvider == FromWhatProvider.OurProduct)) > 0)
                            {
                                categoriesModel.Categories.Add(new ItFamilyCategory
                                {
                                    Id = category.Id,
                                    ItFamilyParentId = category.ItFamilyParentId,
                                    CategoryId = category.CategoryId,
                                    Name = category.Name,
                                    ParentId = category.ParentId,
                                    FromWhatProvider = FromWhatProvider.OurProduct,
                                });
                            }
                        }
                    }
                    //categoriesModel.Categories.AddRange(addCategories);
                }
                Recurs(categoriesModel.Categories, sourceCategory);
            }
            return outCategory;
        }
        protected override void InTransaction()
        {
            //CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var cats = Gateway.GetItFamilyCategoriesWithUnitsForoOurStock(OurStockRoomId).ToList();//Gateway.GetItFamilyCategoriesWithParams((int)FromWhatProvider.OurProduct).ToList();

            var catsOut = new List<ItFamilyCategory>();
            if (cats.Count > 0)
                catsOut.AddRange(cats.Where(x => x.ItFamilyParentId == null).Select(x => new ItFamilyCategory
                {
                    Id = x.Id,
                    ItFamilyParentId = x.ItFamilyParentId,
                    Name = x.Name,
                    Categories = cats.Where(y => y.ItFamilyParentId == x.Id).Select(d => new ItFamilyCategory
                    {
                        Id = d.Id,
                        Name = d.Name,
                        ItFamilyParentId = d.ItFamilyParentId,
                        //BrainProductModel = IsSite ? helper.FormBrainProductModel(Context, brModels.FirstOrDefault(brMod => brMod.Id == d.BrainProduct_Id), cont) : null,
                        Categories = cats.Where(q => q.ItFamilyParentId == d.Id).Select(q => new ItFamilyCategory
                        {
                            Id = q.Id,
                            ItFamilyParentId = q.ItFamilyParentId,
                            Name = q.Name,
                            Categories = cats.Where(a => a.ItFamilyParentId == q.Id).Select(a => new ItFamilyCategory
                            {
                                Id = a.Id,
                                ItFamilyParentId = a.ItFamilyParentId,
                                Name = a.Name,
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList());

            ItFamilyCategories = catsOut;
            //Context.Configuration.AutoDetectChangesEnabled = false;
            //Context.Configuration.ValidateOnSaveEnabled = false;
            //var categories = Context.ItFamilyCategories.Where(x => !x.Deleted).ToList();
            //
            //var hierarchy = categories.Where(x => x.ParentId == 1 || x.ParentId == 0 && !x.Deleted).ToList();
            //var newEl = hierarchy.Select(brainCategory => new ItFamilyCategory
            //{
            //    Id = brainCategory.Id,
            //    ItFamilyParentId = brainCategory.ItFamilyParentId,
            //    CategoryId = brainCategory.CategoryId,
            //    Name = brainCategory.Name,
            //    ParentId = brainCategory.ParentId,
            //}).ToList();
            //foreach (var brainCategory in hierarchy)
            //    categories.Remove(brainCategory);
            //var cat = Recurs(newEl, categories);
            //ItFamilyCategories = new List<ItFamilyCategory>();
            //foreach (var itFamilyCategory in cat.Where(itFamilyCategory => itFamilyCategory.Categories.Count > 0))
            //    ItFamilyCategories.Add(itFamilyCategory);
        }
    }
}