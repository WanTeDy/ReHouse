using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper.ForDbTypes;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper.Helpers;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.BussOpWithDapper
{
    public class GetFormedItfamilyCategories : BaseEntityDapperOperation
    {
        public String TokenHash { get; set; }
        public Boolean IsSite { get; set; }
        public List<ItFamilyCategory> ItFamilyCategories { get; set; }
        public Boolean IsProvider { get; set; }
        public GetFormedItfamilyCategories(bool isSite, bool isProvider, string tokenHash = "")
        {
            TokenHash = tokenHash;
            IsSite = isSite;
            IsProvider = isProvider;
        }

        protected override void InTransaction()
        {
            var cont = Context.Contractors.Include("Role").FirstOrDefault(x => !x.Deleted && x.IsActive && x.TokenHash == TokenHash);
            var cats = Gateway.GetItFamilyCategories().ToList();
            var helper = new HelperFormPriceForModelCategories(Context, cont);
            //for one brainProductModel
            //foreach (var brainProductModel in brModels)
            //{
            //    brainProductModel.IsAvailable = brainProductModel.IsAv ? "В наличии" : "Недоступен";
            //}
            //foreach (var i in catsBr)
            //{
            //    var brModel = Gateway.GetBrainProductModel(i).ToList();
            //    if (brModel.Any())
            //    {
            //        brModel[0].IsAvailable = brModel[0].IsAv ? "В наличии" : "Недоступен";
            //        brModels.AddRange(brModel);
            //    }
            //}

            //Закоментировал потом проверю
            //var brModels = new List<BrainProductModel>();
            //if (IsSite)
            //{
            //    var catsBr = cats.Where(x => x.BrainProduct_Id.HasValue && x.BrainProduct_Id.Value != 0)
            //        .Select(x => new TablesOfId { Id = x.BrainProduct_Id.Value }).ToList();
            //    
            //    var prod = catsBr.AsTableValuedParameter("dbo.TableOfId", new[] { "Id" });
            //    brModels = Gateway.GetProductModelFromListStockId(prod).ToList();
            //}

            var catit = FormCategory(cats);

            if (IsSite)
            {
                var tabs = new List<TablesOfId>();
                foreach (var itFamilyCategory in catit)
                {
                    var tmp = itFamilyCategory.Categories.Where(x => x.Categories.Count == 0 && x.BrainProduct_Id.HasValue && x.BrainProduct_Id.Value != 0)
                        .Select(y => new TablesOfId { Id = y.BrainProduct_Id.Value })
                        .ToList();
                    tabs.AddRange(tmp);
                }
                var products = tabs.AsTableValuedParameter("dbo.TableOfId", new[] { "Id" });
                var brModels = Gateway.GetProductModelFromListStockId(products).ToList();
                foreach (var itFamilyCategory in catit)
                {
                    foreach (var itfamCat in itFamilyCategory.Categories.Where(x => x.Categories.Count == 0 && x.BrainProduct_Id.HasValue && x.BrainProduct_Id.Value != 0))
                    {
                        itfamCat.BrainProductModel = helper.FormBrainProductModel(Context, brModels.FirstOrDefault(brMod => brMod.Id == itfamCat.BrainProduct_Id), cont);
                    }
                }
            }
            
            ItFamilyCategories = catit;
            
        }

        private List<ItFamilyCategory> FormCategory(List<Categories> cats)
        {
            var catit = new List<ItFamilyCategory>();
            if (cats.Count > 0)
            {
                if(IsSite || !IsProvider)
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
                else
                {
                    catit.AddRange(cats.Where(x => x.ItFamilyParentId == null && x.FromWhatProvider == FromWhatProvider.Provider1).Select(x => new ItFamilyCategory
                    {
                        Id = x.Id,
                        ItFamilyParentId = x.ItFamilyParentId,
                        CategoryId = x.CategoryId,
                        HasRule = x.HasRule,
                        Name = x.Name,
                        ParentId = x.ParentId,
                        BrainProduct_Id = x.BrainProduct_Id,
                        FromWhatProvider = x.FromWhatProvider,
                        Categories = cats.Where(y => y.ItFamilyParentId == x.Id && y.FromWhatProvider == FromWhatProvider.Provider1).Select(d => new ItFamilyCategory
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
                            Categories = cats.Where(q => q.ItFamilyParentId == d.Id && q.FromWhatProvider == FromWhatProvider.Provider1).Select(q => new ItFamilyCategory
                            {
                                Id = q.Id,
                                ParentId = q.ParentId,
                                BrainProduct_Id = q.BrainProduct_Id,
                                CategoryId = q.CategoryId,
                                HasRule = q.HasRule,
                                ItFamilyParentId = q.ItFamilyParentId,
                                Name = q.Name,
                                FromWhatProvider = q.FromWhatProvider,
                                Categories = cats.Where(a => a.ItFamilyParentId == q.Id && a.FromWhatProvider == FromWhatProvider.Provider1).Select(a => new ItFamilyCategory
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
                }
            }

            return catit;
        }
    }
}