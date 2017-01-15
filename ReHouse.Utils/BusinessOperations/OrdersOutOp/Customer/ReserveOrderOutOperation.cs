using System;
using System.Linq;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp.Customer
{
    public class ReserveOrderOutOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 OrderId { get; set; }
        private Int32 AdressId { get; set; }
        /// <summary>
        /// дата бронирования (по умолчанию - +1 сутки к текущей дате)
        /// </summary>
        private DateTime? ReservedDate { get; set; }

        public ReserveOrderOutOperation(string tokenHash, int orderId, int adressId, DateTime? reservedDate = null)
        {
            TokenHash = tokenHash;
            OrderId = orderId;
            AdressId = adressId;
            ReservedDate = reservedDate;
            RussianName = "Резервирование заказа у поставщика";
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
                        var res = OrdersFacade.ReserveOrder(OrderId, sid, role.ProviderLogin1, role.ProviderMd5Password1, ReservedDate).Result;
                        if (res.status != 1)
                            throw new OrderException("Ошибка на сервере поставщика № = " + res.error_code);
                        order.OrderType = OrderOutType.reserved;
                        Context.SaveChanges();
                    }
                }
            }
        }
    }
}