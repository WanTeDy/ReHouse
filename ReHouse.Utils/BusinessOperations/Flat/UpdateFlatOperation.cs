using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class UpdateFlatOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public Advert _model { get; set; }
        private IEnumerable<HttpPostedFileBase> _images { get; set; }
        private IEnumerable<HttpPostedFileBase> _planImages { get; set; }
        public Advert _advert { get; set; }


        public UpdateFlatOperation(string tokenHash, Advert advert, IEnumerable<HttpPostedFileBase> images, IEnumerable<HttpPostedFileBase> planImages)
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
            if (_model.Price < 0)
            {
                Errors.Add("Price", "Цена не может быть отрицательная");
            }
            else
            {
                _advert = Context.Adverts.FirstOrDefault(x => x.Id == _model.Id && !x.Deleted);
                if (_advert != null)
                {
                    var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
                    if (user != null && (_advert.UserId == user.Id || user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager))
                    {
                        if (_images != null)
                        {
                            if (_advert.Images == null)
                                _advert.Images = new List<Image>();

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
                            if (_advert.Images == null)
                                _advert.PlanImages = new List<PlanImage>();

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
                        _advert.IsHot = _model.IsHot;
                        _advert.CategoryId = _model.CategoryId;
                        _advert.TitleId = _model.TitleId;
                        _advert.DistrictId = _model.DistrictId;
                        _advert.Price = _model.Price;
                        _advert.Description = _model.Description;
                        _advert.Street = _model.Street;
                        _advert.YouTubeUrl = _model.YouTubeUrl;
                        _advert.MarketTypeId = _model.MarketTypeId;
                        _advert.TrimConditionId = _model.TrimConditionId;
                        _advert.Latitude = _model.Latitude;
                        _advert.Longitude = _model.Longitude;
                        _advert.IsHot = _model.IsHot;
                        _advert.IsExclusive = _model.IsExclusive;
                        _advert.IsModerated = _model.IsModerated;
                        foreach (var prop in _model.AdvertPropertyValues)
                        {
                            var property = _advert.AdvertPropertyValues.FirstOrDefault(x => x.Id == prop.Id);
                            property.PropertiesValue = prop.PropertiesValue;
                        }
                        Context.SaveChanges();
                    }
                }
                else
                {
                    throw new ActionNotAllowedException("Недостаточно прав на редактирование чужих объектов");
                }
            }
        }
    }
}