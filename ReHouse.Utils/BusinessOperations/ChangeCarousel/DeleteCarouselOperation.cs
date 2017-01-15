using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.BusinessOperations.ChangeCarousel
{
    public class DeleteCarouselOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 DeleteId { get; set; }
        public List<CarouselModel> CarouselModels { get; set; }
        public DeleteCarouselOperation(string tokenHash, int deleteId)
        {
            TokenHash = tokenHash;
            DeleteId = deleteId;
            RussianName = "Удаление одного элемента карусели";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var carousel =
                Context.DinamicDatas.FirstOrDefault( x => x.Id == DeleteId && !x.Deleted && x.TypeData == TypeData.Carousel);
            if(carousel != null)
                carousel.Deleted = true;
            Context.SaveChanges();
            CarouselModels = CommonAccess.GetCarouselModels(Context);
        }
    }
}