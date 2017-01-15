using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;


namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class ReserveGoodsInOurStockRoomOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 OrderId { get; set; }
        private List<OrderItem> OrderItems { get; set; }
        private Int32 OurStockRoomId { get; set; }
        public ReserveGoodsInOurStockRoomOperation(string tokenHash, List<OrderItem> orderItems, int stockRoomId, int orderId)
        {
            TokenHash = tokenHash;
            OrderItems = orderItems;
            OurStockRoomId = stockRoomId;
            OrderId = orderId;
            RussianName = "Резервирование товаров на нашем складе под определенный заказ";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var role =
                Context.RoleSet.FirstOrDefault(
                    x =>
                        !x.Deleted && !String.IsNullOrEmpty(x.ProviderLogin1) &&
                        !String.IsNullOrEmpty(x.ProviderMd5Password1));
            if (role == null) return;
            var stock = Context.OurStockRooms.FirstOrDefault(x => x.Id == OurStockRoomId && !x.Deleted);
            if (stock == null)
                throw new ObjectNotFoundException("Склад не найден");
            //Проверка если есть товар на складе, то до необ. кол - ва добавить + quantity
           
            var existProducts = OrderItems.Select(orderItem => Context.UnitOfCommodities
            .FirstOrDefault(x => x.StockProductId == orderItem.productID && x.OurStockRoomId == OurStockRoomId))
            .Where(exProd => exProd != null).ToList();

            var productsId = OrderItems.Where(y => y.productID != 0).Select(x => x.productID).ToList();
            var fullInfo = CommonAccess.GetProductsFromBrain(role.ProviderLogin1, role.ProviderMd5Password1, productsId);

            if (existProducts.Count > 0)
            {
                foreach (var stockProduct in existProducts)
                {
                    var d = OrderItems.FirstOrDefault(x => x.productID == stockProduct.StockProductId);
                    var inf = fullInfo.FirstOrDefault(x => x.productID == stockProduct.StockProductId);
                    if (d == null) continue;
                    stockProduct.Quantity += d.quantity;
                    if (inf == null) continue;
                    //TODO stockProduct.PriceProviderUah = inf.price_uah;
                    //stockProduct.PriceProviderUsd = inf.price;
                }
                //var lastProd = (from orderItem in OrderItems let exist = existProducts.FirstOrDefault(x => x.ProductId == orderItem.productID) where exist == null select orderItem).ToList();
                //var lastProd =
                //    OrderItems.Where(y => existProducts.Select(x => x.ProductId).Any(u => u != y.productID)).ToList();
                //OrderItems = lastProd;
            }

            /*var selProd = OrderItems.Select(orderItem => Context.Products.FirstOrDefault(x => x.productID == orderItem.productID)).Where(exProd => exProd != null).ToList();
            var stockProducts = new List<StockProduct>();
            foreach (var brainProduct in selProd)
            {
                var ord = OrderItems.FirstOrDefault(x => x.productID == brainProduct.productID);
                var inf = fullInfo.FirstOrDefault(x => x.productID == brainProduct.productID);
                var stProd = new StockProduct
                {
                    Articul = brainProduct.articul,
                    BriefDescription = brainProduct.brief_description,
                    Name = brainProduct.name,
                    ProductCode = brainProduct.product_code,
                    Volume = brainProduct.volume,
                    Warranty = brainProduct.warranty,
                    ProductId = brainProduct.productID,
                    ProductStatusInStock = ProductStatusInStock.NotProcessed,
                    Amount = 0,
                    NeedQuantity = ord != null ? ord.quantity : 0,
                    OurStockRoomId = OurStockRoomId
                };
                if (inf != null && inf.price != 0 && inf.price_uah != 0)
                {
                    if (inf.price != brainProduct.price)
                        brainProduct.price = inf.price;
                    if (inf.price_uah != brainProduct.price_uah)
                        brainProduct.price_uah = inf.price_uah;
                    //stProd.PriceProviderUah = brainProduct.price_uah; //TODO serial number
                    //stProd.PriceProviderUsd = brainProduct.price;
                }

                AddCategoryAndVendorFromStockRoom(brainProduct, stProd);
                stockProducts.Add(stProd);
            }*/

            //if (stockProducts.Count > 0)
            //    Context.StockProducts.AddRange(stockProducts);

            Context.SaveChanges();
        }
    }
}