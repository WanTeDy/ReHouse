using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.ChangeCarousel
{
    public class GetCarouselsOperation : BaseOperation
    {
        public List<CarouselModel> CarouselModels { get; set; }
        
        protected override void InTransaction()
        {
            CarouselModels = CommonAccess.GetCarouselModels(Context);
        }
    }
}