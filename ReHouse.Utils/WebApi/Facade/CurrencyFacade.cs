using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class CurrencyFacade : BaseFacade
    {
        public static async Task<CurrencyResponse> CheckNewCurrency(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Currency/CheckNewCurrency", requestObj, typeof(CurrencyResponse)).ConfigureAwait(false);

            var res = response as CurrencyResponse;
            return res;
        }
        public static async Task<CurrencyResponse> GetCurrencyCollections(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Currency/GetCurrencyCollections", requestObj, typeof(CurrencyResponse)).ConfigureAwait(false);

            var res = response as CurrencyResponse;
            return res;
        }
        public static async Task<CurrencyResponse> GetCurrencyForPartnerOrClients(string tokenHash, bool check = true)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Currency/GetCurrencyForPartnerOrClients", requestObj, typeof(CurrencyResponse), check).ConfigureAwait(false);

            var res = response as CurrencyResponse;
            return res;
        }
        public static async Task<BaseResponse> ChangeOurCurrency(string tokenHash, List<Currency> currencies)
        {
            var requestObj = new CurrencyRequest { TokenHash = tokenHash, Currencies = currencies};
            var response = await Post("api/Currency/ChangeOurCurrency", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}