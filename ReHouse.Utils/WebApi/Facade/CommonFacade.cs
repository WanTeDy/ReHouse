using System;
using System.Threading.Tasks;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class CommonFacade : BaseFacade
    {
        public static async Task<AuthResponse> OrderCities()
        {
            var requestObj = new BaseRequest();
            var response = await Post("api/Common/LoadOrderCities", requestObj, typeof(AuthResponse)).ConfigureAwait(false);

            var res = response as AuthResponse;
            return res;
        }
        public static async Task<RoleResponse> RoleModel()
        {
            var requestObj = new BaseRequest();
            var response = await Post("api/Common/LoadRoles", requestObj, typeof(RoleResponse)).ConfigureAwait(false);

            var res = response as RoleResponse;
            return res;
        }

        public static async Task<FeedbackResponse> Feedbacks(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Common/LoadFeedback", requestObj, typeof(FeedbackResponse)).ConfigureAwait(false);

            var res = response as FeedbackResponse;
            return res;
        }

        public static async Task<EmployeerResponse> Employeers(string tokenHash)
        {
            var requestObj = new BaseRequest {TokenHash = tokenHash};
            var response = await Post("api/Common/LoadEmployeers", requestObj, typeof(EmployeerResponse)).ConfigureAwait(false);

            var res = response as EmployeerResponse;
            return res;
        }

        public static async Task<EmployeerResponse> LoadEmployeer(int selId, string tokenHash)
        {
            var requestObj = new CommonRequest { SelectedId = selId, TokenHash = tokenHash};
            var response = await Post("api/Common/LoadEmployeer", requestObj, typeof(EmployeerResponse)).ConfigureAwait(false);

            var res = response as EmployeerResponse;
            return res;
        }

        public static async Task<ContractorResponse> Clients(string tokenHash)
        {
            var requestObj = new BaseRequest{TokenHash = tokenHash};
            var response = await Post("api/Common/LoadClients", requestObj, typeof(ContractorResponse)).ConfigureAwait(false);

            var res = response as ContractorResponse;
            return res;
        }

        public static async Task<ContractorResponse> LoadClient(int selId, String tokenHash)
        {
            var requestObj = new CommonRequest { SelectedId = selId, TokenHash = tokenHash};
            var response = await Post("api/Common/LoadClient", requestObj, typeof(ContractorResponse)).ConfigureAwait(false);

            var res = response as ContractorResponse;
            return res;
        }

        public static async Task<EntrepreneurResponse> LegalEntities(string tokenHash)
        {
            var requestObj = new BaseRequest{TokenHash = tokenHash};
            var response = await Post("api/Common/LoadEntrepreneurs", requestObj, typeof(EntrepreneurResponse)).ConfigureAwait(false);

            var res = response as EntrepreneurResponse;
            return res;
        }

        public static async Task<EntrepreneurResponse> LoadLegalEntity(string tokenHash, int selId)
        {
            var requestObj = new CommonRequest { SelectedId = selId, TokenHash = tokenHash};
            var response = await Post("api/Common/LoadEntrepreneur", requestObj, typeof(EntrepreneurResponse)).ConfigureAwait(false);

            var res = response as EntrepreneurResponse;
            return res;
        }
        public static async Task<ServiceResponse> LoadServices()
        {
            var response = await Post("api/Common/LoadServices", new BaseRequest(), typeof(ServiceResponse)).ConfigureAwait(false);

            var res = response as ServiceResponse;
            return res;
        }

        public static async Task<ServiceResponse> LoadService(int selId)
        {
            var requestObj = new CommonRequest { SelectedId = selId };
            var response = await Post("api/Common/LoadService", requestObj, typeof(ServiceResponse)).ConfigureAwait(false);

            var res = response as ServiceResponse;
            return res;
        }

        public static async Task<FrequencyResponse> LoadFrequency()
        {
            var response = await Post("api/Common/LoadFrequency", new BaseRequest(), typeof(FrequencyResponse)).ConfigureAwait(false);

            var res = response as FrequencyResponse;
            return res;
        }

        public static async Task<FrequencyResponse> LoadFrequency(int selId)
        {
            var response = await Post("api/Common/LoadSelFrequency", new BaseRequest(), typeof(FrequencyResponse)).ConfigureAwait(false);

            var res = response as FrequencyResponse;
            return res;
        }
    }
}