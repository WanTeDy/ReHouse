using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;


namespace ITfamily.Utils.Brain.Response
{
    public class VendorsResponse : BaseBrainResponse
    {
        public Int32 status { get; set; }
        public new List<Vendor> result { get; set; }
    }
}