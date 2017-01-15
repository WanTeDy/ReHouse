using System;
using System.Linq;
using ITfamily.Utils.Except;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.BusinessOperations.AuthBrainApi
{
    public class AuthOperation : BaseOperation
    {
        private String Login { get; set; }
        private String Password { get; set; }

        public String TokenHash { get; set; }
        public AuthOperation(string login, string password)
        {
            Login = login;
            Password = password;
        }

        protected override void InTransaction()
        {
            //var emp =
            //    Context.EmployeerSet.FirstOrDefault(
            //        x => x.Login == Login && x.Password == Password && !x.Deleted && x.IsActive);
            //
            //if(emp == null)
            //    throw new ObjectNotFoundException("Не найден обьект. Login = " + Login);
            //
            //emp.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + emp.Password + Guid.NewGuid() + emp.Login + Guid.NewGuid() + emp.Email);
            //TokenHash = emp.TokenHash;
            //Context.SaveChanges();
        }
    }
}