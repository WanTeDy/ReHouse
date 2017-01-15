using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.OurStock.UnitOfCommodity
{
    public class LoadUnitOfCommoditiesOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<UnitOfCommodityModel> UnitOfCommodityModels { get; set; }
        public Int32 StockProductId { get; set; }

        public LoadUnitOfCommoditiesOperation(string tokenHash, int stockProductId)
        {
            TokenHash = tokenHash;
            StockProductId = stockProductId;
        }

        protected override void InTransaction()
        {
            var units = Context.UnitOfCommodities.Where(x => x.StockProductId == StockProductId).ToList();
            var stockRooms = Context.OurStockRooms.Where(x => !x.Deleted).ToList();
            var unitsModel = units.Select(x =>
            {
                var firstOrDefault = stockRooms.FirstOrDefault(y=>y.Id == x.OurStockRoomId);
                return firstOrDefault != null ? new UnitOfCommodityModel
                                                   {
                                                       StockProductId = x.StockProductId,
                                                       Id = x.Id,
                                                       SerialNumber = x.SerialNumber,
                                                       ArrivalDate = x.ArrivalDate,
                                                       OurStockRoomId = x.OurStockRoomId,
                                                       Notes = x.Notes,
                                                       PurchasePriceUAH = x.PurchasePriceUAH,
                                                       PurchasePriceUSD = x.PurchasePriceUSD,
                                                       DateOfSale = x.DateOfSale,
                                                       SalePriceUAH = x.SalePriceUAH,
                                                       SalePriceUSD = x.SalePriceUSD,
                                                       ProductStatusInStockName = ConstV.ProductStatusInStocks[x.ProductStatusInStock],
                                                       OurStockRoomName = firstOrDefault.Name,
                                                   } :
                                                   new UnitOfCommodityModel
                                                   {
                                                       StockProductId = x.StockProductId,
                                                       Id = x.Id,
                                                       SerialNumber = x.SerialNumber,
                                                       ArrivalDate = x.ArrivalDate,
                                                       OurStockRoomId = x.OurStockRoomId,
                                                       Notes = x.Notes,
                                                       PurchasePriceUAH = x.PurchasePriceUAH,
                                                       PurchasePriceUSD = x.PurchasePriceUSD,
                                                       DateOfSale = x.DateOfSale,
                                                       SalePriceUAH = x.SalePriceUAH,
                                                       SalePriceUSD = x.SalePriceUSD,
                                                       ProductStatusInStockName = ConstV.ProductStatusInStocks[x.ProductStatusInStock],
                                                       OurStockRoomName = "Не указан склад",
                                                   };
            }).ToList();
            UnitOfCommodityModels = unitsModel;
        }
    }
}