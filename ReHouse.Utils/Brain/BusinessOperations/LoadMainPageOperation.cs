using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadMainPageOperation : BaseOperation
    {
        public List<BrainProductModel> ProductsOverview { get; set; }
        public List<BrainProductModel> ProductsBestsellers { get; set; }
        private String TokenHash { get; set; }

        public LoadMainPageOperation(string tokenHash)
        {
            TokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            ProductsBestsellers = new List<BrainProductModel>();
            ProductsOverview = new List<BrainProductModel>();
            var random = new Random();
            List<StockProduct> overview = null;
            List<StockProduct> bestSellers = null;
            var prods =
                Context.StockProducts.Include("AdditionalData")
                    .Where(x => x.AdditionalData.DateModified.HasValue && x.IsAvailable && !x.Deleted);
            var maxOver = prods.Count(x => x.AdditionalData.DateModified.HasValue) - 4;
            var rand1 = random.Next(1, maxOver);
            overview =
                prods.Where(x => x.AdditionalData.DateModified.HasValue)
                    .OrderByDescending(x => x.AdditionalData.DateModified)
                    .Skip(rand1)
                    .Take(3)
                    .ToList();
            maxOver = Context.StockProducts.Include("AdditionalData").Count(x => x.IsAvailable && !x.Deleted);
            rand1 = random.Next(1, maxOver);
            bestSellers =
                Context.StockProducts.Include("AdditionalData")
                    .Where(x => x.IsAvailable && !x.Deleted)
                    .OrderBy(x => x.Id)
                    .Skip(rand1)
                    .Take(3)
                    .ToList();
            //var u = Context.BrainStockses.Include("BrainProducts").FirstOrDefault(x => x.stockID == 168);
            //if (u != null)
            //{
            //    //var max = u.BrainProducts.Count(x=>!String.IsNullOrEmpty(x.description)) - 4;
            //    var max = u.BrainProducts.Count(x=>x.DateTimeModified.HasValue) - 4;
            //    var rand = random.Next(1, max);
            //    //overview = u.BrainProducts.Where(x => !String.IsNullOrEmpty(x.description)).Skip(rand).Take(2).ToList();
            //    overview = u.BrainProducts.Where(x=>x.DateTimeModified.HasValue).OrderByDescending(x=>x.DateTimeModified).Skip(rand).Take(3).ToList();
            //    max = u.BrainProducts.Count - 4;
            //    rand = random.Next(1, max);
            //    bestSellers = u.BrainProducts.OrderBy(x => x.Id).Skip(rand).Take(3).ToList();
            //}
            
            foreach (var brainProduct in bestSellers)
            {
                var helper = new HelperPriceForListProduct(TokenHash, brainProduct.ItFamilyCategoryId, Context);
                helper.Init();
                var pr = helper.AddProductToList(brainProduct, false);
                if (pr != null)
                    ProductsBestsellers.Add(pr);
            }

            foreach (var brainProduct in overview)
            {
                var helper = new HelperPriceForListProduct(TokenHash, brainProduct.ItFamilyCategoryId, Context);
                helper.Init();
                var pr = helper.AddProductToList(brainProduct, true);
                if (pr != null)
                    ProductsOverview.Add(pr);
            }
        }
    }
}