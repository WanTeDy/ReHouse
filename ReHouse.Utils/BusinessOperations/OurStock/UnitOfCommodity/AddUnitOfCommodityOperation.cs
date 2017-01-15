using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.UnitOfCommodity
{
    public class AddUnitOfCommodityOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public DataBase.OurStocks.UnitOfCommodity UnitOfCommodity { get; set; }
        public Int32 FromOrderOutId { get; set; }
        public AddUnitOfCommodityOperation(string tokenHash, DataBase.OurStocks.UnitOfCommodity unitOfCommodity, int fromOrderOutId)
        {
            TokenHash = tokenHash;
            UnitOfCommodity = unitOfCommodity;
            FromOrderOutId = fromOrderOutId;
            RussianName = "Добавление уникальной единици товара на склад";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            if(UnitOfCommodity == null)
                throw new ActionNotAllowedException("Передан пустой обьект, попытайтесь еще раз");
            if(UnitOfCommodity.StockProductId == 0)
                throw new ActionNotAllowedException("Не указан StockProductId - " + UnitOfCommodity.StockProductId);

            if (!String.IsNullOrEmpty(UnitOfCommodity.SerialNumber))
            {
                var unit = Context.UnitOfCommodities.FirstOrDefault(x => x.SerialNumber == UnitOfCommodity.SerialNumber);
                if (unit != null)
                    throw new ActionNotAllowedException("Такой серийный номер уже существует. SerialNumber - " + UnitOfCommodity.SerialNumber);
            }
            
            UnitOfCommodity.ArrivalDate = DateTime.Now;
            if(UnitOfCommodity.PurchasePriceUAH == 0 || UnitOfCommodity.PurchasePriceUSD == 0)
                throw new ActionNotAllowedException("Укажите пожалуйста закупочную цену грн. и $");

            if (//!String.IsNullOrEmpty(UnitOfCommodity.SerialNumber) &&
                UnitOfCommodity.ProductStatusInStock == ProductStatusInStock.InStock)
            {
                var stockProd = Context.StockProducts.FirstOrDefault(x => x.Id == UnitOfCommodity.StockProductId);
                if(stockProd == null)
                    throw new ObjectNotFoundException("Обьект данного товара не найден. UnitOfCommodity.StockProductId = " + UnitOfCommodity.StockProductId);
                stockProd.IsAvailable = true;
            }

            Context.UnitOfCommodities.Add(UnitOfCommodity);
            Context.SaveChanges();
            if (FromOrderOutId > 0)
            {
                var orderItem = Context.OrdersItemForBrain.FirstOrDefault(x => !x.Deleted && x.Id == FromOrderOutId);
                if (orderItem == null)
                    throw new ObjectNotFoundException("Обьект товара в отчете заказа не найден. Id = " + FromOrderOutId);
                var unit =
                    Context.UnitOfCommodities.FirstOrDefault(
                        x =>
                            x.ProductStatusInStock == UnitOfCommodity.ProductStatusInStock &&
                            x.SerialNumber == UnitOfCommodity.SerialNumber &&
                            x.StockProductId == UnitOfCommodity.StockProductId &&
                            x.PurchasePriceUSD == UnitOfCommodity.PurchasePriceUSD);
                if(unit == null)
                    throw new ObjectNotFoundException("Добавляемая единица товара с серийным номером не найдена.");
                orderItem.UnitOfCommodityId = unit.Id;
                Context.SaveChanges();
            }
            
        }
    }
}