using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Response
{
    public class ServiceResponse : BaseResponse
    {
        public List<Services> Serviceses { get; set; }
        public Services Services { get; set; }
    }
}