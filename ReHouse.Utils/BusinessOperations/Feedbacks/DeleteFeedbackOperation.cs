using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class DeleteFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _articleId { get; set; }
        public Article _article { get; set; }

        public DeleteFeedbackOperation(string tokenHash, int articleId)
        {
            _tokenHash = tokenHash;
            _articleId = articleId;
            RussianName = "Удаление новости";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _article = Context.Articles.FirstOrDefault(x => x.Id == _articleId && !x.Deleted);
            if (_article == null)
                Errors.Add("Id", "Выбранная новость не найдена. ArticleId = " + _articleId);
            else
            {
                _article.Deleted = true;
                Context.SaveChanges();
            }
        }
    }
}