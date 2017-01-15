using System;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.WebApi.Request.Brain
{
    public class BrainProductsRequest : BaseRequest
    {
        public Int32 CategoryId { get; set; }
        public Int32 Page { get; set; }
        public Int32 ItemsPerPage { get; set; }
        public Int32 StockId { get; set; }
        public Boolean CheckInStock { get; set; }
        public ColumnSort ColumnSortOrder { get; set; }
        public String PropertyName { get; set; }
        public Boolean IsSite { get; set; }
        public Boolean IsBrainProductId { get; set; }
    }
}