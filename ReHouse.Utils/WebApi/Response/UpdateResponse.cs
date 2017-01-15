using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Response
{
    public class UpdateResponse : BaseResponse
    {
        public Boolean NeedToUpload { get; set; }
        public List<UpdateFile> UpdateFiles { get; set; }
    }
}