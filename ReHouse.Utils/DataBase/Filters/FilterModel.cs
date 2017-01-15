using System;

namespace ITfamily.Utils.DataBase.Filters
{
    public class FilterModel
    {
        public String PropertyName { get; set; }
        public Int32 PropertyId { get; set; }
        public String Value { get; set; }
        public Int32 PropertyValueId { get; set; } 
    }
}