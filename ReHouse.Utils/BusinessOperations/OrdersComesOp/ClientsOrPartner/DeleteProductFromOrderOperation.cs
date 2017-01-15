using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class DeleteProductFromOrderOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 ProductId { get; set; }
        public Int32 QuantityProducts { get; set; }
        public Decimal AmountUah { get; set; }
        public Decimal AmountUsd { get; set; }

        public DeleteProductFromOrderOperation(string tokenHash, int productId)
        {
            TokenHash = tokenHash;
            ProductId = productId;
            RussianName = "Удаление товара с корзины (клиент, партнер)";
        }

        protected override void InTransaction()
        {
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");

            OrderComes order = null;
            order = Context.OrderComes.Include("OrdersItems").FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);

            if (order == null)
                throw new ObjectNotFoundException("Заказ не найден");

            if (order.OrdersItems == null || order.OrdersItems.Count == 0)
                throw new ObjectNotFoundException("Отсуствуют товары в заказе");

            var orderItem = order.OrdersItems.FirstOrDefault(x => x.productID == ProductId && !x.Deleted);
            if (orderItem == null)
                throw new ObjectNotFoundException("Отсуствует выбраный товар");
            orderItem.Deleted = true;
            var justOneItem = order.OrdersItems.FirstOrDefault(x => !x.Deleted);
            if (justOneItem == null)
                order.Deleted = true;

            Context.SaveChanges();
            order = Context.OrderComes.Include("OrdersItems").FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);
            if (order == null || order.OrdersItems == null || order.OrdersItems.Count(x=>!x.Deleted) == 0)
            {
                QuantityProducts = 0;
                AmountUah = 0;
                AmountUsd = 0;
            }
            else
            {
                QuantityProducts = order.OrdersItems.Where(x => !x.Deleted).Sum(x => x.quantity);
                AmountUah = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPriceUah*y.quantity);
                AmountUsd = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPrice * y.quantity);
            }
        }
    }
}