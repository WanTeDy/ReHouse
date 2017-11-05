using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.Except;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.Common;

namespace ReHouse.Utils.BusinessOperations.Slider
{
    public class UpdateSliderOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<SliderParam> _model { get; set; }


        public UpdateSliderOperation(string tokenHash, List<SliderParam> model)
        {
            _tokenHash = tokenHash;
            _model = model;
            RussianName = "Изменение слайдера";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
            if (user != null && (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager || user.Role.RussianName == ConstV.RoleSeo))
            {
                var random = new Random(DateTime.Now.Millisecond);
                foreach (var par in _model)
                {
                    var param = Context.SliderParams.FirstOrDefault(x => !x.Deleted && x.Id == par.Id);
                    if (param != null)
                    {
                        param.IsVideo = par.IsVideo;
                        if (par.IsVideo)
                        {
                            param.Url = par.Url;
                        }
                        else if (par.Image != null)
                        {
                            var url = "~/Content/images/slider/";
                            var path = HttpContext.Current.Server.MapPath(url);
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            par.Image.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                            int point = par.Image.FileName.LastIndexOf('.');
                            var filename = HashHelper.GetMd5Hash(par.Image.FileName.Substring(0, point) + random.Next(1000, 100000) + "_" + DateTime.Now.Millisecond);
                            ImageBuilder.Current.Build(
                                new ImageJob(par.Image.InputStream,
                                path + filename,
                                new Instructions("maxwidth=1500&maxheight=1500&format=jpg&quality=70&watermark=water"),
                                false,
                                true));
                            param.Url = $"{url}{filename}.jpg";
                        }
                    }
                    Context.SaveChanges();
                }
            }
        }
    }
}