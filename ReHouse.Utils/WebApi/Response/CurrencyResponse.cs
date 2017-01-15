using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response
{
    public class CurrencyResponse : BaseResponse
    {
        public List<CurrenciesModel> CollectionCurrencies { get; set; }
        public Boolean IsNewCurrency { get; set; }
    }
}