using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockRoom
{
    public class DeleteOurStockRoomOperation : BaseOperation
    {
        private Int32 SelectedId { get; set; }
        private String TokenHash { get; set; }

        public DeleteOurStockRoomOperation(int selectedId, string tokenHash)
        {
            SelectedId = selectedId;
            TokenHash = tokenHash;
            RussianName = "Удаление склада";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var stock = Context.OurStockRooms.FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if (stock == null)
                throw new ObjectNotFoundException("Склад не найден");
            if(stock.UnitOfCommodities.Count>0)
                throw new ExistsObjectException("Нельзя удалить склад. У выбраного склада, присутствуют товары!");
            stock.Deleted = true;
            Context.SaveChanges();
        }
    }
}