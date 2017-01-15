using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadOptimizeCategoriesOperation : BaseOperation
    {
        public List<BrainCategory> BrainCategories { get; set; }
        private readonly HelperPriceForOneProduct _helperPrice = new HelperPriceForOneProduct();
        private String TokenHash { get; set; }
        private Contractor Contractor { get; set; }
        public LoadOptimizeCategoriesOperation(string tokenHash = "")
        {
            TokenHash = tokenHash;
        }

        public List<BrainCategory> Recurs(List<BrainCategory> outCategory, IQueryable<BrainCategory> sourceCategory)
        {
            foreach (var categoriesModel in outCategory)
            {
                BrainCategory model = categoriesModel;
                var addCategories = sourceCategory.Where(x => x.parentID == model.categoryID && !x.Deleted).ToList();
                if (addCategories.Any())
                {
                    foreach (var brainCategory in addCategories)
                    {
                        categoriesModel.Categories.Add(new BrainCategory
                        {
                            Id = brainCategory.Id,
                            BrainParentID = brainCategory.BrainParentID,
                            categoryID = brainCategory.categoryID,
                            name = brainCategory.name,
                            parentID = brainCategory.parentID,
                            //HasRule = brainCategory.HasRule,
                            //BrainProductModel = _helperPrice.FormBrainProductModel(Context, brainCategory.BrainProduct, Contractor)
                        });
                    }
                }
                Recurs(categoriesModel.Categories, sourceCategory);
            }
            return outCategory;
        }

        protected override void InTransaction()
        {
            Contractor = Context.Contractors.FirstOrDefault(x => x.IsActive && !x.Deleted && x.TokenHash == TokenHash);
            //var d = DeserializeCollection("C:\\Develop\\CategoriesWithTopProducts.xml");
            //if (d != null)
            //{
            //    BrainCategories = d;
            //    return;
            //}

            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            var categories = Context.Categories.Include("BrainProduct").Where(x => !x.Deleted && x.parentID != 1 && x.parentID != 0 && !x.Deleted);

            var hierarchy = Context.Categories.Include("BrainProduct").Where(x => x.parentID == 1 || x.parentID == 0 && !x.Deleted);//.ToList(); //.Select(Mapper.Map<BrainCategory, BrainCategory>)
            var newEl = new List<BrainCategory>();
            
            foreach (var brainCategory in hierarchy)
            {
                var cat = new BrainCategory
                {
                    Id = brainCategory.Id,
                    BrainParentID = brainCategory.BrainParentID,
                    categoryID = brainCategory.categoryID,
                    name = brainCategory.name,
                    parentID = brainCategory.parentID,
                    //HasRule = brainCategory.HasRule,
                };
                newEl.Add(cat);
            }
            
            BrainCategories = Recurs(newEl, categories);
            //SerializeCollection("C:\\Develop\\CategoriesWithTopProducts.xml", BrainCategories);
        }

        private void SerializeCollection(string filename, List<BrainCategory> categories)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<BrainCategory>));

            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, categories);
            writer.Close();
        }
        private List<BrainCategory> DeserializeCollection(string filename)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<BrainCategory>));

                TextReader reader = new StreamReader(filename);
                var d = ser.Deserialize(reader) as List<BrainCategory>;
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