﻿using System;
using System.Linq;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Except;
using System.Web;
using ImageResizer;
using System.IO;

namespace ReHouse.Utils.BusinessOperations.Users
{
    public class UpdateUserOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private HttpPostedFileBase _image { get; set; }
        public User _user { get; set; }

        public UpdateUserOperation(User user, HttpPostedFileBase image, string tokenHash)
        {
            _user = user;
            _image = image;
            _tokenHash = tokenHash;
            RussianName = "Изменение данных пользователя";
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
                    
                    var userForUpdating = Context.Users.FirstOrDefault(x => x.Id == _user.Id && !x.Deleted);
                    if (userForUpdating != null)
                    {
                        SetFields(userForUpdating);
                        if (Success)
                        {
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
                                    new Instructions("maxwidth=1600&maxheight=1600&format=jpg&quality=90"),
                                    false,
                                    true));


                                var avatar = new Avatar
                                {
                                    FileName = filename + ".jpg",
                                    Url = url,
                                };

                                var deleteImg = userForUpdating.Avatar;
                                if (deleteImg != null)
                                {
                                    FileInfo fileInf = new FileInfo(path + deleteImg.FileName);
                                    if (fileInf.Exists)                                    
                                        fileInf.Delete();
                                    
                                    Context.Avatars.Remove(deleteImg);
                                }
                                Context.Avatars.Add(avatar);
                                userForUpdating.Avatar = avatar;
                            }
                            Context.SaveChanges();
                        }
                    }
                    else
                        Errors.Add("Id", "Пользователя с ID=" + _user.Id + " не найден");
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

            if (!String.IsNullOrEmpty(_user.Email))
            {
                if (String.IsNullOrEmpty(user.Email) || user.Email.ToLower() != _user.Email.ToLower())
                {

                    var otherEmail = Context.Users.FirstOrDefault(x => x.Email == _user.Email);
                    if (otherEmail == null)
                        user.Email = _user.Email;
                    else
                        Errors.Add("Email", "Такой email уже существует.");
                }
            }
            if (!String.IsNullOrEmpty(_user.Login))
            {
                if (String.IsNullOrEmpty(user.Login) || user.Login.ToLower() != _user.Login.ToLower())
                {
                    var otherLogin = Context.Users.FirstOrDefault(x => x.Login == _user.Login);
                    if (otherLogin == null)
                        user.Login = _user.Login;
                    else
                        Errors.Add("Login", "Такой Login уже существует.");
                }
            }
            if (_user.Phones != null && _user.Phones.Count > 0)
            {
                _user.Phones = _user.Phones.Where(x => x.Id > 0 || !String.IsNullOrWhiteSpace(x.TelePhone)).ToList();
                _user.Phones.ForEach(x => x.TelePhone = x.TelePhone != null ? x.TelePhone.Trim() : "");
                List<Phone> newPhones = _user.Phones.Where(x => x.Id == 0).ToList();
                List<Phone> oldPhones = _user.Phones.Where(x => x.Id > 0).ToList();
                if (oldPhones.Count > 0)
                {
                    foreach (var phone in oldPhones)
                    {
                        var exPhone = Context.Phones.FirstOrDefault(x => x.Id == phone.Id);
                        if (exPhone != null)
                        {
                            if (String.IsNullOrWhiteSpace(phone.TelePhone))
                                Context.Phones.Remove(exPhone);
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
                    if (user.Phones != null)
                        user.Phones.AddRange(newPhones);
                    else
                        user.Phones = newPhones;
                }
            }
        }
    }
}