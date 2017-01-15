using System.Collections.Generic;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Response
{
    public class AuthorityResponse : BaseResponse
    {
        public List<Role> Roles { get; set; }
        public List<Authority> Authorities { get; set; }
    }
}