using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class AddFlatOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Advert _model { get; set; }
        private IEnumerable<String> _images { get; set; }
        private IEnumerable<String> _planImages { get; set; }
        public Category _category { get; set; }
        public Advert _advert { get; set; }


        public AddFlatOperation(string tokenHash, Advert advert, IEnumerable<String> images, IEnumerable<String> planImages)
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
                    _advert = new Advert
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
                        CreationDate = DateTime.Now,
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
                        RentPeriodType = _model.RentPeriodType,
                        //TitleName = _model.TitleName,
                    };
                    var random = new Random(DateTime.Now.Millisecond);
                    if (_images != null)
                    {
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
                                while (File.Exists(path + filename))
                                {
                                    filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();
                                }
                                ImageBuilder.Current.Build(
                                    new ImageJob(ms, //imageFile.InputStream,
                                    path + filename,
                                    new Instructions("maxwidth=1600&maxheight=1600&format=jpg&quality=90&watermark=water"),
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
                                var url = "~/Content/images/flats/plans/";

                                var path = HttpContext.Current.Server.MapPath(url);
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                byte[] data = System.Convert.FromBase64String(GenerateHash.FixBase64ForImage(imageFile));
                                MemoryStream ms = new MemoryStream(data);
                                //imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                //int point = imageFile.FileName.LastIndexOf('.');
                                var filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();
                                while (File.Exists(path + filename))
                                {
                                    filename = HashHelper.GetMd5Hash("image_" + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();
                                }
                                ImageBuilder.Current.Build(
                                    new ImageJob(ms, //imageFile.InputStream,
                                    path + filename,
                                    new Instructions("maxwidth=1600&maxheight=1600&format=jpg&quality=90&watermark=water"),
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