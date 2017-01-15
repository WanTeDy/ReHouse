using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class GetHelerOrdersDataOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Decimal AmountUah { get; set; }
        public Decimal AmountUsd { get; set; }
        public Int32 QuantityProducts { get; set; }

        public GetHelerOrdersDataOperation(string tokenHash)
        {
            TokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            var contr = Context.Contractors.Include("Role").FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");
            OrderComes order = null;
            order = Context.OrderComes.FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);
            if (order == null || order.OrdersItems == null || order.OrdersItems.Count(x => !x.Deleted) == 0)
            {
                QuantityProducts = 0;
                AmountUah = 0;
                AmountUsd = 0;
            }
            else
            {
                QuantityProducts = order.OrdersItems.Where(x => !x.Deleted).Sum(x => x.quantity);
                AmountUah = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPriceUah * y.quantity);
                AmountUsd = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPrice * y.quantity);
            }
        }
    }
}