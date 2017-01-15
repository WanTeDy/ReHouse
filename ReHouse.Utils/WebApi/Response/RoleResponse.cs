using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response
{
    public class RoleResponse : BaseResponse
    {
        public List<RoleModel> RoleModels { get; set; }
    }
}