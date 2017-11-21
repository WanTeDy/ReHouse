using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Collections.Generic;
using System.Drawing.Imaging;

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
            if (_image != null)
            {
                var random = new Random(DateTime.Now.Millisecond);
                _partner = new Partner
                {
                    CreationDate = DateTime.Now,
                };

                var url = "~/Content/images/partners/";

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
                var img = System.Drawing.Image.FromStream(_image.InputStream);

                img.Save(path + filename + ".png", ImageFormat.Png);
                //ImageBuilder.Current.Build(
                //        new ImageJob(_image.InputStream,
                //        path + filename,
                //        new Instructions("maxwidth=1500&maxheight=1500&format=png"),
                //        false,
                //        true));

                var image = new Image
                {
                    FileName = filename + ".png",
                    Url = url,
                };
                Context.Images.Add(image);
                _partner.Image = image;
                Context.Partners.Add(_partner);
                Context.SaveChanges();
            }
        }
    }
}