using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.AuthoritiesOp
{
    public class LoadAuthoritiesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<Authority> _authorities { get; set; }
        public LoadAuthoritiesOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Просмотр полномочий доступа для ролей";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var authorities = Context.Authorities.ToList();
            if (authorities.Count > 0)
                _authorities = authorities.Select(x => new Authority
                {
                    Id = x.Id,
                    NameBusinessOperation = x.NameBusinessOperation,
                    RussianNameOperation = x.RussianNameOperation
                }).ToList();
        }
    }
}