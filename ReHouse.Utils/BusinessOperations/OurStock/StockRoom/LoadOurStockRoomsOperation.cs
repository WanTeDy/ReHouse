using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockRoom
{
    public class LoadOurStockRoomsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<DataBase.OurStocks.OurStockRoom> OurStockRooms { get; set; }
        public LoadOurStockRoomsOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Получение списка своих складов";
        }
        
        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var stocks = Context.OurStockRooms.Where(x => !x.Deleted).ToList();
            if (stocks.Count > 0)
            {
                OurStockRooms = stocks.Select(x => new DataBase.OurStocks.OurStockRoom
                {
                    Id = x.Id,
                    Name = x.Name,
                    Adress = x.Adress,
                    NumberOfStock = x.NumberOfStock
                }).ToList();
            }
        }
    }
}