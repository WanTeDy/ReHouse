using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.WebApi.Response
{
    public class OurStockRoomResponse : BaseResponse
    {
        public List<StockProduct> StockProducts { get; set; }
        public OurStockRoom OurStockRoom { get; set; }
        public List<OurStockRoom> OurStockRooms { get; set; }
        public List<ItFamilyCategory> ItFamilyCategories { get; set; }
    }
}