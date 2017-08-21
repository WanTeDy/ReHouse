using System;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class LoadUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _userId { get; set; }
        public User _user { get; set; }

        public LoadUserOperation(string tokenHash, int userId)
        {
            _tokenHash = tokenHash;
            _userId = userId;
            RussianName = "Просмотр данных пользователя";
        }

        protected override void InTransaction()
        {
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted && x.IsActive);
            if (user == null)
            {
                Errors.Add("Id", "Неверный Token");
            }
            else
            {
                if (user.Id != _userId)
                {
                    var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
                }
                var userForWatching = Context.Users.FirstOrDefault(x => x.Id == _userId && !x.Deleted && x.IsActive);
                if (userForWatching != null)
                {
                    _user = new User
                    {
                        Id = userForWatching.Id,
                        Login = userForWatching.Login,
                        RoleId = userForWatching.RoleId,
                        Adress = userForWatching.Adress,
                        SecondName = userForWatching.SecondName,
                        Phones = userForWatching.Phones,
                        FirstName = userForWatching.FirstName,
                        FatherName = userForWatching.FatherName,
                        Position = userForWatching.Position,
                        Role = userForWatching.Role,
                        Email = userForWatching.Email,
                        Avatar = userForWatching.Avatar,
                        AvatarId = userForWatching.AvatarId,
                        //Password = userForWatching.Password,
                        IsActive = userForWatching.IsActive,
                        Deleted = userForWatching.Deleted,
                    };
                }
            }            
        }
    }
}