using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.Filters;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.WebApi.Response
{
    public class OurStockResponse : BaseResponse
    {
        public List<ItFamilyVendor> ItFamilyVendorsForOneCategory { get; set; }
        public List<ItFamilyVendor> AllItFamilyVendors { get; set; }
        public List<UnitOfCommodityModel> UnitOfCommodityModels { get; set; }
        public UnitOfCommodity UnitOfCommodity { get; set; }
        public List<ProductProperty> ProductProperties { get; set; }
        public List<PropertyValueModel> PropertyValueModels { get; set; }
        public String CategoryName { get; set; }
    }
}