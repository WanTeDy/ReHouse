using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.Auth.Roles
{
    public class AddStandartsRolesOperations : BaseOperation
    {
        protected override void InTransaction()
        {
            //менеджера, руководителя, партнера, клиента
            var authorities = Context.Authorities.ToList();
            var role1 = new Role {Name = ConstV.RoleManager, Authorities = authorities.Count>0 ? authorities : null};
            var role2 = new Role { Name = ConstV.RoleAdministrator, Authorities = authorities.Count > 0 ? authorities : null };
            var role3 = new Role { Name = ConstV.RolePartner, Authorities = authorities.Count > 0 ? authorities : null };
            var role4 = new Role { Name = ConstV.RoleClient, Authorities = authorities.Count > 0 ? authorities : null };

            var role = Context.RoleSet.Include("Authorities").FirstOrDefault(x => x.Name == role1.Name && !x.Deleted);
            if (role == null)
                Context.RoleSet.Add(role1);
            
            role = Context.RoleSet.FirstOrDefault(x => x.Name == role2.Name && !x.Deleted);
            if (role == null)
                Context.RoleSet.Add(role2);
            else
                GetValue(authorities, role);

            role = Context.RoleSet.FirstOrDefault(x => x.Name == role3.Name && !x.Deleted);
            if (role == null)
                Context.RoleSet.Add(role3);
            
            role = Context.RoleSet.FirstOrDefault(x => x.Name == role4.Name && !x.Deleted);
            if (role == null)
                Context.RoleSet.Add(role4);
            else
                GetValue(authorities, role);

            Context.SaveChanges();
        }

        private static void GetValue(List<Authority> authorities, Role role)
        {
            foreach (var authority in authorities)
            {
                var auth = role.Authorities.FirstOrDefault(x => x.Id == authority.Id);
                if (auth == null)
                    role.Authorities.Add(authority);
            }
        }
    }
}