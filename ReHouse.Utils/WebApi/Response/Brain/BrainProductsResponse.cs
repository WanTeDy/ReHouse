using System;
using System.Collections.Generic;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response.Brain
{
    public class BrainProductsResponse : BaseResponse
    {
        public List<BrainProductModel> BrainProductModels { get; set; }
        public List<BrainProductModel> ProductsOverview { get; set; }
        public BrainProductModel BrainProductModel { get; set; }
        public BrainProductFullInfo BrainProductFullInfo { get; set; }
        public Int32 CountPages { get; set; }
        public Int32 TotalItems { get; set; }
    }
}