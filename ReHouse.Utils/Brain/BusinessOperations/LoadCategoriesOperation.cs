using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadCategoriesOperation : BaseOperation
    {
        public List<BrainCategory> BrainCategories { get; set; } 
        protected override void InTransaction()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            //Context.Configuration.ProxyCreationEnabled = false;
            var categories = Context.Categories.Where(x => !x.Deleted).ToList();
            BrainCategories = new List<BrainCategory>();
            foreach (var brainCategory in categories)
            {
                BrainCategories.Add(new BrainCategory
                {
                    Id = brainCategory.Id,
                    BrainParentID = brainCategory.BrainParentID,
                    categoryID = brainCategory.categoryID,
                    name = brainCategory.name,
                    parentID = brainCategory.parentID,
                    //HasRule = brainCategory.HasRule,
                });
            }
        }
    }
}