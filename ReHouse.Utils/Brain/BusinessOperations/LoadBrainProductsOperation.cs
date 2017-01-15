using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadBrainProductsOperation : BaseOperation
    {
        //public List<BrainProduct> BrainProducts { get; set; }
        public List<BrainProductModel> BrainProductModels { get; set; }
        public Int32 CountPages { get; set; }
        public Int32 TotalItems { get; set; }

        private Int32 CategoryId { get; set; }
        //private Boolean CheckInStock { get; set; }
        private String TokenHash { get; set; }
        private Int32 Page { get; set; }
        private Int32 ItemsPerPage { get; set; }
        private ColumnSort ColumnSortOrder { get; set; }
        private String PropertyName { get; set; }
        private Boolean IsSite { get; set; }

        public LoadBrainProductsOperation(int categoryId, int page, int itemsPerPage, string tokenHash, ColumnSort columnSortOrder, string propertyName, bool isSite = false)
        {
            CategoryId = categoryId;
            TokenHash = tokenHash;
            Page = page;
            ItemsPerPage = itemsPerPage;
            ColumnSortOrder = columnSortOrder;
            PropertyName = propertyName;
            //CheckInStock = false;
            IsSite = isSite;
        }

        public LoadBrainProductsOperation(int categoryId, int page, int itemsPerPage, string tokenHash, bool isSite = true)
        {
            CategoryId = categoryId;
            TokenHash = tokenHash;
            Page = page;
            ItemsPerPage = itemsPerPage;
            ColumnSortOrder = ColumnSort.ASC;
            PropertyName = "Price";
            //CheckInStock = false;
            IsSite = isSite;
        }

        private IQueryable<StockProduct> FillProducts()
        {
            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == CategoryId);
            if (cat != null)
            {
                IQueryable<StockProduct> mainProd = null;
                if(IsSite)
                    mainProd = Context.StockProducts.Where(x => x.IsAvailable && x.ItFamilyCategoryId == CategoryId && !x.Deleted);
                else
                    mainProd = Context.StockProducts.Where(x => x.IsAvailable && x.ItFamilyCategoryId == CategoryId && !x.Deleted && x.FromWhatProvider == FromWhatProvider.Provider1);
                //if (mainProd.Count() > 0)
                //    listToAdd.Add(mainProd);
                if (cat.Categories.Count > 0)
                {
                    foreach (var brainCategory in cat.Categories)
                    {
                        ItFamilyCategory category = brainCategory;
                        IQueryable<StockProduct> ct = null;
                        if(IsSite)
                            ct = Context.StockProducts.Where(x => x.ItFamilyCategoryId == category.Id && !x.Deleted && x.IsAvailable);//.ToList();
                        else
                            ct = Context.StockProducts.Where(x => x.ItFamilyCategoryId == category.Id && !x.Deleted && x.IsAvailable && x.FromWhatProvider == FromWhatProvider.Provider1);
                        if (ct.Count() > 0)
                            mainProd = mainProd.Concat(ct);
                        //listToAdd.Add(ct);//Range(ct);
                        if (brainCategory.Categories.Count > 0)
                        {
                            foreach (var brainCategory1 in brainCategory.Categories)
                            {
                                if (brainCategory1.Categories.Count > 0)
                                {
                                    foreach (var category1 in brainCategory1.Categories)
                                    {
                                        IQueryable<StockProduct> ct2 = null;
                                        if (IsSite)
                                            ct2 = Context.StockProducts.Where(x => x.ItFamilyCategoryId == category1.Id && !x.Deleted && x.IsAvailable);
                                        else
                                            ct2 = Context.StockProducts.Where(x => x.ItFamilyCategoryId == category1.Id && !x.Deleted && x.IsAvailable && x.FromWhatProvider == FromWhatProvider.Provider1);
                                        if (ct2.Count() > 0)
                                            mainProd=mainProd.Concat(ct2);
                                    }
                                }
                                else
                                {
                                    IQueryable<StockProduct> ct2 = null;
                                    if (IsSite)
                                        ct2 = Context.StockProducts.Where(x => x.ItFamilyCategoryId == brainCategory1.Id && !x.Deleted && x.IsAvailable);
                                    else
                                        ct2 = Context.StockProducts.Where(x => x.ItFamilyCategoryId == brainCategory1.Id && !x.Deleted && x.IsAvailable && x.FromWhatProvider == FromWhatProvider.Provider1);
                                    
                                    if (ct2.Count() > 0)
                                        mainProd = mainProd.Concat(ct2);
                                }
                            }
                        }
                    }
                }
                return mainProd;
            }
            return null;
        }

        protected override void InTransaction()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            //CheckTokenHash();
            var cash = FillProducts();
            var pageProd = new List<StockProduct>();
            if (cash != null && cash.Any())
            {
                TotalItems = cash.Count();//x => x.BrainStockses.Count > 0);
                if (((Page - 1) * ItemsPerPage) < TotalItems)
                {
                    //if(ColumnSortOrder == ColumnSortOrder.Ascending)
                    //    pageProd = cash.OrderBy(x=>x.Price).Skip((Page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
                    //else
                    //    pageProd = cash.OrderByDescending(x=>x.Price).Skip((Page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();

                    if (ColumnSortOrder == ColumnSort.ASC)
                        pageProd = cash.OrderBy(PropertyName).Skip((Page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
                    else
                        pageProd = cash.OrderByDescending(PropertyName).Skip((Page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();

                    //pageProd = pageProd.Where(x => x.BrainStockses.Count > 0).ToList();
                    CountPages = Convert.ToInt32(Math.Ceiling((double)TotalItems / ItemsPerPage));
                }
                else
                {
                    //TODO exception
                }
            }
            else return;
            
            BrainProductModels = new List<BrainProductModel>();
            //BaseBrainResponse auth = null;
            //if(CheckInStock)
            //{
            //    var empl = Context.RoleSet.Include("Role").FirstOrDefault(x => x.Name == ConstV.RoleAdministrator && !x.Deleted && !String.IsNullOrEmpty(x.ProviderLogin1) && !String.IsNullOrEmpty(x.ProviderMd5Password1));
            //    if (empl == null) return;
            //    auth = AuthBrainFacade.Auth(empl.ProviderLogin1, empl.ProviderMd5Password1).Result;
            //}
            var helper = new HelperPriceForListProduct(TokenHash, CategoryId, Context);
            helper.Init();
            //GetRulesAndCourse();
            foreach (var brainProduct in pageProd)
            {
                //if (auth != null && auth.status == 1)
                //{
                //    var op = new InStockOperation(auth.result, brainProduct.ProductId);
                //    op.ExcecuteTransaction();
                //    if (op.InStock)
                //    {
                //        var pr = helper.AddProductToList(brainProduct);
                //        if (pr != null)
                //            BrainProductModels.Add(pr);
                //    }
                //}
                //else
                //{
                    var pr = helper.AddProductToList(brainProduct, false);
                    if (pr != null)
                        BrainProductModels.Add(pr);
                //}
            }
        }
    }
}
