using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper.Helpers;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.Analyze
{
    public class AnalyzeGoodsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private DateTime From { get; set; }
        private DateTime To { get; set; }
        private Decimal FromPrice { get; set; }
        private Decimal ToPrice { get; set; }
        private Int32 CategoryId { get; set; }
        private FromWhatProvider FromWhat { get; set; }

        public List<AnalyzeGoodsFromOrderComesModel> AnalyzeDatas { get; set; }

        public AnalyzeGoodsOperation(string tokenHash, DateTime @from, DateTime to, decimal fromPrice, decimal toPrice, int categoryId, FromWhatProvider fromWhat)
        {
            TokenHash = tokenHash;
            From = @from;
            To = to;
            FromPrice = fromPrice;
            ToPrice = toPrice;
            CategoryId = categoryId;
            FromWhat = fromWhat;
            RussianName = "Анализ товаров";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var orders =
                Context.OrderComes.Include("OrdersItems").Where(
                    x =>
                        !x.Deleted && x.OrderType != OrderType.Draft && x.ShipingDate.HasValue &&
                        x.ShipingDate.Value >= From && x.ShipingDate.Value < To).ToList();

            var orderItems = new List<OrderItem>();

            var goods = new List<AnalyzeGoodsFromOrderComesModel>();

            foreach (var orderComese in orders)
            {
                var items = orderComese.OrdersItems.Where(
                    x => !x.Deleted && x.PurchasePrice >= FromPrice && x.PurchasePrice < ToPrice).ToList();

                foreach (var orderItem in items)
                {
                    var good = goods.FirstOrDefault(x => x.ProductId == orderItem.productID && orderItem.articul == x.Articul);
                    if (good != null)
                    {
                        var orderComesdata =
                            good.OrderComesData.FirstOrDefault(
                                x => x.FIO == orderComese.FIO && x.Phone == orderComese.Phone &&
                                     x.OrderType == orderComese.OrderType &&
                                     x.PaymentStatus == orderComese.PaymentStatus &&
                                     x.PurchasePrice == orderItem.PurchasePrice && x.SoldPrice == orderItem.SoldPrice);
                        if (orderComesdata != null)
                        {
                            orderComesdata.Quantity += orderItem.quantity;
                        }
                        else
                        {
                            good.OrderComesData.Add(new AdditionalDataForAnalyzeGoods
                            {
                                OrderType = orderComese.OrderType,
                                PurchasePrice = orderItem.PurchasePrice,
                                Phone = orderComese.Phone,
                                Quantity = orderItem.quantity,
                                FIO = orderComese.FIO,
                                PaymentStatus = orderComese.PaymentStatus,
                                SoldPrice = orderItem.SoldPrice,
                                OrderTypeString = ConstV.OrderTypes[orderComese.OrderType],
                                PaymentStatusString = ConstV.PaymentMethods[orderComese.PaymentMethod],
                            });
                        }
                    }
                    else
                    {
                        var product = new AnalyzeGoodsFromOrderComesModel
                        {
                            ProductId = orderItem.productID,
                            Articul = orderItem.articul,
                            OrderComesData = new List<AdditionalDataForAnalyzeGoods>
                            {
                                new AdditionalDataForAnalyzeGoods
                                {
                                    OrderType = orderComese.OrderType,
                                    PurchasePrice = orderItem.PurchasePrice,
                                    Phone = orderComese.Phone,
                                    Quantity = orderItem.quantity,
                                    FIO = orderComese.FIO,
                                    PaymentStatus = orderComese.PaymentStatus,
                                    SoldPrice = orderItem.SoldPrice,
                                    OrderTypeString = ConstV.OrderTypes[orderComese.OrderType],
                                    PaymentStatusString = ConstV.PaymentMethods[orderComese.PaymentMethod],
                                }
                            },
                            FromWhatProvider = "Свой товар"
                        };
                        goods.Add(product);
                    }
                }
            }

            var gateway = new DatabaseGateway();

            try
            {
                var articulAndProductId = goods.Select(x => new ArticulAndProductId
                {
                    Articul = x.Articul,
                    ProductId = x.ProductId
                }).ToList();
                var specif = articulAndProductId.AsTableValuedParameter("dbo.ArticulAndProductId", new[] { "ProductId", "Articul"});
                var stockProducts = gateway.GetStockProductFromListStockId(specif);

                foreach (var stockProduct in stockProducts)
                {
                    var good = goods.FirstOrDefault( x => x.ProductId == stockProduct.Id && x.Articul == stockProduct.Articul);
                    if (good != null)
                    {
                        if (FromWhat == FromWhatProvider.All || FromWhat == stockProduct.FromWhatProvider)
                        {
                            if (CategoryId == 0 || CategoryId == stockProduct.ItFamilyCategoryId)
                            {
                                good.FromWhat = stockProduct.FromWhatProvider;
                                if (stockProduct.FromWhatProvider == FromWhatProvider.Provider1)
                                {
                                    good.FromWhatProvider = "Поставщик 1";
                                }
                                else if (stockProduct.FromWhatProvider == FromWhatProvider.Provider2)
                                {
                                    good.FromWhatProvider = "Поставщик 2";
                                }
                                else if (stockProduct.FromWhatProvider == FromWhatProvider.OurProduct)
                                {
                                    good.FromWhatProvider = "Свой товар";
                                }
                                good.Warranty = stockProduct.Warranty;
                                good.CounterVisit = good.CounterVisit;
                                good.ProductName = stockProduct.Name;

                                var quantity = good.OrderComesData.Sum(x => x.Quantity);
                                good.Quantity = quantity;
                                good.AmountPurchasePrice = good.OrderComesData.Sum(x => x.Quantity*x.PurchasePrice);
                                good.AmountSoldPriceUsd = good.OrderComesData.Sum(x => x.Quantity*x.SoldPrice);
                            }
                            else
                            {
                                goods.Remove(good);
                            }
                        }
                        else
                        {
                            goods.Remove(good);
                        }
                    }
                }
            }
            finally
            {
                gateway.Dispose();
            }

            AnalyzeDatas = goods;
        }
    }
}