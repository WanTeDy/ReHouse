using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class RulesForPricesFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddCollectionRulesPriceForCategory(string tokenHash, int categoryId, List<RuleForPriceModel> ruleForPrices)
        {
            var requestObj = new RulesForPricesRequest { TokenHash = tokenHash, CategoryId = categoryId, RuleForPrices = ruleForPrices};
            var response = await Post("api/RulesForPrices/AddCollectionRulesPriceForCategory", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> AddGlobalRulesForProducts(string tokenHash, List<RuleForPriceModel> ruleForPrices)
        {
            var requestObj = new RulesForPricesRequest { TokenHash = tokenHash, RuleForPrices = ruleForPrices };
            var response = await Post("api/RulesForPrices/AddGlobalRulesForProducts", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<RulesForPricesResponse> GetNeedDataForGlobalRules(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/RulesForPrices/GetNeedDataForGlobalRules", requestObj, typeof(RulesForPricesResponse)).ConfigureAwait(false);

            var res = response as RulesForPricesResponse;
            return res;
        }
        //AddGlobalRulesForProductsOperation
        //GetNeedDataForGlobalRulesOperation
        public static async Task<BaseResponse> UpdateCollectionRulesPriceForCategory(string tokenHash, int categoryId, List<RuleForPriceModel> ruleForPrices)
        {
            var requestObj = new RulesForPricesRequest { TokenHash = tokenHash, CategoryId = categoryId, RuleForPrices = ruleForPrices };
            var response = await Post("api/RulesForPrices/UpdateCollectionRulesPriceForCategory", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //
        public static async Task<RulesForPricesResponse> GetCollectionRulesPriceForCategory(string tokenHash, int categoryId)
        {
            var requestObj = new RulesForPricesRequest { TokenHash = tokenHash, CategoryId = categoryId };
            var response = await Post("api/RulesForPrices/GetCollectionRulesPriceForCategory", requestObj, typeof(RulesForPricesResponse)).ConfigureAwait(false);

            var res = response as RulesForPricesResponse;
            return res;
        }
    }
}