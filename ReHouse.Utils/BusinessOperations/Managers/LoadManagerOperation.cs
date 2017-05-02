using System;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Managers
{
    public class LoadManagerOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _userId { get; set; }
        public User _user { get; set; }

        public LoadManagerOperation(string tokenHash, int userId)
        {
            _tokenHash = tokenHash;
            _userId = userId;
            RussianName = "Просмотр данных менеджера";
        }

        protected override void InTransaction()
        {
            _user = Context.Users.FirstOrDefault(x => x.Id == _userId && !x.Deleted && x.IsActive && x.Role.RussianName != ConstV.RoleAdministrator);            
        }
    }
}