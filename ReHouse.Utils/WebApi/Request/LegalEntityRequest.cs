using System;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Request
{
    public class LegalEntityRequest : BaseRequest
    {
        public Contractor LegalEntity { get; set; }
        public Int32 SelId { get; set; }
    }
}