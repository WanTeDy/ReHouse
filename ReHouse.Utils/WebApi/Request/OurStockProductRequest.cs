using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.WebApi.Request
{
    public class OurStockProductRequest : BaseRequest
    {
        public Int32 SelectedId { get; set; }
        public Int32 NumberOfStock { get; set; }
        public Byte[] MainImageBytes { get; set; }
        //public List<ImagesBytes> ImagesBytes { get; set; }
        public StockProduct StockProduct { get; set; }
        public Decimal PriceUsdForManager { get; set; }
        public Decimal PriceUsdForPartner { get; set; }
        public Decimal PriceUsdForClients { get; set; }
        public Boolean IsPriceForOneProduct { get; set; }
    }
}