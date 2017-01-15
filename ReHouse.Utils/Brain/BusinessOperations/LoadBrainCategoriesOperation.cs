using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadBrainCategoriesOperation : BaseOperation
    {
        public List<BrainCategory> BrainCategories { get; set; }

        public List<BrainCategory> Recurs(List<BrainCategory> outCategory, List<BrainCategory> sourceCategory)
        {
            foreach (var categoriesModel in outCategory)
            {
                BrainCategory model = categoriesModel;
                var addCategories = sourceCategory.Where(x => x.parentID == model.categoryID).ToList();
                if (addCategories.Count > 0)
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
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            //Context.Configuration.ProxyCreationEnabled = false;
            var categories = Context.Categories.Where(x => !x.Deleted && (x.parentID != 1 || x.parentID != 0)).ToList();

            var hierarchy = Context.Categories.Where(x => x.parentID == 1 || x.parentID == 0 && !x.Deleted).ToList(); //.Select(Mapper.Map<BrainCategory, BrainCategory>)
            var newEl = hierarchy.Select(brainCategory => new BrainCategory
            {
                Id = brainCategory.Id, 
                BrainParentID = brainCategory.BrainParentID, 
                categoryID = brainCategory.categoryID, 
                name = brainCategory.name, 
                parentID = brainCategory.parentID,
                //HasRule = brainCategory.HasRule
            }).ToList();
            //foreach (var brainCategory in hierarchy)
            //    categories.Remove(brainCategory);
            BrainCategories = Recurs(newEl, categories);
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.Configuration.ValidateOnSaveEnabled = true;
            Context.Configuration.ProxyCreationEnabled = true;
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
    }
}