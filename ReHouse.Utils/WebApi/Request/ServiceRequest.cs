using System;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Request
{
    public class ServiceRequest : BaseRequest
    {
        public Services Service { get; set; }
        public Int32 SelId { get; set; }
    }
}