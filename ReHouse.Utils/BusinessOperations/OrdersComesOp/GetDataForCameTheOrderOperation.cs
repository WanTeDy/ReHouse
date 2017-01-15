using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class GetDataForCameTheOrderOperation : BaseOperation
    {
        public OrderComes OrderComes { get; set; }
        private Int32 SelectedOrderComesId { get; set; }
        private String TokenHash { get; set; }
        public String FIO { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String ContractorRole { get; set; }

        public GetDataForCameTheOrderOperation(int selectedOrderComesId, string tokenHash)
        {
            SelectedOrderComesId = selectedOrderComesId;
            TokenHash = tokenHash;
            RussianName = "Получение данных конкретного приходящего заказа";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var orderComes = Context.OrderComes.FirstOrDefault(x => x.Id == SelectedOrderComesId && !x.Deleted);
            if(orderComes == null)
                throw new ObjectNotFoundException("Заказ не найден");

            if (orderComes.ContractorId != 0)
            {
                var client = Context.Contractors.Include("Role").FirstOrDefault(x => x.Id == orderComes.ContractorId);
                if(client != null)
                {
                    FIO = client.SecondName + " " + client.FirstName + " " + client.FatherName;
                    Phone = client.Phone;
                    Email = client.Email;
                    ContractorRole = " "+client.Role.Name+"а";
                }
            }
            OrderComes = new OrderComes
            {
                Id = orderComes.Id,
                ContractorId = orderComes.ContractorId,
                OrderType = orderComes.OrderType,
                Comment = orderComes.Comment + "\nСвои примечания: " + orderComes.Notes,
                PaymentStatus = orderComes.PaymentStatus,
                IsExistsInOtherStock = orderComes.IsExistsInOtherStock,
                Adress = orderComes.Adress,
                Currency = orderComes.Currency,
                Amount = orderComes.Amount,
                DeliveryDate = orderComes.DeliveryDate,
                PointOfDelivery = orderComes.PointOfDelivery,
                Quantity = orderComes.Quantity,
                ShipingDate = orderComes.ShipingDate,
                OrdersItems = orderComes.OrdersItems.Where(y=>!y.Deleted).Select(x=> new OrderItem
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    articul = x.articul,
                    SoldPrice = x.SoldPrice,
                    SoldPriceUah = x.SoldPriceUah,
                    PurchasePrice = x.PurchasePrice,
                    productID = x.productID,
                    product_code = x.product_code,
                    quantity = x.quantity
                }).ToList(),
            };
            foreach (var ordersItem in OrderComes.OrdersItems)
            {
                var prod = Context.StockProducts.Include("UnitOfCommodities").FirstOrDefault(x => x.Id == ordersItem.productID);
                if (prod != null)
                {
                    if (prod.FromWhatProvider == FromWhatProvider.OurProduct)
                    {
                        ordersItem.FromWhat = "Наш товар";
                        //записать что свой
                    }
                    else
                    {
                        ordersItem.FromWhat = "От 1 поставщика";
                        //записать что товар от поставщика
                        ordersItem.ProductIdForProvider = prod.ProductId;
                    }
                    //var tes = Context.UnitOfCommodities.Where(x => x.StockProductId == prod.Id).ToList();
                    var units = Context.UnitOfCommodities.Any(x => x.StockProductId == prod.Id && x.ProductStatusInStock == ProductStatusInStock.InStock);
                    if (units)
                    {
                        ordersItem.InStockUnitOfCommodity = "В наличии на нашем складе";
                        //ordersItem.StockQuantity = units.Sum(x => x.Quantity);
                        //записать что товар есть на нашем складе и указать склад название
                    }
                    else
                    {
                        //записать что товар отсутствует на нашем складе
                        ordersItem.InStockUnitOfCommodity = "Отсутствует на нашем складе";
                    }
                }
            }
        }
    }
}