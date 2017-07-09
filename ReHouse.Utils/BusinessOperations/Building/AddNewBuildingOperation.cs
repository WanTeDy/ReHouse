using System;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Helpers;
using System.Collections.Generic;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.Security;
using System.Text;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class AddNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public NewBuilding _model { get; set; }
        private IEnumerable<String> _images { get; set; }
        private IEnumerable<String> _planImages { get; set; }
        public NewBuilding _newBuilding { get; set; }


        public AddNewBuildingOperation(string tokenHash, NewBuilding newBuilding, IEnumerable<String> images, IEnumerable<String> planImages)
        {
            _tokenHash = tokenHash;
            _model = newBuilding;
            _images = images;
            _planImages = planImages;
            RussianName = "Добавление новостроек";
        }

        protected override void InTransaction()
        {
            new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_model == null)
                return;

            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted);
            if(user == null)
            {
                Errors.Add("TokenHash", "Неверный Token");
            }
            else
            {
                if (_model.Price < 0)
                {
                    Errors.Add("Price", "Цена не может быть отрицательная");
                }
                else
                {
                    _newBuilding = new NewBuilding();
                    _newBuilding.Name = _model.Name;
                    _newBuilding.Adress = _model.Adress;
                    _newBuilding.Construct = _model.Construct;
                    _newBuilding.DistrictId = _model.DistrictId;
                    _newBuilding.ExpluatationDateId = _model.ExpluatationDateId;
                    _newBuilding.FloatQuantity = _model.FloatQuantity;
                    _newBuilding.FloorQuantity = _model.FloorQuantity;
                    _newBuilding.Heating = _model.Heating;
                    _newBuilding.HouseQuantity = _model.HouseQuantity;
                    _newBuilding.Parking = _model.Parking;
                    _newBuilding.Price = _model.Price;
                    _newBuilding.SectionQuantity = _model.SectionQuantity;
                    _newBuilding.Url = _model.Url;
                    _newBuilding.WallHeight = _model.WallHeight;
                    _newBuilding.WallMaterial = _model.WallMaterial;
                    _newBuilding.YouTubeUrl = _model.YouTubeUrl;
                    _newBuilding.PublicationDate = DateTime.Now;
                    _newBuilding.UserId = user.Id;
                    _newBuilding.Latitude = _model.Latitude;
                    _newBuilding.Longitude = _model.Longitude;
                    _newBuilding.IsHot = _model.IsHot;
                    _newBuilding.IsExclusive = _model.IsExclusive;
                    _newBuilding.IsModerated = _model.IsModerated;
                    _newBuilding.Description = _model.Description;

                    if (_images != null)
                    {
                        if (_newBuilding.Images == null)
                            _newBuilding.Images = new List<Image>();

                        foreach (var imageFile in _images)
                        {
                            if (imageFile != null)
                            {
                                var url = "~/Content/images/newBuildings/images/";

                                var path = HttpContext.Current.Server.MapPath(url);
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                byte[] data = System.Convert.FromBase64String(GenerateHash.FixBase64ForImage(imageFile));
                                MemoryStream ms = new MemoryStream(data);
                                //imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                //int point = imageFile.FileName.LastIndexOf('.');
                                var filename = HashHelper.GetMd5Hash("image_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                                ImageBuilder.Current.Build(
                                    new ImageJob(ms, //imageFile.InputStream,
                                    path + filename,
                                    new Instructions("maxwidth=1000&maxheight=1000&format=jpg&quality=70&watermark=water"),
                                    false,
                                    true));

                                var image = new Image
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };
                                Context.Images.Add(image);
                                _newBuilding.Images.Add(image);
                            }
                        }
                    }

                    if (_planImages != null)
                    {
                        if (_newBuilding.PlanImages == null)
                            _newBuilding.PlanImages = new List<PlanImage>();

                        foreach (var imageFile in _planImages)
                        {
                            if (imageFile != null)
                            {
                                var url = "~/Content/images/newBuildings/plans/";

                                var path = HttpContext.Current.Server.MapPath(url);
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                byte[] data = System.Convert.FromBase64String(GenerateHash.FixBase64ForImage(imageFile));
                                MemoryStream ms = new MemoryStream(data);
                                //imageFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                //int point = imageFile.FileName.LastIndexOf('.');
                                var filename = HashHelper.GetMd5Hash("image_" + DateTime.Now.Millisecond);//imageFile.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

                                ImageBuilder.Current.Build(
                                    new ImageJob(ms, //imageFile.InputStream,
                                    path + filename,
                                    new Instructions("maxwidth=1000&maxheight=1000&format=jpg&quality=70&watermark=water"),
                                    false,
                                    true));

                                var image = new PlanImage
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };
                                Context.PlanImages.Add(image);
                                _newBuilding.PlanImages.Add(image);
                            }
                        }
                    }

                    if (_model.Phones != null && _model.Phones.Count > 0)
                    {
                        _model.Phones = _model.Phones.Where(x => x.Id > 0 || !String.IsNullOrWhiteSpace(x.TelePhone)).ToList();
                        _model.Phones.ForEach(x => x.TelePhone = x.TelePhone != null ? x.TelePhone.Trim() : "");
                        List<Phone> newPhones = _model.Phones.Where(x => x.Id == 0).ToList();
                        List<Phone> oldPhones = _model.Phones.Where(x => x.Id > 0).ToList();
                        if (oldPhones.Count > 0)
                        {
                            foreach (var phone in oldPhones)
                            {
                                var exPhone = Context.Phones.FirstOrDefault(x => x.Id == phone.Id);
                                if (exPhone != null)
                                {
                                    if (String.IsNullOrWhiteSpace(phone.TelePhone))
                                        exPhone.Deleted = true;
                                    else
                                        exPhone.TelePhone = phone.TelePhone;
                                }
                            }
                        }
                        if (newPhones.Count > 0)
                        {
                            foreach (var phone in newPhones)
                            {
                                Context.Phones.Add(phone);
                            }
                            if (_newBuilding.Phones != null)
                                _newBuilding.Phones.AddRange(newPhones);
                            else
                                _newBuilding.Phones = newPhones;
                        }
                    }
                    _newBuilding.Builders = new List<Builder>();
                    if (_model.BuildersId != null && _model.BuildersId.Count > 0)
                    {
                        foreach (var id in _model.BuildersId)
                        {
                            _newBuilding.Builders.Add(Context.Builders.FirstOrDefault(x => x.Id == id));
                        }
                    }
                    Context.NewBuildings.Add(_newBuilding);
                    Context.SaveChanges();
                }
            }
        }
    }
}