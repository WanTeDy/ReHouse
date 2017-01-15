using System;
using System.Collections.Generic;
using System.IO;
using ITfamily.Utils.BusinessOperations.UpdateData;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Except;
using Newtonsoft.Json;

namespace ITfamily.Utils.BusinessOperations.ChangeCarousel
{
    public class AddCarouselOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private String NameFile { get; set; }
        private String FirstString { get; set; }
        private String SecondGreenString { get; set; }
        private String ThirdString { get; set; }
        private byte[] Bytes { get; set; }
        public String UrlHref { get; set; }
        public List<CarouselModel> CarouselModels { get; set; }
        public AddCarouselOperation(string tokenHash, string nameFile, string firstString, string secondGreenString, string thirdString, string urlHref, byte[] bytes)
        {
            TokenHash = tokenHash;
            NameFile = nameFile;
            FirstString = firstString;
            SecondGreenString = secondGreenString;
            ThirdString = thirdString;
            Bytes = bytes;
            UrlHref = urlHref;
            RussianName = "Добавление элемента карусели";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            
            var fileName = ConstV.PathCarousel + "\\" + NameFile;
            if (Bytes != null && Bytes.Length > 0)
            {
                CommonAccess.CreateDitectoryIfNotExist();
                CommonAccess.ByteArrayToFile(fileName, Bytes);
                var carousel = new DinamicData
                {
                    UrlPicture = ConstV.UrlCarousel + NameFile,
                    FirstString = FirstString,
                    SecondString = SecondGreenString,
                    ThirdString = ThirdString,
                    UrlHref = UrlHref,
                    TypeData = TypeData.Carousel,
                };
                Context.DinamicDatas.Add(carousel);
                Context.SaveChanges();
            }
            CarouselModels = CommonAccess.GetCarouselModels(Context);
        }
    }
}