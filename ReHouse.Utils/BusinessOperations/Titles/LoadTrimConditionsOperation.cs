using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Titles
{
    public class LoadTrimConditionsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<TrimCondition> _trimConditions { get; set; }

        public LoadTrimConditionsOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех состояний отделки";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var trimConditions = Context.TrimConditions.Where(x => !x.Deleted).ToList();
            _trimConditions = trimConditions.Select(x => new TrimCondition
            {
                Id = x.Id,
                RussianName = x.RussianName,
            }).ToList();
        }
    }
}