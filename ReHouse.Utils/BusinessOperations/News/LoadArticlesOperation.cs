using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class LoadArticlesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<Article> _articles { get; set; }

        public LoadArticlesOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех новостей с фильтром по свежести";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var articles = Context.Articles.Include("User").Include("Image").Where(x => !x.Deleted).OrderByDescending(x => x.Date).ToList();
            _articles = articles.Select(x => new Article
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Date = x.Date,
                UserId = x.UserId,
                User = x.User,
                ImageId = x.ImageId,
                Image = x.Image,
            }).ToList();
        }
    }
}