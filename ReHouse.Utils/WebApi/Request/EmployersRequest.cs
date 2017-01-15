using System;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Request
{
    public class EmployersRequest : BaseRequest
    {
        public Contractor Employeer { get; set; }
        public Int32 SelId { get; set; }

        public String Login { get; set; }
        public String Password { get; set; }
        public String ProviderKey { get; set; }
        public String GoogleEmail { get; set; }
        public Provider Provider { get; set; }
    }
}