using System.Threading.Tasks;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class CustomerCardsFacade : BaseFacade
    {
        public static async Task<CustomerCardsResponse> LoadCustomerCards(string tokenHash, int partnerId, bool check = true)
        {
            var requestObj = new CustomerCardsRequest { TokenHash = tokenHash, SelectedId = partnerId };
            var response = await Post("api/CustomerCards/LoadCustomerCards", requestObj, typeof(CustomerCardsResponse), check).ConfigureAwait(false);

            var res = response as CustomerCardsResponse;
            return res;
        }
        public static async Task<BaseResponse> AddComesMoney(string tokenHash, int partnerId, CustomerCard card)
        {
            var requestObj = new CustomerCardsRequest { TokenHash = tokenHash, SelectedId = partnerId, AddComesMoneyCustomerCard = card};
            var response = await Post("api/CustomerCards/AddComesMoney", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}