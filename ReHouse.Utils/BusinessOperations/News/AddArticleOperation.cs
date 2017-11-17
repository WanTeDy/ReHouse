using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Collections.Generic;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class AddArticleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _title { get; set; }
        private String _description { get; set; }
        private HttpPostedFileBase _image { get; set; }
        public Article _article { get; set; }

        public AddArticleOperation(string tokenHash, string title, string description, HttpPostedFileBase image)
        {
            _tokenHash = tokenHash;
            _title = title;
            _description = description;
            _image = image;
            RussianName = "Добавление новой новости";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
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
                    if (_image != null)
                    {
                        var url = "~/Content/images/news/";

                        var path = HttpContext.Current.Server.MapPath(url);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        _image.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                        int point = _image.FileName.LastIndexOf('.');
                        var filename = _image.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                        ImageBuilder.Current.Build(
                            new ImageJob(_image.InputStream,
                            path + filename,
                            new Instructions("maxwidth=1500&maxheight=1500&format=jpg&quality=80&watermark=water"),
                            false,
                            true));

                        var image = new Image
                        {
                            FileName = filename + ".jpg",
                            Url = url,
                        };
                        Context.Images.Add(image);
                        _article.Images = new List<Image> { image };
                    }
                    Context.Articles.Add(_article);
                    Context.SaveChanges();
                }
            }           
        }
    }
}