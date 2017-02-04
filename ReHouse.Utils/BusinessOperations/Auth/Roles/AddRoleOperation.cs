using System;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Auth.Roles
{
    public class AddRoleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _name { get; set; }
        private String _russianName { get; set; }
        public Role _role { get; set; }

        public AddRoleOperation(string tokenHash, string name, string russianName)
        {
            _tokenHash = tokenHash;
            _name = name;
            _russianName = russianName;
            RussianName = "Добавление новой роли";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var role = Context.Roles.FirstOrDefault(x => x.RussianName.ToLower() == _russianName.ToLower());
            if (role != null)
                Errors.Add("Name", "Такая роль уже существует!");
            else
            {
                _role = new Role
                {                    
                    RussianName = _russianName,
                };
                Context.Roles.Add(role);
                Context.SaveChanges();
            }            
        }
    }
}