using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class CarouselFacade : BaseFacade
    {
        public static async Task<CarouselResponse> AddCarousel(string tokenHash, string nameFile, string firstString, string secondGreenString, string thirdString, string urlHref, byte[] bytes)
        {
            var requestObj = new CarouselRequest
            {
                TokenHash = tokenHash, FirstString = firstString, SecondGreenString = secondGreenString, ThirdString = thirdString, Bytes = bytes, UrlOrNameFile = nameFile, UrlHref = urlHref
            };
            var response = await Post("api/Carousel/AddCarousel", requestObj, typeof(CarouselResponse)).ConfigureAwait(false);

            var res = response as CarouselResponse;
            return res;
        }
        public static async Task<CarouselResponse> UpdateCarousels(string tokenHash, int idCarousel, string urlOrNameFile, bool isUrl, string firstString, string secondGreenString, string thirdString, string urlHref, byte[] bytes)
        {
            var requestObj = new CarouselRequest
            {
                TokenHash = tokenHash,
                FirstString = firstString,
                SecondGreenString = secondGreenString,
                ThirdString = thirdString,
                Bytes = bytes,
                IsUrl = isUrl,
                IdCarousel = idCarousel,
                UrlOrNameFile = urlOrNameFile,
                UrlHref = urlHref
            };
            var response = await Post("api/Carousel/UpdateCarousels", requestObj, typeof(CarouselResponse)).ConfigureAwait(false);

            var res = response as CarouselResponse;
            return res;
        }
        public static async Task<CarouselResponse> DeleteCarousel(string tokenHash, int deleteId)
        {
            var requestObj = new CarouselRequest { TokenHash = tokenHash, IdCarousel = deleteId };
            var response = await Post("api/Carousel/DeleteCarousel", requestObj, typeof(CarouselResponse)).ConfigureAwait(false);

            var res = response as CarouselResponse;
            return res;
        }
        public static async Task<CarouselResponse> GetCarousels()
        {
            var requestObj = new BaseRequest();
            var response = await Post("api/Carousel/GetCarousels", requestObj, typeof(CarouselResponse)).ConfigureAwait(false);

            var res = response as CarouselResponse;
            return res;
        }
    }
}