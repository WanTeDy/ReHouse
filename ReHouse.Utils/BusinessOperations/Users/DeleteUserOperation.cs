using System;
using System.Linq;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class DeleteUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _userId { get; set; }

        public DeleteUserOperation(int userId, string tokenHash)
        {
            _userId = userId;
            _tokenHash = tokenHash;
            RussianName = "Удаление пользователя";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.Include("Role").FirstOrDefault(x => x.Id == _userId && !x.Deleted && x.IsActive);
            if (user != null)
            {
                Errors.Add("Id", "Пользователь с ID=" + _userId + " не найден!");
            }
            else if(user.Role.RussianName == ConstV.RoleAdministrator)
            {
                Errors.Add("Id", "Нельзя удалить пользователя, у которого роль " + ConstV.RoleAdministrator);
            }
            else
            {
                user.Deleted = true;
                Context.SaveChanges();
            }
        }
    }
}