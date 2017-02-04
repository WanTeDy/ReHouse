using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Auth.Roles
{
    public class UpdateRoleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _russianName { get; set; }
        private List<Authority> _authorities { get; set; }
        private Int32 _roleId { get; set; }
        public Role _role { get; set; }


        public UpdateRoleOperation(string tokenHash, int roleId, string russianName, List<Authority> authorities)
        {
            _tokenHash = tokenHash;
            _roleId = roleId;
            _russianName = russianName;
            RussianName = "Изменение роли";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _role = Context.Roles.Include("Authorities").FirstOrDefault(x => x.Id == _roleId && !x.Deleted);
            if (_role == null)
                Errors.Add("Id", "Выбраная роль не найдена. RoleId = " + _roleId);
            else if (_role.RussianName == ConstV.RoleAdministrator)
            {
                Errors.Add("Id", "Нельзя изменять роль администратора!");
            }
            else
            {
                if(_role.RussianName.ToLower() == _russianName.ToLower())
                    Errors.Add("Name", "Такая роль уже существует!");
                else
                {
                    _role.Authorities = _authorities;
                    _role.RussianName = _russianName;
                    Context.SaveChanges();
                }
            }
        }
    }
}