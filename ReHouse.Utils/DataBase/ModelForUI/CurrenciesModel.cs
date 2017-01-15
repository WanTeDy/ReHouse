using System;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class CurrenciesModel
    {
        public Int32 Id { get; set; }
        public Decimal Value { get; set; }
        public String Name { get; set; }
        public Int32? CurrencyId { get; set; }
        public String TypeCurrency { get; set; }
    }
}