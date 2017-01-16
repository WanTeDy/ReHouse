using System;
using System.Linq;

namespace ReHouse.Utils.BusinessOperations.Auth
{
    public class CheckTokenForAccessOperation : BaseOperation
    {
        public String _tokenHash { get; set; }
        public Boolean _access { get; set; }

        public CheckTokenForAccessOperation(string tokenHash)
        {
            _access = false;
            _tokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted && x.IsActive);
            if (user != null)
            {
                _access = true;                
            }
        }
    }
}