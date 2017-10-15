using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class LoadUsersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<User> _users { get; set; }

        public LoadUsersOperation(string tokenHash, int page, int count)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            RussianName = "Загрузка списка пользователей";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _users = Context.Users
                        .Where(x => !x.Deleted && x.Role.RussianName != ConstV.RoleAdministrator && x.Role.RussianName != ConstV.RoleSeo)
                        .OrderBy(x => x.OrderByField)
                        .Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}