using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockRoom
{
    public class AddOurStockRoomOperation : BaseOperation
    {
        private String NameOfStock { get; set; }
        private String Adress { get; set; }
        private Int32 NumberOfStock { get; set; }
        private String TokenHash { get; set; }

        public AddOurStockRoomOperation(string nameOfStock, string adress, int numberOfStock, string tokenHash)
        {
            NameOfStock = nameOfStock;
            Adress = adress;
            NumberOfStock = numberOfStock;
            TokenHash = tokenHash;
            RussianName = "Добавление склада";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var existStock =
                Context.OurStockRooms.FirstOrDefault(
                    x => (x.Adress == Adress || x.Name == NameOfStock || x.NumberOfStock == NumberOfStock) && !x.Deleted);
            if(existStock != null)
                throw new ExistsObjectException("Присутствуют совпадающие поля!");
            var stock = new DataBase.OurStocks.OurStockRoom
            {
                Name = NameOfStock,
                NumberOfStock = NumberOfStock,
                Adress = Adress
            };
            Context.OurStockRooms.Add(stock);
            Context.SaveChanges();
        }
    }
}