using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class LoadPartnersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<Partner> _partners { get; set; }

        public LoadPartnersOperation(string tokenHash, int page, int count)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            RussianName = "Получение всех партнеров с фильтром по свежести";
        }

        protected override void InTransaction()
        {
            _partners = Context.Partners.Where(x => !x.Deleted).OrderByDescending(x => x.CreationDate)
                .Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}