using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.Brain.Response
{
    public class BrainStocksResponse : BaseBrainResponse
    {
        public Int32 status { get; set; }
        public new List<BrainStocks> result { get; set; } 
    }
}