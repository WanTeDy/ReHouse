using System;
using System.Linq;
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
            //TODO: Correct checking phones and roles. Noone can set admin role. Only 1 user admin can be admin         
            //if (user.Phone != _user.Phone)
            //{
            //    var otherPhone = Context.Contractors.FirstOrDefault(c => c.Phone == _user.Phone);
            //    if (otherPhone != null)
            //        throw new ExistsObjectException("Такой телефон уже сужествует.");
            //    user.Phone = _user.Phone;
            //}
            if(user.RoleId != _user.RoleId)
            {

            }
            //if (_user.RoleId != 0 && CommonAccess.CheckContractorRoleAuthorityBool(Context, _tokenHash, ConstV.ChangeRoles))
            //    x.RoleId = _user.RoleId;
            //if (CommonAccess.CheckContractorRoleAuthorityBool(Context, _tokenHash, ConstV.ChangeCreditLimit))
            //    x.CreditLimit = _user.CreditLimit;
            //if (CommonAccess.CheckContractorRoleAuthorityBool(Context, _tokenHash, ConstV.IsActive))
            //    x.IsActive = _user.IsActive;
            //x.Password = _user.Password;
        }        
    }
}