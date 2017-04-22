using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class DeleteArticleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _articlesId { get; set; }
        public Article _article { get; set; }

        public DeleteArticleOperation(string tokenHash, int[] articlesId)
        {
            _tokenHash = tokenHash;
            _articlesId = articlesId;
            RussianName = "Удаление новости";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_articlesId != null && _articlesId.Length > 0)
            {
                foreach (var articleId in _articlesId)
                {
                    var article = Context.Articles.FirstOrDefault(x => x.Id == articleId);
                    if (article != null)
                        article.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}