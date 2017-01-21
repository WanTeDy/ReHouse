using System;
using System.Linq;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class UpdateRoleForUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _userId { get; set; }
        private Int32 _roleId { get; set; }

        public UpdateRoleForUserOperation(int userId, int roleId, string tokenHash)
        {
            _userId = userId;
            _roleId = roleId;
            _tokenHash = tokenHash;
            RussianName = "Изменение роли пользователя";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.FirstOrDefault(x => x.Id == _userId && !x.Deleted && x.IsActive);
            if (user == null)
            {
                Errors.Add("UserId", "Такого пользователя не существует");
            }
            else
            {
                if (user.RoleId != _roleId)
                {
                    if (user.Role.RussianName == ConstV.RoleAdministrator)
                    {
                        Errors.Add("RoleId", "Нельзя изменять пользователя с ролью администратора!");
                    }
                    else
                    {
                        var role = Context.Roles.FirstOrDefault(x => x.Id == _roleId && !x.Deleted);
                        if (role == null)
                            Errors.Add("RoleId", "Выбраная роль не найдена. RoleId = " + _roleId);
                        else if (role.RussianName == ConstV.RoleAdministrator)
                        {
                            Errors.Add("RoleId", "Нельзя больше делать администраторов!");
                        }
                        else
                        {
                            user.RoleId = _roleId;
                            Context.SaveChanges();
                        }
                    }
                }
            }




        }
    }
}