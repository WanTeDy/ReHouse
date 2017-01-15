using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class EmployersFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddEmployer(Contractor employeer, int roleId, string tokenHash)
        {
            var requestObj = new EmployersRequest { Employeer = employeer, SelId = roleId, TokenHash = tokenHash };
            var response = await Post("api/Employers/AddEmployer", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteEmployer(int deleteId, string tokenHash)
        {
            var requestObj = new EmployersRequest { SelId = deleteId, TokenHash = tokenHash };
            var response = await Post("api/Employers/DeleteEmployer", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateEmployer(Contractor employeer, int roleId, string tokenHash)
        {
            var requestObj = new EmployersRequest { Employeer = employeer, SelId = roleId, TokenHash = tokenHash };
            var response = await Post("api/Employers/UpdateEmployer", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> SelectTopProductForCategory(int productId, string tokenHash)
        {
            var requestObj = new EmployersRequest { SelId = productId, TokenHash = tokenHash };
            var response = await Post("api/Employers/SelectTopProductForCategory", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<TopProductResponse> GetTopProduct(int productId, string tokenHash, bool check)
        {
            var requestObj = new EmployersRequest { SelId = productId, TokenHash = tokenHash };
            var response = await Post("api/Employers/GetTopProduct", requestObj, typeof(TopProductResponse), check).ConfigureAwait(false);

            var res = response as TopProductResponse;
            return res;
        }
    }
}