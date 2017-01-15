using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class GetOrderOperation : BaseOperation
    {
        private String TokenHash { get; set; }

        public OrderComes OrderComes { get; set; }

        public GetOrderOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Просмотр корзины (клиент, партнер)";
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
                return;//throw new ObjectNotFoundException("Заказ не найден");
            //OrderComes = Mapper.Map<OrderComes, OrderComes>(order);
            OrderComes = new OrderComes
            {
                Id = order.Id,
                ContractorId = order.ContractorId,
                DeliveryDate = order.DeliveryDate,
                PointOfDelivery = order.PointOfDelivery,
                OrderType = order.OrderType,
                ShipingDate = order.ShipingDate,
                Amount = order.Amount,
                Currency = order.Currency,
                Comment = order.Comment,
                PaymentStatus = order.PaymentStatus,
                IsExistsInOtherStock = order.IsExistsInOtherStock,
                OrdersItems = order.OrdersItems.Where(y=>!y.Deleted).Select(x=>new OrderItem
                {
                    Id = x.Id,
                    productID = x.productID,
                    SoldPrice = x.SoldPrice,
                    SoldPriceUah = x.SoldPriceUah,
                    articul = x.articul,
                    quantity = x.quantity,
                    product_code = x.product_code,
                    ProductName = x.ProductName
                }).ToList(),
                Adress = order.Adress,
            };
        }
    }
}