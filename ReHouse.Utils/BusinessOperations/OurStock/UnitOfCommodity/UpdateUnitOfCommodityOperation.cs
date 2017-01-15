using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.UnitOfCommodity
{
    public class UpdateUnitOfCommodityOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public DataBase.OurStocks.UnitOfCommodity UnitOfCommodity { get; set; }

        public UpdateUnitOfCommodityOperation(string tokenHash, DataBase.OurStocks.UnitOfCommodity unitOfCommodity)
        {
            TokenHash = tokenHash;
            UnitOfCommodity = unitOfCommodity;
            RussianName = "Изменение данных уникальной ед. товара";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            if (UnitOfCommodity == null)
                throw new ActionNotAllowedException("Передан пустой обьект, попытайтесь еще раз");
            if (UnitOfCommodity.StockProductId == 0)
                throw new ActionNotAllowedException("Не указан StockProductId - " + UnitOfCommodity.StockProductId);
            if (UnitOfCommodity.Id == 0)
                throw new ActionNotAllowedException("Не указан Id этой единицы товара - " + UnitOfCommodity.Id);

            if (!String.IsNullOrEmpty(UnitOfCommodity.SerialNumber))
            {
                var unit = Context.UnitOfCommodities.FirstOrDefault(x => x.SerialNumber == UnitOfCommodity.SerialNumber && x.Id != UnitOfCommodity.Id);
                if (unit != null)
                    throw new ActionNotAllowedException("Такой серийный номер уже существует. SerialNumber - " + UnitOfCommodity.SerialNumber);
            }

            if(!UnitOfCommodity.ArrivalDate.HasValue)
                UnitOfCommodity.ArrivalDate = DateTime.Now;
            if (UnitOfCommodity.PurchasePriceUAH == 0 || UnitOfCommodity.PurchasePriceUSD == 0)
                throw new ActionNotAllowedException("Укажите пожалуйста закупочную цену грн. и $");

            var unitUp = Context.UnitOfCommodities.FirstOrDefault(x => x.Id == UnitOfCommodity.Id);
            if(unitUp == null)
                throw new ActionNotAllowedException("Обьекта с таким Id не существует.");

            if (!String.IsNullOrEmpty(UnitOfCommodity.SerialNumber) &&
                UnitOfCommodity.ProductStatusInStock == ProductStatusInStock.InStock)
            {
                var stockProd = Context.StockProducts.FirstOrDefault(x => x.Id == UnitOfCommodity.StockProductId);
                if (stockProd == null)
                    throw new ObjectNotFoundException("Обьект данного товара не найден. UnitOfCommodity.StockProductId = " + UnitOfCommodity.StockProductId);
                stockProd.IsAvailable = true;
            }

            unitUp.ArrivalDate = UnitOfCommodity.ArrivalDate;
            unitUp.PurchasePriceUAH = UnitOfCommodity.PurchasePriceUAH;
            unitUp.PurchasePriceUSD = UnitOfCommodity.PurchasePriceUSD;
            unitUp.SerialNumber = UnitOfCommodity.SerialNumber;
            unitUp.StockProductId = UnitOfCommodity.StockProductId;
            unitUp.Notes = UnitOfCommodity.Notes;
            unitUp.ProductStatusInStock = UnitOfCommodity.ProductStatusInStock;
            unitUp.OurStockRoomId = UnitOfCommodity.OurStockRoomId;
            
            Context.SaveChanges();
        }
    }
}