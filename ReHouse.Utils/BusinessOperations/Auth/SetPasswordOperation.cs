using System;
using System.Linq;
using ReHouse.Utils.Except;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Auth
{
    public class SetPasswordOperation : BaseOperation
    {
        private String _password { get; set; }
        private String _tokenHash { get; set; }
        public User _user { get; set; }        

        public SetPasswordOperation(string password, string tokenHash)
        {
            _password = password;
            _tokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            var user = Context.Users.Include("Phones").Include("Role").FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted && x.IsActive);
            if (user == null)
                throw new ActionNotAllowedException("Данный " + _tokenHash + " не найден");
            user.Password = _password;
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
                Role = user.Role,
            };
            Context.SaveChanges();
        }
    }
}