using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Web;
using System.IO;
using ImageResizer;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class UpdateFlatOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Advert _advert { get; set; }
        private IEnumerable<HttpPostedFileBase> _images { get; set; }
        private IEnumerable<HttpPostedFileBase> _planImages { get; set; }


        public UpdateFlatOperation(string tokenHash, Advert advert, IEnumerable<HttpPostedFileBase> images, IEnumerable<HttpPostedFileBase> planImages)
        {
            _tokenHash = tokenHash;
            _advert = advert;
            _images = images;
            _planImages = planImages;
            RussianName = "Изменение объявлений";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_advert.Price < 0)
            {
                Errors.Add("Price", "Цена не может быть отрицательная");
            }
            else
            {
                var advert = Context.Adverts.FirstOrDefault(x => x.Id == _advert.Id && !x.Deleted);
                if (advert != null)
                {
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
                                    new Instructions("maxwidth=1600&maxheight=1200&format=jpg&quality=80"),
                                    false,
                                    true));

                                var image = new Image
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };
                                Context.Images.Add(image);
                                advert.Images.Add(image);
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
                                    new Instructions("maxwidth=1600&maxheight=1200&format=jpg&quality=80"),
                                    false,
                                    true));

                                var image = new PlanImage
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };
                                Context.PlanImages.Add(image);
                                advert.PlanImages.Add(image);
                            }
                        }
                    }
                    advert.CategoryId = _advert.CategoryId;
                    advert.TitleId = _advert.TitleId;
                    advert.Price = _advert.Price;
                    advert.Description = _advert.Description;
                    advert.Street = _advert.Street;
                    advert.YouTubeUrl = _advert.YouTubeUrl;
                    advert.MarketTypeId = _advert.MarketTypeId;
                    advert.TrimConditionId = _advert.TrimConditionId;
                    foreach (var prop in _advert.AdvertPropertyValues)
                    {
                        var property = advert.AdvertPropertyValues.FirstOrDefault(x => x.AdvertPropertyId == prop.AdvertPropertyId);
                        property.PropertiesValue = prop.PropertiesValue;
                    }
                    Context.SaveChanges();
                }
            }
        }
    }
}