using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class UpdateArticleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _title { get; set; }
        private String _description { get; set; }
        private Int32 _articleId { get; set; }
        private HttpPostedFileBase _image { get; set; }
        private Image _imageData { get; set; }
        public Article _article { get; set; }

        public UpdateArticleOperation(string tokenHash, int articleId, string title, string description, HttpPostedFileBase image, Image imageData)
        {
            _tokenHash = tokenHash;
            _articleId = articleId;
            _title = title;
            _description = description;
            _image = image;
            _imageData = imageData;
            RussianName = "Изменение новости";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _article = Context.Articles.FirstOrDefault(x => x.Id == _articleId && !x.Deleted);
            if (_article == null)
                Errors.Add("Id", "Выбранная новость не найдена. ArticleId = " + _articleId);
            else
            {
                var article = Context.Articles.FirstOrDefault(x => x.Title.ToLower() == _title.ToLower());
                if (article != null && _article.Id != article.Id)
                    Errors.Add("Title", "Такой заголовок новости уже существует!");
                else
                {
                    var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
                    if (user != null && (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager || user.Role.RussianName == ConstV.RoleSeo))
                    {
                        if (_image != null)
                        {
                            var random = new Random(DateTime.Now.Millisecond);
                            var url = "~/Content/images/news/";

                            var path = HttpContext.Current.Server.MapPath(url);
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            _image.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                            int point = _image.FileName.LastIndexOf('.');
                            var filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();
                            while (File.Exists(path + filename))
                            {
                                filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();
                            }
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

                            var deleteImg = _article.Images.FirstOrDefault();
                            if (deleteImg != null)
                            {
                                FileInfo fileInf = new FileInfo(path + deleteImg.FileName);
                                if (fileInf.Exists)
                                {
                                    fileInf.Delete();
                                }
                                Context.Images.Remove(deleteImg);
                            }
                            Context.Images.Add(image);
                            _article.Images = new List<Image> { image };
                        }
                        if (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleSeo)
                        {
                            var image = _article.Images.FirstOrDefault(x => !x.Deleted);
                            if (image != null)
                            {
                                image.Title = _imageData.Title;
                                image.Alt = _imageData.Alt;
                            }
                        }
                        _article.Title = _title;
                        _article.Description = _description;
                        Context.SaveChanges();
                    }
                    else
                    {
                        throw new ActionNotAllowedException("Недостаточно прав на редактирование чужих новостей");
                    }
                }
            }
        }
    }
}