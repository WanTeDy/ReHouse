using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockRoom
{
    public class UpdateOurStockRoomOperation : BaseOperation
    {
        private String NameOfStock { get; set; }
        private String Adres { get; set; }
        private Int32 NumberOfStock { get; set; }
        private Int32 SelectedId { get; set; }
        private String TokenHash { get; set; }

        public UpdateOurStockRoomOperation(string nameOfStock, string adres, int numberOfStock, int selectedId, string tokenHash)
        {
            NameOfStock = nameOfStock;
            Adres = adres;
            NumberOfStock = numberOfStock;
            SelectedId = selectedId;
            TokenHash = tokenHash;
            RussianName = "Изменение данных своих складов";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var stock = Context.OurStockRooms.FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if (stock == null)
                throw new ObjectNotFoundException("Склад не найден");

            stock.Adress = Adres;
            stock.Name = NameOfStock;
            stock.NumberOfStock = NumberOfStock;
            Context.SaveChanges();
        }
    }
}