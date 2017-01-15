using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockRoom
{
    public class GetOurStockRoomOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 SelectedId { get; set; }
        public DataBase.OurStocks.OurStockRoom OurStockRoom { get; set; }

        public GetOurStockRoomOperation(string tokenHash, int selectedId)
        {
            TokenHash = tokenHash;
            SelectedId = selectedId;
            RussianName = "Загрузка данных конкретного своего склада";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var stock = Context.OurStockRooms.FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if (stock == null)
                throw new ObjectNotFoundException("Склад не найден");
            OurStockRoom = new DataBase.OurStocks.OurStockRoom
            {
                Id = stock.Id,
                Name = stock.Name,
                NumberOfStock = stock.NumberOfStock,
                Adress = stock.Adress,
            };
        }
    }
}