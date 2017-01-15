using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Response
{
    public class EmployeerResponse : BaseResponse
    {
        public List<EmployeerModel> Employeers { get; set; }
        public Contractor Employeer { get; set; }
    }
}