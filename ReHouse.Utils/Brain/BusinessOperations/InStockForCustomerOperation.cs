using System;
using System.Linq;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class InStockForCustomerOperation : BaseOperation
    {
        public Boolean InStock { get; set; }
        private Int32 ProductId { get; set; }
        public DateTime DateTimeArrival { get; set; }

        public InStockForCustomerOperation(int productId)
        {
            ProductId = productId;
        }

        protected override void InTransaction()
        {
            //var emp = Context.EmployeerSet.Include("Role").FirstOrDefault(x => x.Role.Name == ConstV.RoleAdministrator && !x.Deleted && !String.IsNullOrEmpty(x.ProviderLogin1) && !String.IsNullOrEmpty(x.ProviderPassword1));
            //if(emp == null) return;
            //var auth = Facade.AuthBrainFacade.Auth(emp.ProviderLogin1, emp.ProviderPassword1).Result;
            //if(auth == null || auth.status != 1) return;
            //var date = Facade.BrainCommonFacade.GetDeliveryTime(ProductId, 168, auth.result).Result;
            //if (date == null) return;
            //InStock = true;
            //DateTimeArrival = date.Value;
        }
    }
}