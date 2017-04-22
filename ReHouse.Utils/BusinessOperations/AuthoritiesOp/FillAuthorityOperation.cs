using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.BusinessOperations.Auth.Roles;
using ReHouse.Utils.BusinessOperations.Users;
using ReHouse.Utils.BusinessOperations.Titles;
using ReHouse.Utils.BusinessOperations.News;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.AuthoritiesOp
{
    public class FillAuthorityOperation : BaseOperation
    {
        public List<BaseOperation> BaseOperations { get; set; }
        protected override void OnBeginTransaction()
        {
            BaseOperations = new List<BaseOperation>
            {
                new LoadAuthoritiesOperation(null),
                new LoadRolesWithAuthorityOperation(null),
                new AddRoleOperation(null, null, null),
                new DeleteRoleOperation(null, 0),
                new LoadDataRolesOperation(null),
                new UpdateRoleOperation(null, 0, null, null),
                new LoadUsersOperation(null),
                new LoadUserOperation(null, 0),
                new AddUserOperation(null, null, null, 0),
                new DeleteUserOperation(0, null),
                new UpdateUserOperation(null, null, null),
                new UpdateRoleForUserOperation(0, 0, null),
                new UpdateTitleOperation(null, 0, null),
                new AddTitleOperation(null, null),
                new DeleteTitleOperation(null, 0),                
                new UpdateArticleOperation(null, 0, null, null, null),
                new AddArticleOperation(null, null, null, null),
                new DeleteArticleOperation(null, null),

            };            
        }

        protected override void InTransaction()
        {
            var addAuthority = new List<Authority>();
            foreach (var baseOperation in BaseOperations)
            {                
                var authority = Context.Authorities.FirstOrDefault(x => x.NameBusinessOperation == baseOperation.Name);
                if (authority == null)
                {
                    authority = Context.Authorities.FirstOrDefault(x => x.RussianNameOperation == baseOperation.RussianName);                    
                    if (authority != null)
                        authority.NameBusinessOperation = baseOperation.Name;
                    else
                    {
                        authority = new Authority
                        {
                            NameBusinessOperation = baseOperation.Name,
                            RussianNameOperation = baseOperation.RussianName
                        };
                        addAuthority.Add(authority);
                    }
                }
                else
                {
                    if (authority.RussianNameOperation != baseOperation.RussianName)
                        authority.RussianNameOperation = baseOperation.RussianName;
                    if (authority.Deleted)
                        authority.Deleted = false;
                }
            }
            if (addAuthority.Count > 0)
                Context.Authorities.AddRange(addAuthority);
            Context.SaveChanges();

            var count = Context.Authorities.Count();
            if (count > BaseOperations.Count)
            {
                var authorities = Context.Authorities.ToList();
                foreach (var authority in authorities)
                {
                    var baseOp = BaseOperations.FirstOrDefault(x => x.Name == authority.NameBusinessOperation);
                    if (baseOp == null)
                        Context.Authorities.Remove(authority); //Полное удаление
                        //var opForDeleting = Context.Authorities.FirstOrDefault(x => x.NameBusinessOperation == authority.NameBusinessOperation);
                        //opForDeleting.Deleted = true;
                }
                Context.SaveChanges();
            }
        }       
    }
}