using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.BusinessOperations.Common
{
    public class SearchAdvertsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _searchRequest { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<CartAdvertModel> _adverts { get; set; }
        //public List<Article> _articles { get; set; }

        public SearchAdvertsOperation(string tokenHash, string searchRequest, int page, int count)
        {
            _tokenHash = tokenHash;
            _searchRequest = searchRequest;
            _page = page;
            _count = count;
            RussianName = "Получение объявлений по поиску";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            var strings = _searchRequest.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            strings.ForEach(
                x =>
                {
                    if (x.Length > 3)
                        x = x.Substring(0, x.Length - 1);
                });

            var flats = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && strings.Any(r => x.Id.ToString().Contains(r) || x.Title.RussianName.ToLower().Contains(r) || x.Description.ToLower().Contains(r) || x.District.RussianName.ToLower().Contains(r) || x.AdvertPropertyValues.All(y => y.PropertiesValue.Contains(r))))
            .OrderByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
            _adverts = new List<CartAdvertModel>();
            _adverts.AddRange(flats.Select(x =>

                new CartAdvertModel()
                {
                    Id = x.Id,
                    Price = x.Price,
                    Adress = x.Street,
                    Type = x.Type,
                    Description = x.Description.Length > ConstV.DescMinimizeSymbols + 5 ? x.Description.Substring(0, ConstV.DescMinimizeSymbols) + "..." : x.Description,
                    Name = x.Title.RussianName,
                    //Name = el.TitleName,
                    Image = x.Images.FirstOrDefault(y => !x.Deleted),
                    IsHot = x.IsHot,
                    IsExclusive = x.IsExclusive,
                }

            ));

            var newBuilding = Context.NewBuildings.Where(x => !x.Deleted && x.IsModerated && strings.Any(r => x.Id.ToString().Contains(r) || x.Name.ToLower().Contains(r) || x.Description.ToLower().Contains(r) || x.Adress.ToLower().Contains(r) || x.Construct.ToLower().Contains(r)
                || x.District.RussianName.ToLower().Contains(r) || x.Heating.ToLower().Contains(r) || x.Parking.ToLower().Contains(r)))
                .OrderByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Take(_count).ToList();

            _adverts.AddRange(newBuilding.Select(x =>

                new CartAdvertModel()
                {
                    Id = x.Id,
                    Price = x.Price,
                    Adress = x.Adress,
                    Type = AdvertsType.NewBuilding,
                    Description = x.ExpluatationDate.Name,
                    Name = x.Name,
                    //Name = el.TitleName,
                    Image = x.Images.FirstOrDefault(y => !x.Deleted),
                    IsHot = x.IsHot,
                    IsExclusive = x.IsExclusive,
                }

            ));

            //_articles = Context.Articles.Where(x => !x.Deleted)
            //    .OrderByDescending(x => x.Date).Take(_articlesCount).ToList();
        }
    }
}