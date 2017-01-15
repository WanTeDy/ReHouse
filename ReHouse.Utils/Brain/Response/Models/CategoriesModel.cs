using System;
using System.Collections.Generic;

namespace ITfamily.Utils.Brain.Response.Models
{
    public class CategoriesModel
    {
        public Int32 parentID { get; set; }
        public Int32 categoryID { get; set; }
        public String name { get; set; }
        public List<CategoriesModel> Categories { get; set; }

        public CategoriesModel()
        {
            Categories = new List<CategoriesModel>();
        }
    }
}