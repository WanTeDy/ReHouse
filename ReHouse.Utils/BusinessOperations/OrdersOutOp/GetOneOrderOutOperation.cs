using System;
using System.Linq;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp
{
    public class GetOneOrderOutOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 SelectedId { get; set; }
        public OrderOut OrderOut { get; set; }
        public GetOneOrderOutOperation(string tokenHash, int selectedId)
        {
            TokenHash = tokenHash;
            SelectedId = selectedId;
            RussianName = "Получение данных одного заказа от поставщика";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var orderOut = Context.OrderOut.Include("OrdersItems").FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if(orderOut != null)
            {
                var ord = new OrderOut
                {
                    Id = orderOut.Id,
                    AdressData = orderOut.AdressData,
                    AdressId = orderOut.AdressId,
                    Comment = orderOut.Comment + "\r\n\n Свои примечания: " + orderOut.Notes,
                    ContractorId = orderOut.ContractorId,
                    Amount = orderOut.Amount,
                    Currency = orderOut.Currency,
                    DeliveryDate = orderOut.DeliveryDate,
                    OrderId = orderOut.OrderId,
                    OrderType = orderOut.OrderType,
                    OrderTypeString = ConstV.OrderOutTypeToString[orderOut.OrderType],
                    OrderedId = orderOut.OrderedId,
                    PointOfDelivery = orderOut.PointOfDelivery,
                    TargetId = orderOut.TargetId,
                    Quantity = orderOut.Quantity,
                    ShipingDate = orderOut.ShipingDate,
                    Status = orderOut.Status,
                    InStock = orderOut.InStock,
                    TargetData = orderOut.TargetData,
                    OrdersItems = orderOut.OrdersItems.Where(w=>!w.Deleted).Select(x=>new OrdersItemForBrain
                    {
                        Id = x.Id,
                        productID = x.productID,
                        articul = x.articul,
                        comment = x.comment,
                        price = x.price,
                        price_uah = x.price_uah,
                        product_code = x.product_code,
                        quantity = x.quantity,
                        UnitOfCommodityId = x.UnitOfCommodityId,
                        IsUnitOfCommodity = x.UnitOfCommodityId.HasValue && x.UnitOfCommodityId.Value != 0 ? "Записан" : "Не записан",
                    }).ToList(),
                };
                OrderOut = ord;

                foreach (var ordersItemForBrain in OrderOut.OrdersItems)
                {
                    var prod = Context.Products.FirstOrDefault(x => x.productID == ordersItemForBrain.productID);
                    if (prod != null)
                        ordersItemForBrain.ProductName = prod.name;
                }
            }

        }
    }
}