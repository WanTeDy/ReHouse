using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp
{
    public class ChangeStatusForOrderOutOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 SelId { get; set; }
        public OrderOutType OrderOutType { get; set; }
        public ChangeStatusForOrderOutOperation(string tokenHash, int selId, OrderOutType orderOutType)
        {
            TokenHash = tokenHash;
            SelId = selId;
            OrderOutType = orderOutType;
            RussianName = "Изменение состояния заказов поставщика";
        }
        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var order = Context.OrderOut.FirstOrDefault(x => x.Id == SelId && !x.Deleted);
            if (order == null)
                throw new ObjectNotFoundException("Обьект заказа не найден. Id = " + SelId);
            order.OrderType = OrderOutType;
            Context.SaveChanges();
        }
    }
}