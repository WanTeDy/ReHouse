using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class AddManagerToOrderComesOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 SelectedId { get; set; }
        public AddManagerToOrderComesOperation(string tokenHash, int selectedId)
        {
            TokenHash = tokenHash;
            SelectedId = selectedId;
            RussianName = "Закрепление менеджера за приходящим заказом";
        }

        protected override void InTransaction()
        {
            var contractor = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var order = Context.OrderComes.FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if(order == null)
                throw new ObjectNotFoundException("Приходящий заказ Id: " + SelectedId + " не найден.");
            order.ManagerId = contractor.Id;
            Context.SaveChanges();
        }
    }
}