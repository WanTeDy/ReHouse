using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Helpers;
using System.Web;
using System.IO;
using ImageResizer;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class AddUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private HttpPostedFileBase _image { get; set; }
        public User _user { get; set; }

        public AddUserOperation(User user, HttpPostedFileBase image, string tokenHash)
        {
            _user = user;
            _image = image;
            _tokenHash = tokenHash;
            RussianName = "Добавление пользователя";
        }

        protected override void InTransaction()
        {
            if (String.IsNullOrEmpty(_user.Email))
                Errors.Add("Email", "Укажите Email");
            else
            {
                _user.Email = _user.Email.Trim();

                var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted && x.IsActive);
                if (user == null)
                    Errors.Add("Id", "Неверный Token");
                else
                {
                    //if (user.Id != _user.Id)
                    //    new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

                    var newUser = new User();
                    SetFields(newUser);
                    if (Success)
                    {
                        var role = Context.Roles.FirstOrDefault(x => x.RussianName == ConstV.RoleRieltor);
                        newUser.RoleId = role.Id;
                        newUser.IsActive = false;

                        if (_image != null)
                        {
                            var random = new Random(DateTime.Now.Millisecond);
                            var url = "~/Content/images/loaded/avatars/";

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
                                new Instructions("maxwidth=1500&maxheight=1500&format=jpg&quality=80"),
                                false,
                                true));


                            var avatar = new Avatar
                            {
                                FileName = filename + ".jpg",
                                Url = url,
                            };
                                                        
                            Context.Avatars.Add(avatar);
                            newUser.Avatar = avatar;
                        }
                        Context.Users.Add(newUser);
                        Context.SaveChanges();
                    }
                }
            }
        }

        private void SetFields(User user)
        {
            user.Adress = _user.Adress;
            user.FirstName = _user.FirstName;
            user.SecondName = _user.SecondName;
            user.FatherName = _user.FatherName;
            user.MessengerName = _user.MessengerName;
            //user.About = _user.About;
            user.Position = _user.Position;
            user.OrderByField = _user.OrderByField;

            var otherEmail = Context.Users.FirstOrDefault(x => x.Email == _user.Email);
            if (otherEmail == null)
                user.Email = _user.Email;
            else
                Errors.Add("Email", "Такой email уже существует.");

            if (!String.IsNullOrEmpty(_user.Login))
            {
                var otherLogin = Context.Users.FirstOrDefault(x => x.Login == _user.Login);
                if (otherLogin == null)
                    user.Login = _user.Login;
                else
                    Errors.Add("Login", "Такой Login уже существует.");
            }
            if (_user.Phones != null && _user.Phones.Count > 0)
            {
                _user.Phones = _user.Phones.Where(x => !String.IsNullOrWhiteSpace(x.TelePhone)).ToList();
                _user.Phones.ForEach(x => x.TelePhone = x.TelePhone != null ? x.TelePhone.Trim() : "");
                
                if (_user.Phones.Count > 0)
                {
                    foreach (var phone in _user.Phones)
                    {
                        Context.Phones.Add(phone);
                    }
                    if (user.Phones != null)
                        user.Phones.AddRange(_user.Phones);
                    else
                        user.Phones = _user.Phones;
                }
            }
        }
    }
}