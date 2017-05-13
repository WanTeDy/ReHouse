using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Cart
{
    public class LoadCartOperation : BaseOperation
    {
        private Int32 _length = 90;
        private Int32 _subLength = 85;
        private List<CartModel> _cartModel { get; set; }
        public List<CartAdvertModel> _adverts { get; set; }

        public LoadCartOperation(List<CartModel> cartModel)
        {
            _cartModel = cartModel;
            RussianName = "Получение избранных объявлений";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _adverts = new List<CartAdvertModel>();
            foreach (var item in _cartModel)
            {
                CartAdvertModel adv = null;
                if (item.Type == AdvertsType.NewBuilding)
                {
                    var el = Context.NewBuildings.FirstOrDefault(x => !x.Deleted && x.IsModerated && x.Id == item.AdvertId);
                    if (el != null)
                    {
                        adv = new CartAdvertModel()
                        {
                            Id = item.AdvertId,
                            Price = el.Price,
                            Adress = el.Adress,
                            Type = item.Type,
                            Description = el.ExpluatationDate.Name,
                            Name = el.Name,
                            Image = el.Images.FirstOrDefault(x => !x.Deleted),
                            IsHot = el.IsHot,
                            IsExclusive = el.IsExclusive,
                        };
                        _adverts.Add(adv);
                    }
                }
                else
                {
                    var el = Context.Adverts.FirstOrDefault(x => !x.Deleted && x.IsModerated && x.Id == item.AdvertId);
                    if (el != null)
                    {
                        adv = new CartAdvertModel()
                        {
                            Id = item.AdvertId,
                            Price = el.Price,
                            Adress = el.Street,
                            Type = item.Type,
                            Description = el.Description.Length > _length ? el.Description.Substring(0, _subLength) + "..." : el.Description,
                            Name = el.Title.RussianName,
                            Image = el.Images.FirstOrDefault(x => !x.Deleted),
                            IsHot = el.IsHot,
                            IsExclusive = el.IsExclusive,
                        };
                        _adverts.Add(adv);
                    }
                }
            }
        }
    }
}