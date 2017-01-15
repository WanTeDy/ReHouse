using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Auth.Roles
{
    public class UpdateProviderRolesOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<Role> Roles { get; set; }

        public UpdateProviderRolesOperation(string tokenHash, List<Role> roles)
        {
            TokenHash = tokenHash;
            Roles = roles;
            RussianName = "Изменение логина и пароля поставщика";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            foreach (var role1 in Roles)
            {
                var role = Context.RoleSet.Include("Contractors").FirstOrDefault(x => x.Id == role1.Id && !x.Deleted);
                if (role == null)
                    throw new ObjectNotFoundException("Выбраный объект не найден. RoleId = " + role1.Id);
                role.Name = role1.Name;
                role.ProviderLogin1 = role1.ProviderLogin1;
                role.ProviderPassword1 = role1.ProviderPassword1;
                role.ProviderMd5Password1 = role1.ProviderMd5Password1;
            }
            Context.SaveChanges();

        }
    }
}