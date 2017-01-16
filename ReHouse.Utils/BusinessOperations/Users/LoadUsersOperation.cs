using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class LoadUsersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<User> _users { get; set; }

        public LoadUsersOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Загрузка списка пользователей";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var users = Context.Users.ToList();
            if (users.Count > 0)
            {
                _users = users.Select(x => new User
                {
                    Id = x.Id,
                    Adress = x.Adress,
                    Login = x.Login,
                    Email = x.Email,
                    FatherName = x.FatherName,
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    Phones = x.Phones,
                    RoleId = x.RoleId,
                    IsActive = x.IsActive,
                    Deleted = x.Deleted,
                    Role = x.Role,                    
                }).ToList();
            }
        }
    }
}