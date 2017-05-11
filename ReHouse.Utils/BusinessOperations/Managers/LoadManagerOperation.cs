using System;
using System.Linq;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Managers
{
    public class LoadManagerOperation : BaseOperation
    {
        private Int32 _length = 90;
        private Int32 _subLength = 85;
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
            if (_user != null)
            {
                _user.Adverts = _user.Adverts.Where(x => !x.Deleted/* && x.IsModerated*/).OrderByDescending(x => x.IsHot)
                        .ThenByDescending(x => x.PublicationDate).ToList();
                _user.Adverts.ForEach(
                        x =>
                        {
                            if (!String.IsNullOrEmpty(x.Description) && x.Description.Length > _length)
                                x.Description = x.Description.Substring(0, _subLength) + "...";
                        });

                _user.NewBuildings = _user.NewBuildings.Where(x => !x.Deleted /*&& x.IsModerated*/).OrderByDescending(x => x.IsHot)
                        .ThenByDescending(x => x.PublicationDate).ToList();

            }
        }
    }
}