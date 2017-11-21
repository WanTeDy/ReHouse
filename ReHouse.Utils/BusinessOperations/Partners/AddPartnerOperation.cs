using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Collections.Generic;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class AddPartnerOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private HttpPostedFileBase _image { get; set; }
        public Partner _partner { get; set; }

        public AddPartnerOperation(string tokenHash, HttpPostedFileBase image)
        {
            _tokenHash = tokenHash;
            _image = image;
            RussianName = "Добавление нового партнера";
        }

        protected override void InTransaction()
        {
            _partner = new Partner
            {
                CreationDate = DateTime.Now,
            };
            if (_image != null)
            {
                var url = "~/Content/images/partners/";

                var path = HttpContext.Current.Server.MapPath(url);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                _image.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                int point = _image.FileName.LastIndexOf('.');
                var filename = _image.FileName.Substring(0, point) + "_" + DateTime.Now.ToFileTime();

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
                Context.Images.Add(image);
                _partner.Image = image;
            }
            Context.Partners.Add(_partner);
            Context.SaveChanges();
        }
    }
}