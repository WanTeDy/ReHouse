﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Cart
{
    public class LoadCartOperation : BaseOperation
    {
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
                            IsNew = el.IsNew,
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
                            Description = el.Description.Length > ConstV.DescMinimizeSymbols + 5 ? el.Description.Substring(0, ConstV.DescMinimizeSymbols) + "..." : el.Description,
                            Name = el.Title.RussianName,
                            RentPeriodType = el.RentPeriodType,
                            //Name = el.TitleName,
                            Image = el.Images.FirstOrDefault(x => !x.Deleted),
                            IsHot = el.IsHot,
                            IsExclusive = el.IsExclusive,
                            IsNew = el.IsNew,
                            FullSquare = el.AdvertPropertyValues.FirstOrDefault(x => x.AdvertPropertyId == 3)?.PropertiesValue,
                            Square = el.AdvertPropertyValues.FirstOrDefault(x => x.AdvertPropertyId == 7)?.PropertiesValue,
                        };
                        _adverts.Add(adv);
                    }
                }
            }
        }
    }
}