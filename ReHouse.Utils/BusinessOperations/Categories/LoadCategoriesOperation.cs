using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Categories
{
    public class LoadCategoriesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }        
        private Int32 _categoryId { get; set; }
        public List<Category> _categories { get; set; }

        public LoadCategoriesOperation(string tokenHash, int categoryId = 0)
        {
            _tokenHash = tokenHash;            
            _categoryId = categoryId;
            RussianName = "Получение категорий";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            
            if (_categoryId == 0)
            {
                _categories = Context.Categories.Where(x => !x.Deleted).ToList();                
            }
            else
            {
                _categories = Context.Categories.Where(x => !x.Deleted && x.ParentId == _categoryId).ToList();
            }
        }
    }
}