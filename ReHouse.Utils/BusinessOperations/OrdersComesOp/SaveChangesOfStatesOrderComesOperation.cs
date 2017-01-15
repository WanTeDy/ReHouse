using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class SaveChangesOfStatesOrderComesOperation : BaseOperation
    {
        private OrderType OrderType { get; set; }
        private PaymentStatus PaymentStatus { get; set; }
        private Int32 SelectedOrderComesId { get; set; }
        private String TokenHash { get; set; }

        public SaveChangesOfStatesOrderComesOperation(OrderType orderType, PaymentStatus paymentStatus, int selectedOrderComesId, string tokenHash)
        {
            OrderType = orderType;
            PaymentStatus = paymentStatus;
            SelectedOrderComesId = selectedOrderComesId;
            TokenHash = tokenHash;
            RussianName = "Сохранение изменений конкретного приходящего заказа";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var orderComes = Context.OrderComes.FirstOrDefault(x => x.Id == SelectedOrderComesId && !x.Deleted);
            if (orderComes == null)
                throw new ObjectNotFoundException("Заказ не найден");

            orderComes.OrderType = OrderType;
            orderComes.PaymentStatus = PaymentStatus;

            Context.SaveChanges();
        }
    }
}