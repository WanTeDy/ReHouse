using System;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class JournalOrderFacade : BaseFacade
    {
        public static async Task<JournalResponse> GetJournalOrders(string tokenHash, OrderType orderType, bool check = true)
        {
            var requestObj = new JournalRequest { TokenHash = tokenHash, OrderType = orderType};
            var response = await Post("api/JournalOrder/GetJournalOrders", requestObj, typeof(JournalResponse), check).ConfigureAwait(false);

            var res = response as JournalResponse;
            return res;
        }
        public static async Task<JournalResponse> GetJournalOrdersWithDates(string tokenHash, OrderType orderType, DateTime @from, DateTime to, bool check = true)
        {
            var requestObj = new JournalRequest { TokenHash = tokenHash, OrderType = orderType, From = @from, To = to};
            var response = await Post("api/JournalOrder/GetJournalOrdersWithDates", requestObj, typeof(JournalResponse), check).ConfigureAwait(false);

            var res = response as JournalResponse;
            return res;
        }
    }
}