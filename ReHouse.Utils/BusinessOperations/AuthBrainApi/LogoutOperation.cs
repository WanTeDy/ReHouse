using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.AuthBrainApi
{
    public class LogoutOperation : BaseOperation
    {
        public String TokenHash { get; set; }

        public LogoutOperation(string tokenHash)
        {
            TokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            //var emp = Context.EmployeerSet.FirstOrDefault(x => x.TokenHash == TokenHash);
            //if(emp == null)
            //    throw new ObjectNotFoundException("Объкт не найден! Hash = " + TokenHash);
            //
            //emp.TokenHash = "";
            //Context.SaveChanges();
        }
    }
}