using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class OurStockRoomRequest : BaseRequest
    {
        public String Name { get; set; }
        public String Adres { get; set; }
        public Int32 NumberOfStock { get; set; }
        public Int32 SelectedId { get; set; }
    }
}