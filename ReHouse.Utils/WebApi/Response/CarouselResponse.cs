using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Response
{
    public class CarouselResponse : BaseResponse
    {
        public List<CarouselModel> CarouselModels { get; set; } 
    }
}