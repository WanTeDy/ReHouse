using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Auth
{
    public class CheckSignInDataOperation : BaseOperation
    {
        private String _login { get; set; }
        private String _password { get; set; }
        private String _tokenHash { get; set; }
        private Role _role { get; set; }
        public User _user { get; set; }

        public CheckSignInDataOperation(string login, string password)
        {
            _login = login;
            _password = password;
        }

        protected override void InTransaction()
        {
            var user = Context.Users.Include("Phones").FirstOrDefault(x => (x.Email == _login || x.Login == _login) && x.Password == _password && !x.Deleted && x.IsActive);
            if (user == null)
                Errors.Add("Email", "Неправильный логин или пароль");
            else
            {
                user.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + user.Password + Guid.NewGuid() + user.Email);
                _tokenHash = user.TokenHash;
                var role = Context.Roles.Include("Authorities").FirstOrDefault(x => x.Id == user.RoleId && !x.Deleted);
                if (role != null)
                {
                    _role = new Role
                    {
                        Id = role.Id,
                        RussianName = role.RussianName,
                        Authorities = role.Authorities,
                    };
                }

                _user = new User
                {
                    FatherName = user.FatherName,
                    Email = user.Email,
                    SecondName = user.SecondName,
                    TokenHash = user.TokenHash,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    IsActive = user.IsActive,
                    Phones = user.Phones,
                    Adress = user.Adress,
                    Login = user.Login,
                    RoleId = user.RoleId,
                    Role = _role,
                };

                Context.SaveChanges();
            }
        }
    }
}