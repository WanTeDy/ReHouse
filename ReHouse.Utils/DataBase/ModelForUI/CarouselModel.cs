using System;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class CarouselModel
    {
        public Int32 Id { get; set; }
        public String FirstString { get; set; }
        public String SecondString { get; set; }
        public String ThirdString { get; set; }
        public String UrlPicture { get; set; }
        public String UrlHref { get; set; }
        public Boolean ChangeUrl { get; set; }
        public byte[] Bytes { get; set; }
    }
}