using System;
using System.Collections.Generic;

namespace ITfamily.Utils.WebApi.Response
{
    public class PicturesResponse : BaseResponse
    {
        public List<String> ImageUrls { get; set; }
    }
}