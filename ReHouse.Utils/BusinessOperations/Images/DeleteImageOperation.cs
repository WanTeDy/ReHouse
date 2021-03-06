﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.News;
using System.Web;
using System.IO;

namespace ReHouse.Utils.BusinessOperations.Images
{
    public class DeleteImageOperation : BaseOperation
    {
        private int _id { get; set; }
        public Image _image { get; set; }

        public DeleteImageOperation(int id)
        {
            _id = id;
            RussianName = "Удаление фотографии";
        }

        protected override void InTransaction()
        {
            _image = Context.Images.FirstOrDefault(x => x.Id == _id && !x.Deleted);
            if (_image == null)
                Errors.Add("Id", "Данной фотографии не найдено");
            else
            {
                var path = HttpContext.Current.Server.MapPath(_image.Url);
                FileInfo fileInf = new FileInfo(path + _image.FileName);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }
                Context.Images.Remove(_image);
            }
            Context.SaveChanges();
        }
    }
}