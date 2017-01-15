using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;

namespace ITfamily.Utils.WebApi.Response
{
    public class RulesForPricesResponse : BaseResponse
    {
        public List<RuleForPriceModel> RuleForPriceModels { get; set; }
        public Decimal MinPriceUsd { get; set; }
        public Decimal MaxPriceUsd { get; set; }
    }
}