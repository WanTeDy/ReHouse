using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Managers
{
    public class LoadManagersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<User> _users { get; set; }

        public LoadManagersOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Загрузка списка менеджеров";
        }

        protected override void InTransaction()
        {
            _users = Context.Users
                        .Where(x => !x.Deleted && x.Role.RussianName != ConstV.RoleAdministrator && x.Role.RussianName != ConstV.RoleSeo)
                        .OrderBy(x => x.OrderByField).ToList();
        }
    }
}