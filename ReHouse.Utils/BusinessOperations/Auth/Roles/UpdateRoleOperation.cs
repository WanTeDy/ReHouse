﻿using System;
using System.Data;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Auth.Roles
{
    public class UpdateRoleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _name { get; set; }
        private String _russianName { get; set; }
        private Int32 _roleId { get; set; }
        public Role _role { get; set; }


        public UpdateRoleOperation(string tokenHash, int roleId, string name, string russianName)
        {
            _tokenHash = tokenHash;
            _roleId = roleId;
            _name = name;
            _russianName = russianName;
            RussianName = "Изменение роли";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _role = Context.Roles.FirstOrDefault(x => x.Id == _roleId && !x.Deleted);
            if (_role == null)
                Errors.Add("Id", "Выбраная роль не найдена. RoleId = " + _roleId);
            else if (_role.RussianName == ConstV.RoleAdministrator)
            {
                Errors.Add("Id", "Нельзя изменять роль администратора!");
            }
            else
            {
                var role = Context.Roles.FirstOrDefault(x => x.RussianName == _russianName);
                if (role != null)
                    Errors.Add("Name", "Такая роль уже существует!");
                else
                {
                    role.RussianName = _russianName;
                    Context.SaveChanges();
                }
            }
        }
    }
}