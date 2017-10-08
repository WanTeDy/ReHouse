using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.BusinessOperations.Auth;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Builders
{
    public class LoadBuildersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private Boolean _isAdmin { get; set; }
        public List<Builder> _builders { get; set; }

        public LoadBuildersOperation(string tokenHash, int page, int count, bool isAdmin = false)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _isAdmin = isAdmin;
            RussianName = "Получение всех застройщиков";
        }

        protected override void InTransaction()
        {
            //if (_isAdmin)            
            //    new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
           
            _builders = Context.Builders.Where(x => !x.Deleted).OrderBy(x => x.Id)
                .Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}