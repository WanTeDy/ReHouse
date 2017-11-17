using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Helpers;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.AdminFeedbacks
{
    public class AddAdminFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private HttpPostedFileBase _image { get; set; }
        public AdminFeedback _feedback { get; set; }

        public AddAdminFeedbackOperation(AdminFeedback feedback, HttpPostedFileBase image, string tokenHash)
        {
            _feedback = feedback;
            _image = image;
            _tokenHash = tokenHash;
            RussianName = "Добавление отзыва";
        }

        protected override void InTransaction()
        {
            _feedback.CreationDate = DateTime.Now;
            if (_image != null)
            {
                var random = new Random(DateTime.Now.Millisecond);
                var url = "~/Content/images/loaded/feedbacks/";

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
                    new Instructions("maxwidth=1200&maxheight=1200&format=jpg&quality=80"),
                    false,
                    true));


                var image = new Image
                {
                    FileName = filename + ".jpg",
                    Url = url,
                };

                Context.Images.Add(image);
                _feedback.Image = image;
            }
            Context.AdminFeedbacks.Add(_feedback);
            Context.SaveChanges();
        }
    }
}