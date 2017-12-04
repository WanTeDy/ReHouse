using System;
using System.Linq;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.Except;
using System.Web;
using ImageResizer;
using System.IO;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.AdminFeedbacks
{
    public class UpdateAdminFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private HttpPostedFileBase _image { get; set; }
        public AdminFeedback _feedback { get; set; }

        public UpdateAdminFeedbackOperation(AdminFeedback feedback, HttpPostedFileBase image, string tokenHash)
        {
            _feedback = feedback;
            _image = image;
            _tokenHash = tokenHash;
            RussianName = "Изменение отзыва";
        }

        protected override void InTransaction()
        {
            var feedbackForUpdating = Context.AdminFeedbacks.FirstOrDefault(x => x.Id == _feedback.Id && !x.Deleted);
            if (feedbackForUpdating != null)
            {
                feedbackForUpdating.Adress = _feedback.Adress;
                feedbackForUpdating.Username = _feedback.Username;
                feedbackForUpdating.Description = _feedback.Description;
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
                        new Instructions("maxwidth=1600&maxheight=1600&format=jpg&quality=90"),
                        false,
                        true));


                    var image = new Image
                    {
                        FileName = filename + ".jpg",
                        Url = url,
                    };

                    var deleteImg = feedbackForUpdating.Image;
                    if (deleteImg != null)
                    {
                        FileInfo fileInf = new FileInfo(path + deleteImg.FileName);
                        if (fileInf.Exists)
                            fileInf.Delete();

                        Context.Images.Remove(deleteImg);
                    }
                    Context.Images.Add(image);
                    feedbackForUpdating.Image = image;
                }
                Context.SaveChanges();
            }
            else
                Errors.Add("Id", "Отзыв с ID=" + _feedback.Id + " не найден");
        }
    }
}