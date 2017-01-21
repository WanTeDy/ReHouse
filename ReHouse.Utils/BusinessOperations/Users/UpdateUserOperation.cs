using System;
using System.Linq;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class UpdateUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public User _user { get; set; }

        public UpdateUserOperation(User user, string tokenHash)
        {
            _user = user;
            _tokenHash = tokenHash;
            RussianName = "Изменение данных пользователя";
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
                if (user.Id != _user.Id)
                {
                    var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
                }
                var userForUpdating = Context.Users.Include("Role").Include("Phones").FirstOrDefault(x => x.Id == _user.Id && !x.Deleted);
                if (userForUpdating != null)
                {
                    SetFields(userForUpdating);
                    if (Success)
                    {
                        Context.SaveChanges();
                    }
                }
                else
                    Errors.Add("Id", "Пользователя с ID=" + _user.Id + " не найден");
            }
        }

        private void SetFields(User user)
        {
            user.Adress = _user.Adress;
            user.FirstName = _user.FirstName;
            user.SecondName = _user.SecondName;
            user.FatherName = _user.FatherName;
            if (user.Email != _user.Email)
            {
                var otherEmail = Context.Users.FirstOrDefault(x => x.Email == _user.Email);
                if (otherEmail == null)
                    user.Email = _user.Email;
                else
                    Errors.Add("Email", "Такой email уже сужествует.");
            }
            //TODO: Correct checking phones.        
            if (user.Phones.Count > 0)
            {
                IEnumerable<Phone> newPhones = null;
                foreach (var phone in _user.Phones)
                {
                    newPhones = user.Phones.Where(x => x.TelePhone != phone.TelePhone);
                }
                if (newPhones != null)
                {
                    bool exists = false;
                    foreach (var phone in newPhones)
                    {
                        var searchPhone = Context.Phones.FirstOrDefault(x => x.TelePhone == phone.TelePhone && !x.Deleted);
                        if (searchPhone != null)
                            exists = true;
                    }
                    if(exists)
                        Errors.Add("Phone", "Такой телефон уже существует");
                    else
                    {
                        user.Phones = _user.Phones;
                    }
                }
            }
            //TODO: This must be the new operation named like UpdateRoleForUserOperation.   Noone can set admin role. Only 1 user admin can be admin  
            //if (user.RoleId != _user.RoleId)
            //{
            //    if (user.Role.RussianName == ConstV.RoleAdministrator)
            //    {
            //        Errors.Add("Id", "Нельзя изменять пользователя с ролью администратора!");
            //    }
            //    else
            //    {
            //        var role = Context.Roles.FirstOrDefault(x => x.Id == _user.RoleId && !x.Deleted);
            //        if (role == null)
            //            Errors.Add("Id", "Выбраная роль не найдена. RoleId = " + _user.RoleId);
            //        else if (role.RussianName == ConstV.RoleAdministrator)
            //        {
            //            Errors.Add("Id", "Нельзя больше иметь администраторов!");
            //        }
            //        else
            //        {
            //            user.RoleId = _user.RoleId;
            //        }
            //    }
            //}
        }
    }
}