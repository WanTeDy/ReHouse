using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.UnitOfCommodity
{
    public class LoadOneUnitOfCommodityOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 UnitOfCommodityId { get; set; }
        public DataBase.OurStocks.UnitOfCommodity UnitOfCommodity { get; set; }
        public LoadOneUnitOfCommodityOperation(string tokenHash, int unitOfCommodityId)
        {
            TokenHash = tokenHash;
            UnitOfCommodityId = unitOfCommodityId;
            RussianName = "Загрузка одной уникальной ед. товара";
        }

        protected override void InTransaction()
        {
            var unit = Context.UnitOfCommodities.Include("ReservedUnits").FirstOrDefault(x => x.Id == UnitOfCommodityId && !x.Deleted);
            if(unit == null)
                throw new ObjectNotFoundException("Обьект UnitOfCommodity не найдет, Id = " + UnitOfCommodityId);
            UnitOfCommodity = new DataBase.OurStocks.UnitOfCommodity
            {
                Id = unit.Id,
                ArrivalDate = unit.ArrivalDate,
                DateOfSale = unit.DateOfSale,
                Notes = unit.Notes,
                OurStockRoomId = unit.OurStockRoomId,
                ProductStatusInStock = unit.ProductStatusInStock,
                PurchasePriceUAH = unit.PurchasePriceUAH,
                PurchasePriceUSD = unit.PurchasePriceUSD,
                SalePriceUAH = unit.SalePriceUAH,
                SalePriceUSD = unit.SalePriceUSD,
                SerialNumber = unit.SerialNumber,
                StockProductId = unit.StockProductId,
                ReservedQuantity = unit.ReservedQuantity,
                Quantity = unit.Quantity,
                ReservedUnits = unit.ReservedUnits.Where(x => !x.Deleted).Select(x => new DataBase.OurStocks.ReservedUnit
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    OrderComesId = x.OrderComesId,
                    UnitOfCommodityId = x.UnitOfCommodityId,
                    NeededQuantity = x.NeededQuantity,
                }).ToList(),
            };
        }
    }
}