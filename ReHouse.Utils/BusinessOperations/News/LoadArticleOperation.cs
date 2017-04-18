using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class LoadArticleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        public Article _article { get; set; }

        public LoadArticleOperation(string tokenHash, int id)
        {
            _tokenHash = tokenHash;
            _id = id;
            RussianName = "Получение новости";
        }

        protected override void InTransaction()
        {
            _article = Context.Articles.FirstOrDefault(x => x.Id == _id && !x.Deleted);
        }
    }
}