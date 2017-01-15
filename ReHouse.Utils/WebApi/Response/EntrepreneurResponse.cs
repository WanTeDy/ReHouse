using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Response
{
    public class EntrepreneurResponse : BaseResponse
    {
        public Contractor Entrepreneur { get; set; }
        public List<EntrepreneurModel> EntrepreneurModels { get; set; }
    }
}