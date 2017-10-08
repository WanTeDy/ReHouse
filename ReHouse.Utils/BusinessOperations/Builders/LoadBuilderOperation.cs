using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Builders
{
    public class LoadBuilderOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        public Builder _builder { get; set; }

        public LoadBuilderOperation(string tokenHash, int id)
        {
            _tokenHash = tokenHash;
            _id = id;
            RussianName = "Получение застройщика";
        }

        protected override void InTransaction()
        {
            //new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _builder = Context.Builders.FirstOrDefault(x => x.Id == _id && !x.Deleted);
        }
    }
}