using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class LoadNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<Title> _titles { get; set; }

        public LoadNewBuildingOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех заголовков объявлений";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var titles = Context.Titles.Where(x => !x.Deleted).ToList();
            _titles = titles.Select(x => new Title
            {
                Id = x.Id,
                RussianName = x.RussianName,
            }).ToList();
        }
    }
}