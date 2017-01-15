using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.AuthoritiesOp
{
    public class ChangeAccessForRoleOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 RoleId { get; set; }
        private List<AuthorityForOneRoleModel> AuthorityForOneRoleModels { get; set; }

        public ChangeAccessForRoleOperation(string tokenHash, int roleId, List<AuthorityForOneRoleModel> authorityForOneRoleModels)
        {
            TokenHash = tokenHash;
            RoleId = roleId;
            AuthorityForOneRoleModels = authorityForOneRoleModels;
            RussianName = "Изменение полномочий для конкретной роли";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var role = Context.RoleSet.Include("Authorities").FirstOrDefault(x => !x.Deleted && x.Id == RoleId);
            if(role == null)
                throw new ObjectNotFoundException("Роль не найдена! RoleId = " + RoleId + " Операция: " + Name);

            var authorityForCheck = AuthorityForOneRoleModels.Where(x => x.IsAccess).ToList().Select(authorityModel => Context.Authorities.FirstOrDefault(x => x.Id == authorityModel.AuthorityId)).Where(authority => authority != null).ToList();

            foreach (var authority in authorityForCheck)
            {
                var existAuthority = role.Authorities.FirstOrDefault(x => x.Id == authority.Id);
                if(existAuthority == null)
                    role.Authorities.Add(authority);
            }
            Context.SaveChanges();

            role = Context.RoleSet.Include("Authorities").FirstOrDefault(x => !x.Deleted && x.Id == RoleId);
            if (role == null)
                throw new ObjectNotFoundException("Роль не найдена! RoleId = " + RoleId + " Операция: " + Name);


            //TODO check in debug
            var delAuthority = new List<Authority>();
            foreach (var authority in role.Authorities)
            {
                var existAuthority = authorityForCheck.FirstOrDefault(x => x.Id == authority.Id);
                if (existAuthority == null)
                    delAuthority.Add(authority);
            }
            
            foreach (var authority in delAuthority)
            {
                role.Authorities.Remove(authority);
            }
            Context.SaveChanges();
        }
    }
}