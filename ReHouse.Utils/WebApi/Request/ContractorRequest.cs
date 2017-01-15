using System;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Request
{
    public class ContractorRequest : BaseRequest
    {
        public Contractor Client { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Int32 SelId { get; set; }
    }
}