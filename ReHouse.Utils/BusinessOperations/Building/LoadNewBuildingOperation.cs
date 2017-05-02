using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class LoadNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public NewBuilding _newBuilding { get; set; }
        public List<NewBuilding> _otherNewBuilding { get; set; }

        public LoadNewBuildingOperation(string tokenHash, int id, int page, int count)
        {
            _tokenHash = tokenHash;
            _id = id;
            _page = page;
            _count = count;
            RussianName = "Получение обьявления по новостройке";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _newBuilding = Context.NewBuildings.FirstOrDefault(x => !x.Deleted /*&& x.IsModerated*/ && x.Id == _id);
            if(_page != 0)
                _otherNewBuilding = Context.NewBuildings.Where(x => !x.Deleted /*&& x.IsModerated*/ && x.Id != _id).OrderByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
            if(_newBuilding != null)
                _newBuilding.BuildersId = _newBuilding.Builders?.Select(x => x.Id).ToList();
        }
    }
}