using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.WebApi.Response
{
    public class TopProductResponse : BaseResponse
    {
        public StockProduct StockProduct { get; set; }
    }
}