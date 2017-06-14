using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class LoadSeoParametrsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }

        public List<SeoParam> _seoParametrs { get; set; }

        public LoadSeoParametrsOperation(string tokenHash)
        {
            _tokenHash = tokenHash;

            RussianName = "Получение сео параметров для всех страниц";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
            if (user != null && (user.Role.RussianName == ConstV.RoleAdministrator /*|| user.Role.RussianName == ConstV.RoleManager*/ || user.Role.RussianName == ConstV.RoleSeo))
                _seoParametrs = Context.SeoParams.Where(x => !x.Deleted && x.ActionName != ConstV.DetailAction).ToList();
            else
                throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции");
        }
    }
}