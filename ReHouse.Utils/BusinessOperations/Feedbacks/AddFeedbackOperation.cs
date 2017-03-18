using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class AddFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _title { get; set; }
        private String _description { get; set; }
        public Article _article { get; set; }

        public AddFeedbackOperation(string tokenHash, string title, string description)
        {
            _tokenHash = tokenHash;
            _title = title;
            _description = description;
            RussianName = "Добавление новой новости";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted);
            if (user == null)
                Errors.Add("Id", "Неверный TokemHash пользователя");
            else
            {
                var article = Context.Articles.FirstOrDefault(x => x.Title.ToLower() == _title.ToLower());
                if (article != null)
                    Errors.Add("Title", "Такой заголовок новости уже существует!");
                else
                {
                    _article = new Article
                    {
                        Title = _title,
                        Description = _description,
                        Date = DateTime.Now,
                        UserId = user.Id,
                        Deleted = false,
                    };
                    Context.Articles.Add(article);
                    Context.SaveChanges();
                }
            }           
        }
    }
}