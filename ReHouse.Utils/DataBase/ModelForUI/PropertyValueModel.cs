using System;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class PropertyValueModel
    {
        public Int32 PropValueId { get; set; }
        public Int32 ProductPropertyId { get; set; }
        public String ProductPropertyName { get; set; }
        public String Value { get; set; }
        public Int32 StockProductId { get; set; }
    }
}