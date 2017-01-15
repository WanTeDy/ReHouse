using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadBrainProductForReviewGoodOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 ProductId { get; set; }
        public BrainProductFullInfo BrainProductFullInfo { get; set; }
        public BrainProductModel BrainProductModel { get; set; }
        public List<BrainProductModel> OftenBuyProducts { get; set; }
        private Contractor cont = null;
        public Boolean IsBrainProductId { get; set; }

        public LoadBrainProductForReviewGoodOperation(string tokenHash, int productId, bool isBrainProductId)
        {
            TokenHash = tokenHash;
            ProductId = productId;
            IsBrainProductId = isBrainProductId;
        }
        protected override void InTransaction()
        {
            CheckTokenHash();
            StockProduct prod = null;
            if (IsBrainProductId)
                prod = Context.StockProducts.Include("AdditionalData").Include("PropertyValueses").Include("AdditionalData.PathImageses")
                        .FirstOrDefault(x => x.ProductId == ProductId && !x.Deleted);
            else
                prod = Context.StockProducts.Include("AdditionalData").Include("PropertyValueses").Include("AdditionalData.PathImageses")
                        .FirstOrDefault(x => x.Id == ProductId && !x.Deleted);
            var helper = new HelperPriceForOneProduct();
            BrainProductModel = helper.FormBrainProductModel(Context, prod, cont);
            
            if (prod != null)
            {
                List<Specifications> specifications = null;
                if (prod.PropertyValueses != null && prod.PropertyValueses.Any(x=>!x.Deleted))
                {
                    specifications = prod.PropertyValueses.Where(y=>!y.Deleted).Select(x => new Specifications
                    {
                        name  = x.ProductProperty != null ? x.ProductProperty.PropertyName : x.PropertyId.ToString(),
                        value = x.Value
                    }).ToList();
                }

                var fullInfo = new BrainProductFullInfo
                {
                    name = prod.Name,
                    articul = prod.Articul,
                    brief_description = prod.BriefDescription,
                    date_modified = 
                        prod.AdditionalData != null && prod.AdditionalData.DateModified.HasValue
                            ? prod.AdditionalData.DateModified.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "",
                    description = prod.AdditionalData != null ? prod.AdditionalData.Description : "",
                    options = specifications,
                    product_code = prod.AdditionalData != null ? prod.AdditionalData.ProductCode : "",
                    is_archive = prod.IsAvailable
                };
                BrainProductFullInfo = fullInfo;
                if(BrainProductModel == null) return;
                if (cont != null && cont.Role != null && cont.Role.Name == ConstV.RoleAdministrator)
                {
                    fullInfo.price = BrainProductModel.price;
                    fullInfo.price_uah = BrainProductModel.price_uah;
                }
                else if (cont != null && cont.Role != null && cont.Role.Name == ConstV.RoleManager)
                {
                    fullInfo.price = BrainProductModel.PriceUsdForManager;
                }
                else if (cont != null && cont.Role != null && cont.Role.Name == ConstV.RolePartner)
                {
                    fullInfo.price = BrainProductModel.PriceUsdForPartner;
                }
                else
                {
                    fullInfo.price_uah = BrainProductModel.PriceUahForClients;
                }
                BrainProductFullInfo = fullInfo;

                var cat = Context.ItFamilyCategories.Include("Parent").FirstOrDefault(x => x.Id == BrainProductModel.ItfamilyCategoryID);

                if (cat != null && cat.Parent != null && cat.Parent.Categories.Count > 0)
                {
#if TEST
                    var categ =
                        Context.Categories.Include("Categories").Include("BrainProduct").Where(x => x.parentID == cat.parentID && x.categoryID != cat.categoryID)
                            .ToList();
                    var list = new List<BrainProduct>();
                    foreach (var brainCategory in categ)
                    {
                        if (brainCategory.Categories!=null && brainCategory.Categories.Count>0)
                        {
                            foreach (var category in brainCategory.Categories.Where(x=>x.BrainProduct!=null))
                            {
                                list.Add(category.BrainProduct);
                            }
                        }
                        else if(brainCategory.BrainProduct != null)
                            list.Add(brainCategory.BrainProduct);
                    }
                    var resList = new List<BrainProductModel>();
                    foreach (var brainProduct in list)
                    {
                        var resProd = helper.FormBrainProductModel(Context, brainProduct, cont);
                        resList.Add(resProd);
                    }
                    OftenBuyProducts = resList;
#endif
                    var list = new List<StockProduct>();
                    var d = Context.ItFamilyCategories.Include("BrainProduct").Where(x => !x.Deleted && x.ItFamilyParentId == cat.ItFamilyParentId).ToList();
                    foreach (var brainCategory in d)
                    {
                        if (brainCategory.Categories != null && brainCategory.Categories.Count > 0)
                        {
                            foreach (var category in brainCategory.Categories.Where(x => x.BrainProduct != null))
                            {
                                list.Add(category.BrainProduct);
                            }
                        }
                        if (brainCategory.BrainProduct != null)
                            list.Add(brainCategory.BrainProduct);
                    }
                    //var prods = cat.Parent.Categories.Where(x => x.BrainProduct != null).Select(x => x.BrainProduct).ToList();
                    OftenBuyProducts = list.Where(x=>x.Id != ProductId).Select(brainProducts => helper.FormBrainProductModel(Context, brainProducts, cont)).ToList();
                    if (OftenBuyProducts == null || OftenBuyProducts.Count == 0)
                    {
                        var catId = cat.Parent.ItFamilyParentId;
                        var secondCats = Context.ItFamilyCategories.Include("Categories").Include("BrainProduct").Where(x => x.ItFamilyParentId == catId).ToList();
                        
                        foreach (var brainCategory in secondCats)
                        {
                            if (brainCategory.Categories != null && brainCategory.Categories.Count > 0)
                            {
                                foreach (var category in brainCategory.Categories.Where(x => x.BrainProduct != null))
                                {
                                    list.Add(category.BrainProduct);
                                }
                            }
                            if (brainCategory.BrainProduct != null)
                                list.Add(brainCategory.BrainProduct);
                        }
                        OftenBuyProducts = list.Where(x => x.Id != ProductId).Select(brainProducts => helper.FormBrainProductModel(Context, brainProducts, cont)).ToList();
                    }
                }
            }
        }

        private void CheckTokenHash()
        {
            if (!String.IsNullOrEmpty(TokenHash))
            {
                cont = Context.Contractors.Include("Role").FirstOrDefault(
                        x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            }
        }
    }
}