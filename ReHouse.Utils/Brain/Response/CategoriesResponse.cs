using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.Brain.Response
{
    public class CategoriesResponse : BaseBrainResponse
    {
        public Int32 status { get; set; }
        public List<BrainCategory> result { get; set; }}
}