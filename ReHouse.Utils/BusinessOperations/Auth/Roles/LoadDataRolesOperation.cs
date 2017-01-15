using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.BusinessOperations;

namespace ReHouse.Utils.BusinessOperations.Auth.Roles
{
    public class LoadDataRolesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<Role> _roles { get; set; }
        public LoadDataRolesOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение данных о ролях";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var roles = Context.Roles.Where(x => !x.Deleted).ToList();
            _roles = roles.Select(x => new Role
            {
                Id = x.Id,
                RussianName = x.RussianName,
            }).ToList();
        }
    }
}