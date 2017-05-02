using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Builders
{
    public class LoadBuildersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<Builder> _builders { get; set; }

        public LoadBuildersOperation(string tokenHash, int page, int count)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            RussianName = "Получение всех застройщиков";
        }

        protected override void InTransaction()
        {
            _builders = Context.Builders.Where(x => !x.Deleted)
                .Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}