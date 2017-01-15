using System;
using ITfamily.Utils.Brain.Response.Models;

namespace ITfamily.Utils.Brain.Response
{
    public class BrainProductsResponse : BaseBrainResponse
    {
        public Int32 status { get; set; }
        public new HelperProducts result { get; set; }
    }
}