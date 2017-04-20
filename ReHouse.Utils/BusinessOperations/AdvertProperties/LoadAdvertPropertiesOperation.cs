using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.AdvertProperties
{
    public class LoadAdvertPropertiesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _categoryId { get; set; }
        public List<AdvertProperty> _advertProperties { get; set; }

        public LoadAdvertPropertiesOperation(string tokenHash, int categoryId = 0)
        {
            _tokenHash = tokenHash;
            _categoryId = categoryId;
            RussianName = "Получение свойств категорий";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            if (_categoryId == 0)
            {
                _advertProperties = Context.AdvertProperties.Where(x => !x.Deleted).ToList();
            }
            else
            {
                _advertProperties = Context.AdvertProperties.Where(x => !x.Deleted && x.Categories.Any(y => y.Id == _categoryId)).ToList();
            }
        }
    }
}