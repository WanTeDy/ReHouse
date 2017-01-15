using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Auth
{
    public class SetPasswordOperation : BaseOperation
    {
        public String Password { get; set; }
        public String TokenHash { get; set; }
        public String Email { get; set; }
        public SetPasswordOperation(string password, string tokenHash)
        {
            Password = password;
            TokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            var contr = Context.Contractors.FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            if (contr == null)
                throw new ObjectNotFoundException("Данный " + TokenHash + " не найден");
            contr.Password = Password;
            Email = contr.Email;
            Context.SaveChanges();
        }
    }
}