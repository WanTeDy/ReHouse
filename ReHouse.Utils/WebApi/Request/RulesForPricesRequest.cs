using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;

namespace ITfamily.Utils.WebApi.Request
{
    public class RulesForPricesRequest : BaseRequest
    {
        public Int32 CategoryId { get; set; }
        public List<RuleForPriceModel> RuleForPrices { get; set; }
    }
}