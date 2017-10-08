using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.Home
{
    public class LoadAdvertsForHomePageOperation : BaseOperation
    {
        private Int32 _count = 10;
        private Int32 _articlesCount = 2;
        private String _tokenHash { get; set; }        
        public List<Advert> _hotAdverts { get; set; }
        public List<Advert> _flatSaleAdverts { get; set; }
        public List<NewBuilding> _newBuildingAdverts { get; set; }
        public List<Advert> _houseSaleAdverts { get; set; }
        public List<Article> _articles { get; set; }

        public LoadAdvertsForHomePageOperation(string tokenHash)
        {
            _tokenHash = tokenHash;            
            RussianName = "Получение объявлений для главной страницы";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _hotAdverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && x.IsHot)
                .OrderByDescending(x => x.PublicationDate).Take(_count).ToList();
            _hotAdverts.ForEach(
                x =>
                {
                    if (!String.IsNullOrEmpty(x.Description) && x.Description.Length > ConstV.DescMinimizeSymbols + 5)
                        x.Description = x.Description.Substring(0, ConstV.DescMinimizeSymbols) + "...";
                });

            _flatSaleAdverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && x.Category.ParentId == (int)ParrentCategories.Flat)
                .OrderByDescending(x=>x.IsHot).ThenByDescending(x => x.PublicationDate).Take(_count).ToList();
            _flatSaleAdverts.ForEach(
                x =>
                {
                    if (!String.IsNullOrEmpty(x.Description) && x.Description.Length > ConstV.DescMinimizeSymbols + 5)
                        x.Description = x.Description.Substring(0, ConstV.DescMinimizeSymbols) + "...";
                });

            _houseSaleAdverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && (x.Category.ParentId == (int)ParrentCategories.House || x.Category.ParentId == (int)ParrentCategories.Homestead))
                .OrderByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Take(_count).ToList();
            _houseSaleAdverts.ForEach(
                x =>
                {
                    if (!String.IsNullOrEmpty(x.Description) && x.Description.Length > ConstV.DescMinimizeSymbols + 5)
                        x.Description = x.Description.Substring(0, ConstV.DescMinimizeSymbols) + "...";
                });

            _newBuildingAdverts = Context.NewBuildings.Where(x => !x.Deleted && x.IsModerated)
                .OrderByDescending(x => x.IsHot).ThenByDescending(x => x.CreationDate).Take(_count).ToList();
            
            //_articles = Context.Articles.Where(x => !x.Deleted)
            //    .OrderByDescending(x => x.Date).Take(_articlesCount).ToList();
        }
    }
}