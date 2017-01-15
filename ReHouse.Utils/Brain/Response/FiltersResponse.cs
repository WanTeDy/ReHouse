using System;
using System.Collections.Generic;
using ITfamily.Utils.Brain.Models;

namespace ITfamily.Utils.Brain.Response
{
    public class FiltersResponse : BaseBrainResponse
    {
        public Int32 status { get; set; }
        public List<Filters> result { get; set; }
    }
}