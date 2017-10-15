using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.Except;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class UpdateFlatOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public Advert _model { get; set; }
        private IEnumerable<String> _images { get; set; }
        private IEnumerable<String> _planImages { get; set; }
        private Image[] _imageData { get; set; }
        private PlanImage[] _planimageData { get; set; }
        public Advert _advert { get; set; }


        public UpdateFlatOperation(string tokenHash, Advert advert, IEnumerable<String> images, IEnumerable<String> planImages, Image[] imageData, PlanImage[] planimageData)
        {
            _tokenHash = tokenHash;
            _model = advert;
            _images = images;
            _planImages = planImages;
            _imageData = imageData;
            _planimageData = planimageData;
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
                    if (user != null && (_advert.UserId == user.Id || user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager || user.Role.RussianName == ConstV.RoleSeo))
                    {
                        var random = new Random(DateTime.Now.Millisecond);
                        if (_images != null)
                        {
                            if (_advert.Images == null)
                                _advert.Images = new List<Image>();

                            foreach (var imageFile in _images)
                            {
                                if (imageFile != null)
                                {
                                    var url = "~/Content/images/flats/images/";

                                    var path = HttpContext.Current.Server.MapPath(url);
                                    if (!Directory.Exists(path))
                                        Directory.CreateDirectory(path);

                                    byte[] data = System.Convert.FromBase64String(GenerateHash.FixBase64ForImage(imageFile));
                                    MemoryStream ms = new MemoryStream(data);
                                    //imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                    //int point = imageFile.FileName.LastIndexOf('.');
                                    var filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                                    ImageBuilder.Current.Build(
                                        new ImageJob(ms, //imageFile.InputStream,
                                        path + filename,
                                        new Instructions("maxwidth=1200&maxheight=1200&format=jpg&quality=70&watermark=water"),
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
                                    var url = "~/Content/images/flats/plans/";

                                    var path = HttpContext.Current.Server.MapPath(url);
                                    if (!Directory.Exists(path))
                                        Directory.CreateDirectory(path);

                                    byte[] data = System.Convert.FromBase64String(GenerateHash.FixBase64ForImage(imageFile));
                                    MemoryStream ms = new MemoryStream(data);
                                    //imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                    //int point = imageFile.FileName.LastIndexOf('.');
                                    var filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                                    ImageBuilder.Current.Build(
                                        new ImageJob(ms, //imageFile.InputStream,
                                        path + filename,
                                        new Instructions("maxwidth=1200&maxheight=1200&format=jpg&quality=70&watermark=water"),
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
                        if (_model.IsModerated && _advert.PublicationDate == null)
                            _advert.PublicationDate = DateTime.Now;

                        _advert.IsModerated = _model.IsModerated;
                        _advert.RentPeriodType = _model.RentPeriodType;
                        //_advert.TitleName = _model.TitleName;
                        foreach (var prop in _model.AdvertPropertyValues)
                        {
                            var property = _advert.AdvertPropertyValues.FirstOrDefault(x => x.Id == prop.Id);
                            property.PropertiesValue = prop.PropertiesValue;
                        }
                        if (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleSeo)
                        {
                            if (_imageData != null)
                            {
                                foreach (var img in _imageData)
                                {
                                    var image = Context.Images.FirstOrDefault(x => x.Id == img.Id && !x.Deleted);
                                    if (image != null)
                                    {
                                        image.Title = img.Title;
                                        image.Alt = img.Alt;
                                    }
                                }
                            }
                            if (_planimageData != null)
                            {
                                foreach (var img in _planimageData)
                                {
                                    var image = Context.PlanImages.FirstOrDefault(x => x.Id == img.Id && !x.Deleted);
                                    if (image != null)
                                    {
                                        image.Title = img.Title;
                                        image.Alt = img.Alt;
                                    }
                                }
                            }
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