using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.AuthoritiesOp
{
    public class LoadRolesWithAuthorityOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<Role> _roles { get; set; }

        public LoadRolesWithAuthorityOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Просмотр ролей с полномочиями доступа";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var roles = Context.Roles.Include("Authorities").Where(x => !x.Deleted).ToList();
            if (roles.Count > 0)
                _roles = roles.Select(x => new Role
                {
                    Id = x.Id,
                    RussianName = x.RussianName,
                    Authorities = x.Authorities.Select(y => new Authority
                    {
                        Id = y.Id,
                        NameBusinessOperation = y.NameBusinessOperation,
                        RussianNameOperation = y.RussianNameOperation
                    }).ToList()
                }).ToList();
        }
    }
}