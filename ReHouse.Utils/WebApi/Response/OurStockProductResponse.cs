using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.WebApi.Response
{
    public class OurStockProductResponse : BaseResponse
    {
        public List<StockProduct> StockProducts { get; set; }
        public StockProductModel StockProductModel { get; set; }

        public StockProduct StockProduct { get; set; }
        public String CategoryName { get; set; }
        public String VendorName { get; set; }
    }
}