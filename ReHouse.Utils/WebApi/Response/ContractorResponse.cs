using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Response
{
    public class ContractorResponse : BaseResponse
    {
        public List<ClientModel> Clients { get; set; }
        public ClientModel ClientModel { get; set; }
        public Contractor Contractor { get; set; }
    }
}