using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Request
{
    public class UpdateRequest : BaseRequest
    {
        public byte[] Bytes { get; set; }
        public String NameFile { get; set; }
        public String UrlPath { get; set; }
        public byte[] HashMD5 { get; set; }
        public List<UpdateFile> UpdateFiles { get; set; }
    }
}