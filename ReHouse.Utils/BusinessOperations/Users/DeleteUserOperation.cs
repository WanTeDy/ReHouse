using System;
using System.Linq;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class DeleteUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _usersId { get; set; }

        public DeleteUserOperation(string tokenHash, int[] usersId)
        {
            _usersId = usersId;
            _tokenHash = tokenHash;
            RussianName = "Удаление пользователя";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_usersId != null && _usersId.Length > 0)
            {
                foreach (var userId in _usersId)
                {
                    var user = Context.Users.FirstOrDefault(x => x.Id == userId && !x.Deleted);
                    if (user != null && user.Role.RussianName != ConstV.RoleAdministrator)
                        user.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}