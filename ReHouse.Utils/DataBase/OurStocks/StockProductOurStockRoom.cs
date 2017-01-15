using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Filters;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class StockProductOurStockRoom : BaseObj
    {
        public Int32 StockProductId { get; set; }                
        public Int32 Amount { get; set; }        
        public Int32 OurStockRoomId { get; set; }        
        public virtual StockProduct StockProduct { get; set; }
        public virtual OurStockRoom OurStockRoom { get; set; }
    }
}