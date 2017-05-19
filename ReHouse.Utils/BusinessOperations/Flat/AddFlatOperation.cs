using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Web;
using System.IO;
using ImageResizer;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class AddFlatOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Advert _model { get; set; }
        private IEnumerable<HttpPostedFileBase> _images { get; set; }
        private IEnumerable<HttpPostedFileBase> _planImages { get; set; }
        public Category _category { get; set; }


        public AddFlatOperation(string tokenHash, Advert advert, IEnumerable<HttpPostedFileBase> images, IEnumerable<HttpPostedFileBase> planImages)
        {
            _tokenHash = tokenHash;
            _model = advert;
            _images = images;
            _planImages = planImages;
            RussianName = "Изменение объявлений";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
            if (user == null)
            {
                Errors.Add("Token", "Некорректный Token");
            }
            else
            {
                if (_model.Price < 0)
                {
                    Errors.Add("Price", "Цена не может быть отрицательная");
                }
                else
                {
                    _category = Context.Categories.FirstOrDefault(x => !x.Deleted && x.Id == _model.CategoryId);
                    var _advert = new Advert
                    {
                        CategoryId = _model.CategoryId,
                        TitleId = _model.TitleId,
                        DistrictId = _model.DistrictId,
                        Price = _model.Price,
                        Description = _model.Description,
                        Street = _model.Street,
                        YouTubeUrl = _model.YouTubeUrl,
                        MarketTypeId = _model.MarketTypeId,
                        TrimConditionId = _model.TrimConditionId,
                        ExpireDate = DateTime.Now.AddMonths(2),
                        PublicationDate = DateTime.Now,
                        Type = _model.Type,
                        UserId = user.Id,
                        AdvertPropertyValues = new List<AdvertPropertyValue>(),
                        Images = new List<Image>(),
                        PlanImages = new List<PlanImage>(),
                        IsHot = _model.IsHot,
                        Latitude = _model.Latitude,
                        Longitude = _model.Longitude,
                        IsExclusive = _model.IsExclusive,
                        IsModerated = _model.IsModerated,
                        TitleName = _model.TitleName,
                    };
                    if (_images != null)
                    {
                        foreach (var imageFile in _images)
                        {
                            if (imageFile != null)
                            {
                                var url = "~/Content/images/adverts/images/";

                                var path = HttpContext.Current.Server.MapPath(url);
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                int point = imageFile.FileName.LastIndexOf('.');
                                var filename = imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                                ImageBuilder.Current.Build(
                                    new ImageJob(imageFile.InputStream,
                                    path + filename,
                                    new Instructions("maxwidth=1200&maxheight=1200&format=jpg&quality=80&watermark=water"),
                                    false,
                                    true));

                                var image = new Image
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };
                                Context.Images.Add(image);
                                _advert.Images.Add(image);
                            }
                        }
                    }

                    if (_planImages != null)
                    {
                        foreach (var imageFile in _planImages)
                        {
                            if (imageFile != null)
                            {
                                var url = "~/Content/images/adverts/plans/";

                                var path = HttpContext.Current.Server.MapPath(url);
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                int point = imageFile.FileName.LastIndexOf('.');
                                var filename = imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                                ImageBuilder.Current.Build(
                                    new ImageJob(imageFile.InputStream,
                                    path + filename,
                                    new Instructions("maxwidth=1200&maxheight=1200&format=jpg&quality=80&watermark=water"),
                                    false,
                                    true));

                                var image = new PlanImage
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };
                                Context.PlanImages.Add(image);
                                _advert.PlanImages.Add(image);
                            }
                        }
                    }

                    foreach (var prop in _model.AdvertPropertyValues)
                    {
                        var value = new AdvertPropertyValue
                        {
                            AdvertPropertyId = prop.AdvertPropertyId,
                            PropertiesValue = prop.PropertiesValue,
                        };
                        Context.AdvertPropertyValues.Add(value);
                        _advert.AdvertPropertyValues.Add(value);
                    }
                    Context.Adverts.Add(_advert);
                    Context.SaveChanges();
                }
            }
        }
    }
}