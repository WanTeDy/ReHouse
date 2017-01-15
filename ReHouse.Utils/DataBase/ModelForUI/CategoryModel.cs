using System;
using System.Collections.Generic;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class CategoryModel
    {
        public Int32? parentID { get; set; }
        public Int32 categoryID { get; set; }
        public String name { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public bool IsActive { get; set; }
    }
}