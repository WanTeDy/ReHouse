using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.ChangeCarousel
{
    public class UpdateCarouselOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 IdCarousel { get; set; }
        private String UrlOrNameFile { get; set; }
        private Boolean IsUrl { get; set; }
        private String FirstString { get; set; }
        private String SecondGreenString { get; set; }
        private String ThirdString { get; set; }
        private byte[] Bytes { get; set; }
        public String UrlHref { get; set; }
        public List<CarouselModel> CarouselModels { get; set; }
        public UpdateCarouselOperation(string tokenHash, int idCarousel, string urlOrNameFile, bool isUrl, string firstString, string secondGreenString, string thirdString, string urlHref, byte[] bytes)
        {
            TokenHash = tokenHash;
            IdCarousel = idCarousel;
            UrlOrNameFile = urlOrNameFile;
            IsUrl = isUrl;
            FirstString = firstString;
            SecondGreenString = secondGreenString;
            ThirdString = thirdString;
            Bytes = bytes;
            RussianName = "Изменение одного элемента карусели";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var carousel =
                Context.DinamicDatas.FirstOrDefault(
                    x => !x.Deleted && x.TypeData == TypeData.Carousel && x.Id == IdCarousel);
            if(carousel == null)
                throw new ObjectNotFoundException("Данный обьект карусули не найден. Id = " + IdCarousel);

            if (IsUrl)
            {
                carousel.FirstString = FirstString;
                carousel.SecondString = SecondGreenString;
                carousel.ThirdString = ThirdString;
                carousel.UrlHref = UrlHref;
                Context.SaveChanges();
                CarouselModels = CommonAccess.GetCarouselModels(Context);
                return;
            }

            var fileName = ConstV.PathCarousel + "\\" + UrlOrNameFile;
            if (Bytes != null && Bytes.Length > 0)
            {
                CommonAccess.CreateDitectoryIfNotExist();
                CommonAccess.ByteArrayToFile(fileName, Bytes);
                carousel.UrlPicture = ConstV.UrlCarousel + UrlOrNameFile;
                carousel.FirstString = FirstString;
                carousel.SecondString = SecondGreenString;
                carousel.ThirdString = ThirdString;
                carousel.UrlHref = UrlHref;
                Context.SaveChanges();
                CarouselModels = CommonAccess.GetCarouselModels(Context);
            }
        }
    }
}