using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class CarouselRequest : BaseRequest
    {
        public byte[] Bytes { get; set; }
        public Int32 IdCarousel { get; set; }
        public String UrlOrNameFile { get; set; }
        public Boolean IsUrl { get; set; }
        public String FirstString { get; set; }
        public String SecondGreenString { get; set; }
        public String ThirdString { get; set; }
        public String UrlHref { get; set; }
    }
}