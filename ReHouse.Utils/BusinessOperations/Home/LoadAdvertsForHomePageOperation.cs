using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Home
{
    public class LoadAdvertsForHomePageOperation : BaseOperation
    {
        private Int32 _count = 10;
        private String _tokenHash { get; set; }        
        public List<Advert> _hotAdverts { get; set; }
        public List<Advert> _flatSaleAdverts { get; set; }
        public List<NewBuilding> _newBuildingAdverts { get; set; }
        public List<Advert> _houseSaleAdverts { get; set; }

        public LoadAdvertsForHomePageOperation(string tokenHash)
        {
            _tokenHash = tokenHash;            
            RussianName = "Получение объявлений для главной страницы";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _hotAdverts = Context.Adverts.Where(x => !x.Deleted && x.IsHot)
                .OrderByDescending(x => x.PublicationDate).Take(_count).ToList();
            _flatSaleAdverts = Context.Adverts.Where(x => !x.Deleted && x.Category.ParentId == (int)ParrentCategories.Flat)
                .OrderByDescending(x => x.PublicationDate).Take(_count).ToList();
            _houseSaleAdverts = Context.Adverts.Where(x => !x.Deleted && (x.Category.ParentId == (int)ParrentCategories.House || x.Category.ParentId == (int)ParrentCategories.Homestead))
                .OrderByDescending(x => x.PublicationDate).Take(_count).ToList();
            _newBuildingAdverts = Context.NewBuildings.Where(x => !x.Deleted)
                .OrderByDescending(x => x.PublicationDate).Take(_count).ToList();
        }
    }
}