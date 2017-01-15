using System.Collections.Generic;

namespace ITfamily.Utils.WebApi.Request
{
    public class CurrencyRequest : BaseRequest
    {
        public List<DataBase.Currencies.Currency> Currencies { get; set; } 
    }
}