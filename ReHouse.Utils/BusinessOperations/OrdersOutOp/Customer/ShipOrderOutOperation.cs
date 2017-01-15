using System;
using System.Linq;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp.Customer
{
    public class ShipOrderOutOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 OrderId { get; set; }
        private Int32 TargetId { get; set; }
        private DateTime ShipingDate { get; set; }
        /// <summary>
        /// необходимость бухучета (1-да, 0-нет)
        /// </summary>
        private Int32 Accounting { get; set; }

        public ShipOrderOutOperation(string tokenHash, int orderId, int targetId, DateTime shipingDate, int accounting = -1)
        {
            TokenHash = tokenHash;
            OrderId = orderId;
            TargetId = targetId;
            ShipingDate = shipingDate;
            Accounting = accounting;
            RussianName = "Отгрузка заказа у поставщика";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var order = Context.OrderOut.FirstOrDefault(x => x.OrderId == OrderId);
            if (order != null)
            {
                var role =
                Context.RoleSet.FirstOrDefault(
                    x =>
                        !x.Deleted && !String.IsNullOrEmpty(x.ProviderLogin1) &&
                        !String.IsNullOrEmpty(x.ProviderMd5Password1));
                if (role != null)
                {
                    var sid = CommonAccess.GetSidToken(role.ProviderLogin1, role.ProviderMd5Password1);
                    if (!String.IsNullOrEmpty(sid))
                    {
                        if (Accounting == -1)
                            Accounting = 0;
                        var res = OrdersFacade.ShipOrder(OrderId, sid, TargetId, ShipingDate, Accounting).Result;
                        if(res.status!=1)
                            throw new OrderException("Ошибка на сервере поставщика № = " + res.error_code);
                        order.OrderType = OrderOutType.ordered;
                        order.ShipingDate = ShipingDate;
                        Context.SaveChanges();
                    }
                }
            }
        }
    }
}