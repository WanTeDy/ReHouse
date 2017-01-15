using System;
using System.Collections.Generic;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response
{
    public class SearchResponse : BaseResponse
    {
        public List<CategorySearchModel> CategorySearchModels { get; set; }
        public List<BrainProductModel> BrainProductModels { get; set; }
        public String SearchName { get; set; }
    }
}