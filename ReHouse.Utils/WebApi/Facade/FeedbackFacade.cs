using System.Threading.Tasks;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class FeedbackFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddFeedback(string message, string email, string sendName)
        {
            var requestObj = new FeedbackRequest
            {
                Email = email,
                Message = message,
                SendName = sendName,
            };
            var response = await Post("api/Feedback/AddFeedback", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> ChangeProcessedFeedback(bool processed, int selId, string tokenHash)
        {
            var requestObj = new FeedbackRequest
            {
                Processed = processed,
                SelId = selId,
                TokenHash = tokenHash
            };
            var response = await Post("api/Feedback/ChangeProcessedFeedback", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}