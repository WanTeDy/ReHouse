using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations
{
    public class CheckUserRoleAuthorityOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _nameOperation { get; set; }
        private String _russianNameOperation { get; set; }
        public User _user { get; set; }
        public List<Authority> _authorities { get; set; }
        public CheckUserRoleAuthorityOperation(string tokenHash, string nameOperation, string russianNameOperation)
        {
            _tokenHash = tokenHash;
            _nameOperation = nameOperation;
            _russianNameOperation = russianNameOperation;
            ExcecuteTransaction();
        }

        protected override void InTransaction()
        {
            _user = Context.Users.Include("Role").FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted && x.IsActive);
            if (_user == null)
                throw new ActionNotAllowedException("Некорректный TokenHash");
            var role = Context.Roles.Include("Authorities").FirstOrDefault(x => !x.Deleted && x.Id == _user.RoleId);
            if (role == null || role.Authorities == null || role.Authorities.Count <= 0)
                throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции: " + _russianNameOperation);
            var authority = role.Authorities.FirstOrDefault(x => x.NameBusinessOperation == _nameOperation);
            if (authority == null)
                throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции: " + _russianNameOperation);
        }
    }
}