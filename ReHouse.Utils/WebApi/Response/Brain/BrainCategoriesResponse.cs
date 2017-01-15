using System.Collections.Generic;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.WebApi.Response.Brain
{
    public class BrainCategoriesResponse : BaseResponse
    {
        public List<BrainCategory> BrainCategories { get; set; } 
    }
}