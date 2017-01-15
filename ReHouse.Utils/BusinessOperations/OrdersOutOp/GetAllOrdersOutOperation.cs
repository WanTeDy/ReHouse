using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp
{
    public class GetAllOrdersOutOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public List<OrderOut> OrdersOut { get; set; }

        public GetAllOrdersOutOperation(string tokenHash)
        {
            TokenHash = tokenHash;
        }

        protected override void InTransaction()
        {
            var orders = Context.OrderOut.Where(x => !x.Deleted).ToList();
            OrdersOut = orders.Select(x => new OrderOut
            {
                Id = x.Id,
                DeliveryDate = x.DeliveryDate,
                Amount = x.Amount,
                Currency = x.Currency,
                PointOfDelivery = x.PointOfDelivery,
                ContractorId = x.ContractorId,
                OrderType = x.OrderType,
                ShipingDate = x.ShipingDate,
                Comment = x.Comment,
                AdressData = x.AdressData,
                AdressId = x.AdressId,
                OrderId = x.OrderId,
                OrderedId = x.OrderedId,
                Quantity = x.Quantity,
                TargetData = x.TargetData,
                TargetId = x.TargetId,
                OrderTypeString = ConstV.OrderOutTypeToString[x.OrderType],
                OrdersItems = x.OrdersItems.Where(h => !h.Deleted).Select(y => new OrdersItemForBrain
                {
                    Id = y.Id,
                    articul = y.articul,
                    productID = y.productID,
                    price = y.price,
                    price_uah = y.price_uah,
                    product_code = y.product_code,
                    quantity = y.quantity,
                    comment = y.comment
                }).ToList(),
                Status = x.Status,
                InStock = x.InStock
            }).ToList();
            foreach (var orderOut in OrdersOut)
            {
                foreach (var ordersItemForBrain in orderOut.OrdersItems)
                {
                    var prod = Context.Products.FirstOrDefault(x => x.productID == ordersItemForBrain.productID);
                    if (prod != null)
                        ordersItemForBrain.ProductName = prod.name;
                }
            }
        }
    }
}