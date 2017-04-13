using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class LoadArticlesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<Article> _articles { get; set; }

        public LoadArticlesOperation(string tokenHash, int page, int count)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            RussianName = "Получение всех новостей с фильтром по свежести";
        }

        protected override void InTransaction()
        {
            _articles = Context.Articles.Where(x => !x.Deleted).OrderByDescending(x => x.Date)
                .Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}