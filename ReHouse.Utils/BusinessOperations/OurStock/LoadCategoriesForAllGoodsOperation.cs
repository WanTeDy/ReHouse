using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.OurStock
{
    public class LoadCategoriesForAllGoodsOperation : BaseOperation
    {
        public List<ItFamilyCategory> ItFamilyCategories { get; set; }
        private readonly HelperPriceForOneProduct _helperPrice = new HelperPriceForOneProduct();
        private String TokenHash { get; set; }
        private Contractor Contractor { get; set; }

        public LoadCategoriesForAllGoodsOperation(string tokenHash)
        {
            TokenHash = tokenHash;
        }

        public List<ItFamilyCategory> Recurs(List<ItFamilyCategory> outCategory, List<ItFamilyCategory> sourceCategory)
        {
            foreach (var categoriesModel in outCategory)
            {
                ItFamilyCategory model = categoriesModel;
                var addCategories = sourceCategory.Where(x => x.ItFamilyParentId == model.Id).ToList();
                if (addCategories.Count > 0)
                {
                    foreach (var category in addCategories)
                    {
                        DataBase.OurStocks.StockProduct st = null;
                        if (category.BrainProduct != null)
                            st = new DataBase.OurStocks.StockProduct
                            {
                                Id = category.BrainProduct.Id,
                                Articul = category.BrainProduct.Articul,
                                BriefDescription = category.BrainProduct.BriefDescription,
                                Name = category.BrainProduct.Name,
                                ItFamilyCategoryId = category.BrainProduct.ItFamilyCategoryId,
                                ItFamilyVendorId = category.BrainProduct.ItFamilyVendorId,
                                Price = category.BrainProduct.Price,
                                PriceUah = category.BrainProduct.PriceUah,
                                ProductId = category.BrainProduct.ProductId,
                                MainImage = category.BrainProduct.MainImage,
                                FromWhatProvider = category.BrainProduct.FromWhatProvider
                            };
                        categoriesModel.Categories.Add(new ItFamilyCategory
                        {
                            Id = category.Id,
                            BrainProduct = st,
                            CategoryId = category.CategoryId,
                            FromWhatProvider = category.FromWhatProvider,
                            HasRule = category.HasRule,
                            Name = category.Name,
                            ItFamilyParentId = category.ItFamilyParentId,
                            ParentId = category.ParentId,
                            BrainProductModel = _helperPrice.FormBrainProductModel(Context, st, Contractor)
                        });
                    }
                    //categoriesModel.Categories.AddRange(addCategories);
                }
                Recurs(categoriesModel.Categories, sourceCategory);
            }
            return outCategory;
        }

        protected override void InTransaction()
        {
            Contractor = Context.Contractors.FirstOrDefault(x => x.IsActive && !x.Deleted && x.TokenHash == TokenHash);
            var d = DeserializeCollection("C:\\Develop\\ItfalimyCategoriesWithTopProducts.xml");
            if (d != null)
            {
                //foreach (var itFamilyCategory in d.Where(itFamilyCategory => itFamilyCategory.BrainProduct != null))
                //{
                //    itFamilyCategory.BrainProductModel = _helperPrice.FormBrainProductModel(Context,
                //        itFamilyCategory.BrainProduct, Contractor);
                //}
                ItFamilyCategories = d;
                return;
            }
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            //Context.Configuration.ProxyCreationEnabled = false;
            var categories = Context.ItFamilyCategories.Include("BrainProduct").Where(x => !x.Deleted && (x.ParentId != 1 || x.ItFamilyParentId != null)).ToList();

            var hierarchy = Context.ItFamilyCategories.Where(x => x.ParentId == 1 || x.ItFamilyParentId == null && !x.Deleted).ToList(); //.Select(Mapper.Map<BrainCategory, BrainCategory>)
            var newEl = hierarchy.Select(category => new ItFamilyCategory
            {
                Id = category.Id,
                CategoryId = category.CategoryId,
                FromWhatProvider = category.FromWhatProvider,
                HasRule = category.HasRule,
                Name = category.Name,
                ItFamilyParentId = category.ItFamilyParentId,
                ParentId = category.ParentId,
            }).ToList();
            //foreach (var brainCategory in hierarchy)
            //    categories.Remove(brainCategory);
            ItFamilyCategories = Recurs(newEl, categories);
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.Configuration.ValidateOnSaveEnabled = true;
            Context.Configuration.ProxyCreationEnabled = true;

            SerializeCollection("C:\\Develop\\ItfalimyCategoriesWithTopProducts.xml", ItFamilyCategories);
            //var flag = false;
            //foreach (var brainCategory in BrainCategories.Where(brainCategory => brainCategory.Categories.Count > 0))
            //{
            //    bool tmp;
            //    foreach (var category in brainCategory.Categories.Where(category => category.Categories.Count > 0))
            //    {
            //        foreach (var category1 in category.Categories.Where(category1 => category1.Categories.Count>0))
            //        {
            //            tmp = category1.HasRule;
            //            category1.HasRule = category1.Categories.Count(x => x.HasRule) == category1.Categories.Count;
            //            if (tmp != category1.HasRule) flag = true;
            //        }
            //        tmp = category.HasRule;
            //        category.HasRule = category.Categories.Count(x => x.HasRule) == category.Categories.Count;
            //        if (tmp != category.HasRule) flag = true;
            //    }
            //    tmp = brainCategory.HasRule;
            //    brainCategory.HasRule = brainCategory.Categories.Count(x => x.HasRule) == brainCategory.Categories.Count;
            //    if (tmp != brainCategory.HasRule) flag = true;
            //}
            //if (flag)
            //    Context.SaveChanges();
        }
        private void SerializeCollection(string filename, List<ItFamilyCategory> categories)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<ItFamilyCategory>));

            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, categories);
            writer.Close();
        }
        private List<ItFamilyCategory> DeserializeCollection(string filename)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ItFamilyCategory>));

                TextReader reader = new StreamReader(filename);
                var d = ser.Deserialize(reader) as List<ItFamilyCategory>;
                reader.Close();
                return d;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}