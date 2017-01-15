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
    public class MoveGoodsToOurStockRoomOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private List<OrderItem> OrderItems { get; set; }
        private Int32 OurStockRoomId { get; set; }
        private Int32 exOrderComesId { get; set; }
        private Int32 OrderOutId { get; set; }
        public MoveGoodsToOurStockRoomOperation(string tokenHash, List<OrderItem> orderItems, int stockRoomId, int orderComesId, int orderOutId)
        {
            TokenHash = tokenHash;
            OrderItems = orderItems;
            OurStockRoomId = stockRoomId;
            exOrderComesId = orderComesId;
            OrderOutId = orderOutId;
            RussianName = "Перемещение товаров приходящего заказа на свой склад";
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
            if (exOrderComesId == 0)
                throw new ItFamilyException("OrderComeId = 0, пока недоступно");
            var stock = Context.OurStockRooms.FirstOrDefault(x => x.Id == OurStockRoomId && !x.Deleted);
            if (stock == null)
                throw new ObjectNotFoundException("Склад не найден");
            var orderOut = Context.OrderOut.FirstOrDefault(x => x.Id == OrderOutId && !x.Deleted);
            if(orderOut == null)
                throw new ObjectNotFoundException("Заказ не найден");
            //Проверка если есть товар на складе, то до необ. кол - ва добавить + quantity
            //TODO MoveGoodsToOurStockRoom

            var existProducts = OrderItems.Select(orderItem => Context.UnitOfCommodities
                .FirstOrDefault(x => x.StockProduct.ProductId == orderItem.productID && x.OurStockRoomId == OurStockRoomId && !x.Deleted))
                .Where(exProd => exProd != null).ToList();

            var newProducts = OrderItems.Where(orderItem => existProducts.FirstOrDefault(x => orderItem.productID == x.StockProduct.ProductId) == null)
                .Select(orderItem => new UnitOfCommodity()
                {
                    StockProductId = Context.StockProducts.FirstOrDefault(x => x.ProductId == orderItem.productID).Id,
                    Quantity = orderItem.quantity,
                    ReservedQuantity = orderItem.quantity,
                    OurStockRoomId = OurStockRoomId,
                    ArrivalDate = DateTime.Now,
                    PurchasePriceUAH = orderItem.PurchasePrice,
                    SalePriceUAH = orderItem.SoldPriceUah,
                    SalePriceUSD = orderItem.SoldPrice
                })
                .ToList();

            var productsId = OrderItems.Where(y => y.productID != 0).Select(x => x.productID).ToList();
            //var fullInfo = CommonAccess.GetProductsFromBrain(role.ProviderLogin1, role.ProviderMd5Password1, productsId);
            if (existProducts.Count > 0)
            {
                foreach (var stockProduct in existProducts)
                {
                    var d = OrderItems.FirstOrDefault(x => x.productID == stockProduct.StockProduct.ProductId);
                    //var inf = fullInfo.FirstOrDefault(x => x.productID == stockProduct.StockProductId);
                    if (d == null) continue;
                    stockProduct.Quantity += d.quantity;
                    var reservedUnit = Context.ReservedUnits.FirstOrDefault(x => x.OrderComesId == exOrderComesId
                        && x.UnitOfCommodity.Id == stockProduct.Id && !x.Deleted);
                    if (reservedUnit == null)
                    {
                        stockProduct.ReservedQuantity += d.quantity;
                        stockProduct.ReservedUnits.Add(new ReservedUnit()
                        {
                            OrderComesId = exOrderComesId,
                            UnitOfCommodityId = stockProduct.Id,
                            Quantity = d.quantity,
                            NeededQuantity = d.quantity,
                        });
                    }
                    else
                    {
                        stockProduct.ReservedQuantity += d.quantity - reservedUnit.Quantity;
                        reservedUnit.Quantity = d.quantity;
                    }
                    
                    //if (inf == null) continue;
                    //TODO stockProduct.PriceProviderUah = inf.price_uah;
                    //stockProduct.PriceProviderUsd = inf.price;
                }
                //var lastProd = (from orderItem in OrderItems let exist = existProducts.FirstOrDefault(x => x.ProductId == orderItem.productID) where exist == null select orderItem).ToList();
                //var lastProd =
                //    OrderItems.Where(y => existProducts.Select(x => x.ProductId).Any(u => u != y.productID)).ToList();
                //OrderItems = lastProd;
            }
            if(newProducts.Count > 0)
            {
                newProducts.ForEach(x => x.ReservedUnits.Add(new ReservedUnit()
                {
                    OrderComesId = exOrderComesId,
                    UnitOfCommodityId = x.Id,
                    Quantity = x.Quantity,
                    NeededQuantity = x.Quantity,
                }));
                Context.UnitOfCommodities.AddRange(newProducts);  
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
            orderOut.InStock = true;
            //Context.ReservedUnits.AddRange(productsForReservation);
            Context.SaveChanges();
        }

        private void AddCategoryAndVendorFromStockRoom(StockProduct stockProduct, DataBase.OurStocks.StockProduct stProd)
        {
            //var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == stockProduct.categoryID);
            //if (cat == null)
            //{
            //    var brCat = Context.Categories.FirstOrDefault(x => x.categoryID == stockProduct.categoryID);
            //    if (brCat != null)
            //    {
            //        var newCategory = new ItFamilyCategory
            //        {
            //            Name = brCat.name,
            //            CategoryId = brCat.categoryID,
            //            ParentId = brCat.parentID
            //        };
            //        stProd.ItFamilyCategory = newCategory;
            //    }
            //}
            //else
            //    stProd.ItFamilyCategoryId = cat.Id;
            //
            //var vend = Context.ItFamilyVendors.FirstOrDefault(x => x.VendorId == stockProduct.Vendor.vendorID);
            //if (vend == null)
            //{
            //    var brVend =
            //        Context.Vendors.Include("BrainCategories").FirstOrDefault(x => x.vendorID == stockProduct.Vendor.vendorID);
            //    if (brVend != null)
            //    {
            //        var categor =
            //            brVend.BrainCategories.Select(
            //                brainCategory =>
            //                    Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory.categoryID))
            //                .ToList();
            //        var newVendor = new ItFamilyVendor
            //        {
            //            Name = brVend.name,
            //            CategoryId = brVend.categoryID,
            //            VendorId = brVend.vendorID,
            //            ItFamilyCategories = categor
            //        };
            //        stProd.ItFamilyVendor = newVendor;
            //    }
            //}
            //else
            //    stProd.ItFamilyVendorId = vend.Id;
        }
    }
}