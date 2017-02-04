using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class UpdateArticleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _title { get; set; }
        private String _description { get; set; }
        private Int32 _articleId { get; set; }
        public Article _article { get; set; }

        public UpdateArticleOperation(string tokenHash, int articleId, string title, string description)
        {
            _tokenHash = tokenHash;
            _articleId = articleId;
            _title = title;
            _description = description;
            RussianName = "Изменение новости";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _article = Context.Articles.FirstOrDefault(x => x.Id == _articleId && !x.Deleted);
            if (_article == null)
                Errors.Add("Id", "Выбранная новость не найдена. ArticleId = " + _articleId);            
            else
            {
                if(_article.Title.ToLower() == _title.ToLower())                
                    Errors.Add("Title", "Такой заголовок новости уже существует!");
                else
                {
                    _article.Title = _title;
                    _article.Description = _description;
                    Context.SaveChanges();
                }
            }
        }
    }
}