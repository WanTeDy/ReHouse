using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class LoadNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private Boolean _isAdmin { get; set; }
        public NewBuilding _newBuilding { get; set; }
        public List<NewBuilding> _otherNewBuilding { get; set; }

        public LoadNewBuildingOperation(string tokenHash, int id, int page, int count, bool isAdmin = false)
        {
            _tokenHash = tokenHash;
            _id = id;
            _page = page;
            _count = count;
            _isAdmin = isAdmin;
            RussianName = "Получение обьявления по новостройке";
        }

        protected override void InTransaction()
        {
            if (_isAdmin)
            {
                new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
                var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
                _newBuilding = Context.NewBuildings.FirstOrDefault(x => !x.Deleted && x.Id == _id);
                if (_newBuilding != null)
                {
                    if (user == null || (user.Role.RussianName != ConstV.RoleAdministrator && user.Role.RussianName != ConstV.RoleManager && user.Role.RussianName != ConstV.RoleSeo && user.Id != _newBuilding.UserId))
                        throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции");
                }
                else
                    return;
            }
            else
            {
                _newBuilding = Context.NewBuildings.FirstOrDefault(x => !x.Deleted && x.IsModerated && x.Id == _id);
            }
            if (_newBuilding != null)
            {
                if (_page != 0)
                    _otherNewBuilding = Context.NewBuildings.Where(x => !x.Deleted && x.IsModerated && x.Id != _id).OrderByDescending(x => x.CreationDate).Skip((_page - 1) * _count).Take(_count).ToList();

                _newBuilding.BuildersId = _newBuilding.Builders.Select(x => x.Id).ToList();
            }
        }
    }
}