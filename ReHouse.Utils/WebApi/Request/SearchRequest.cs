using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.Filters;

namespace ITfamily.Utils.WebApi.Request
{
    public class SearchRequest : BaseRequest
    {
        public Int32 CategoryId { get; set; }
        public String SearchName { get; set; }
        public List<FilterModel> FilterModels { get; set; }
    }
}