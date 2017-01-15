using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Request
{
    public class AuthorityRequest : BaseRequest
    {
        public Int32 RoleId { get; set; }
        public List<AuthorityForOneRoleModel> AuthorityForOneRoleModels { get; set; }
        public List<Role> Roles { get; set; }
    }
}