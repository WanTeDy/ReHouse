using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Auth.Roles
{
    public class DeleteRoleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _roleId { get; set; }
        public Role _role { get; set; }
        
        public DeleteRoleOperation(string tokenHash, int roleId)
        {
            _tokenHash = tokenHash;
            _roleId = roleId;
            RussianName = "Удаление ролей";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _role = Context.Roles.Include("Users").FirstOrDefault(x => x.Id == _roleId && !x.Deleted);
            if(_role == null)
                Errors.Add("Id", "Выбраная роль не найдена. RoleId = " + _roleId);
            else
            {
                if (_role.Users != null && _role.Users.Count > 0)
                {
                    Errors.Add("Id", "Нельзя удалить роль, которая назначена пользователям!");
                }
                else if(_role.RussianName == ConstV.RoleAdministrator)
                {
                    Errors.Add("Id", "Нельзя удалить роль администратора!");
                }
                else
                {
                    _role.Deleted = true;
                    Context.SaveChanges();
                }
            }            
        }
    }
}