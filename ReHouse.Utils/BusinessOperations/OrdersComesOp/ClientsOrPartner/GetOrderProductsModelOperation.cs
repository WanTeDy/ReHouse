using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class GetOrderProductsModelOperation: BaseOperation
    {
        private String TokenHash { get; set; }

        public List<ProductModel> OrderItems { get; set; }
        public Int32 QuantityProducts { get; set; }
        public Decimal AmountUah { get; set; }
        public Decimal AmountUsd { get; set; }
        public List<OrderCities> OrderCitieses { get; set; }
        public GetOrderProductsModelOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Просмотр корзины на сайте (клиент, партнер)";
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
                return;
            var items = order.OrdersItems.Where(y => !y.Deleted).Select(x => new ProductModel
            {
                ProductId = x.productID,
                SoldPrice = x.SoldPrice,
                SoldPriceUah = x.SoldPriceUah,
                Quantity = x.quantity,
                ProductName = x.ProductName,
            }).ToList();
            OrderItems = items;
            foreach (var productModel in OrderItems)
            {
                var prod = Context.StockProducts.FirstOrDefault(x => x.Id == productModel.ProductId);
                if (prod != null)
                {
                    productModel.Image = prod.MainImage;
                }
            }
            if (OrderItems != null && OrderItems.Any())
            {
                QuantityProducts = OrderItems.Sum(x => x.Quantity);
                if (contr.Role.Name == ConstV.RolePartner)
                {
                    AmountUah = OrderItems.Sum(x => x.Quantity*x.SoldPriceUah);
                    AmountUsd = OrderItems.Sum(x => x.Quantity*x.SoldPrice);
                }
            }
            else
            {
                QuantityProducts = 0;
                AmountUah = 0;
                AmountUsd = 0;
            }
            var ord = Context.OrderCities.ToList();
            OrderCitieses = new List<OrderCities>();
            foreach (var orderCitiese in ord)
            {
                OrderCitieses.Add(new OrderCities{Id = orderCitiese.Id, Name = orderCitiese.Name});
            }
        }
    }
}