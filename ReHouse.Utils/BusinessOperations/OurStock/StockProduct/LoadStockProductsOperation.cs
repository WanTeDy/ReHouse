using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class LoadStockProductsOperation : BaseEntityDapperOperation
    {
        public List<DataBase.OurStocks.StockProduct> StockProducts { get; set; }
        //public List<StockProductModel> StockProducts { get; set; }
        private String TokenHash { get; set; }
        private Int32 CategoryId { get; set; }
        private Int32 OurStockRoomId { get; set; }

        public LoadStockProductsOperation(string tokenHash, int categoryId, int ourStockRoomId)
        {
            TokenHash = tokenHash;
            CategoryId = categoryId;
            OurStockRoomId = ourStockRoomId;
            //RussianName = "Загрузка товаров со своего склада";
        }

        protected override void InTransaction()
        {
            //CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var dictionaryStocks = new Dictionary<int, string>();
            var stocks = Context.OurStockRooms.ToList();
            if (stocks.Count > 0)
            {
                foreach (var ourStockRoom in stocks)
                {
                    dictionaryStocks.Add(ourStockRoom.Id, ourStockRoom.Name);
                }
            }
            //var stockProds = Gateway.GetStockProductsForOurStockWithUnitOfCommodity(CategoryId, "*","AND st.FromWhatProvider=" + (int) FromWhatProvider.OurProduct, dictionaryStocks);
            var stockProds = Gateway.GetStockProductsWithUnitOfCommodityQueryMultipleV2(CategoryId, (int)FromWhatProvider.OurProduct, dictionaryStocks);
            var helper = new HelperPriceForListInOurStockRooms(TokenHash, CategoryId, Context);
            helper.Init();
            
            StockProducts = helper.FormStockProductsList(stockProds);
        }
    }
}