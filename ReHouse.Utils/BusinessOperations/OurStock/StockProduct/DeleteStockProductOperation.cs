using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class DeleteStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 DeleteId { get; set; }

        public DeleteStockProductOperation(string tokenHash, int deleteId)
        {
            TokenHash = tokenHash;
            DeleteId = deleteId;
            RussianName = "Удаление товара со склада";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var stockProd = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.Id == DeleteId);
            if(stockProd == null)
                throw new ObjectNotFoundException("Товар не найден");

            var units = Context.UnitOfCommodities.Where(x => x.StockProductId == DeleteId).ToList();

            if (units.Any())
            {
                var unit =
                    units.FirstOrDefault(
                        x =>
                            x.ProductStatusInStock == ProductStatusInStock.PendingDelivery ||
                            x.ProductStatusInStock == ProductStatusInStock.InStock);
                if(unit != null)
                    throw new ActionNotAllowedException("Невозможно удалить товар. Пока экземпляры этого товара есть в наличии или находятся в ожидании доставки.");
            }
            stockProd.IsAvailable = false;
            stockProd.Deleted = true;
            Context.SaveChanges();
        }
    }
}