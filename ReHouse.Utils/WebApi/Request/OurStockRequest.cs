using System;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.WebApi.Request
{
    public class OurStockRequest : BaseRequest
    {
        public Int32 OrderComesId { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 FromOrderOutId { get; set; }
        public Int32 SelectedId { get; set; }
        public String NewName { get; set; }
        public Int32? ItfamilyParentId { get; set; }
        public UnitOfCommodity UnitOfCommodity { get; set; }
        public PropertyValueModel PropertyValueModel { get; set; }
    }
}