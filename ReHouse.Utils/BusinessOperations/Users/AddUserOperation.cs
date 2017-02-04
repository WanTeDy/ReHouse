using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class AddUserOperation : BaseOperation
    {
        private String _login { get; set; }
        private String _email { get; set; }
        private String _password { get; set; }
        private Int32 _roleId { get; set; }
        private String _tokenHash { get; set; }
        public User _user { get; set; }

        public AddUserOperation(string login, string email, string password, int roleId)
        {
            _login = login;
            _email = email;
            _password = password;
            _roleId = roleId;
            RussianName = "Добавление нового пользователя";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var role = Context.Roles.FirstOrDefault(x => x.Id == _roleId && !x.Deleted);
            if (role == null)
                Errors.Add("RoleId", "Роль с Id= " + _roleId + " не найдена!");
            else
            {
                var userLogin = Context.Users.FirstOrDefault(x => !x.Deleted && x.Login.ToLower() == _login.ToLower());
                if (userLogin != null)
                    Errors.Add("Name", "Пользователь с таким логином уже существует!");
                else
                {
                    var userEmail = Context.Users.FirstOrDefault(x => !x.Deleted && x.Email.ToLower() == _email.ToLower());
                    if (userEmail != null)
                        Errors.Add("Email", "Такой электронный адрес уже используется!");
                    else
                    {
                        _user = new User
                        {
                            RoleId = role.Id,
                            Password = _password,
                            Email = _email,
                            Login = _login,
                            IsActive = true,
                            Deleted = false,
                            Role = role,
                        };
                        _user.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + _user.Password + Guid.NewGuid() + _user.Email + Guid.NewGuid());
                        _tokenHash = _user.TokenHash;
                        Context.Users.Add(_user);
                        Context.SaveChanges();
                    }
                }
            }
        }
    }
}