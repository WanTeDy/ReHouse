using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.BusinessOperations;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class SearcProductshOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public String Search { get; set; }
        public Int32 CategoryId { get; set; }
        public List<CategorySearchModel> BrainCategories { get; set; }
        public List<BrainProductModel> BrainProducts { get; set; }
        public SearcProductshOperation(string tokenHash, string search, int categoryId = 0)
        {
            TokenHash = tokenHash;
            Search = search;
            CategoryId = categoryId;
        }

        protected override void InTransaction()
        {
            //var cont = CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, Name);
            var contractor = Context.Contractors.Include("Role").FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            var str = Search.ToLower().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var products = Context.StockProducts.Where(x => !x.Deleted && str.All(r=>x.Name.ToLower().Contains(r)) && x.IsAvailable).ToList();//|| x.brief_description.ToLower().Contains(Search)));
            var helper = new HelperPriceForOneProduct();
            BrainCategories = new List<CategorySearchModel>();
            BrainProducts = new List<BrainProductModel>();
            if (products != null && products.Any())
            {
                SearchProducts(products, helper, contractor);
            }
            //if (CategoryId != 0)
            //{
            //    products = Context.StockProducts.Where(x => !x.Deleted && x.Name.ToLower().Contains(Search) && x.ItFamilyCategoryId == CategoryId && x.IsAvailable).ToList();
            //    foreach (var brainProduct in products)
            //    {
            //        var prod = helper.FormBrainProductModel(Context, brainProduct, contractor);
            //        BrainProducts.Add(prod);
            //    }
            //}

            // q - входная строка
            //var q = "";
            //string where = "";
            //string[] keywords = new Regex("\\s+").Replace(q, " ").Trim().Split(' ');
            //foreach (string keyword in keywords)
            //{
            //    if (!string.IsNullOrEmpty(where))
            //        where += " and ";
            //    string subWhere = "Title like '%" + Utils.SqlEscape(keyword) + "%'";
            //    // если ввели запятую
            //    if (keyword.IndexOf(",") != -1)
            //    {
            //        subWhere = subWhere + " or Title like '%" + Utils.SqlEscape(keyword.Replace(',', '.')) + "%'";
            //    }
            //    // если ввели точку
            //    else if (keyword.IndexOf(".") != -1)
            //    {
            //        subWhere = subWhere + " or Title like '%" + Utils.SqlEscape(keyword.Replace('.', ',')) + "%'";
            //    }
            //    // если слово может являться идентификатором
            //    if (new Regex(@"^\d+$").IsMatch(keyword))
            //    {
            //        subWhere = subWhere + " or Id = " + keyword;
            //    }
            //    where += "(" + subWhere + ")";
            //}
            // составим запрос
            //string sql = "select * from Products where " + where + ";";
        }

        private void SearchProducts(List<StockProduct> products, HelperPriceForOneProduct helper, Contractor contractor)
        {
            foreach (var brainProduct in products)
            {
                var category = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == brainProduct.ItFamilyCategoryId);
                if (category != null)
                {
                    var ct = BrainCategories.FirstOrDefault(x => x.categoryId == brainProduct.ItFamilyCategoryId);
                    if (ct != null)
                        ct.Amount++;
                    else
                    {
                        var cat = new CategorySearchModel
                        {
                            Amount = 1,
                            categoryId = brainProduct.ItFamilyCategoryId,
                            Name = category.Name
                        };
                        BrainCategories.Add(cat);
                    }
                }
                if (CategoryId == 0)
                {
                    var prod = helper.FormBrainProductModel(Context, brainProduct, contractor);
                    BrainProducts.Add(prod);
                }
            }

            var categ = new CategorySearchModel
            {
                Amount = products.Count,
                categoryId = 0,
                Name = "Все"
            };
            BrainCategories.Add(categ);
        }
    }
}