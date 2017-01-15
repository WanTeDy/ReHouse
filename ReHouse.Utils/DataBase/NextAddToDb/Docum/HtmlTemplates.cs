using System;
using System.Collections.Generic;

namespace ITfamily.Utils.DataBase.Docum
{
    public class HtmlTemplates : BaseObj
    {
        public byte[] Template { get; set; }
        public String Name { get; set; }
        public DateTime DateOfCreate { get; set; }
        public virtual List<ChangeableNames> ChangeableNames { get; set; }
    }
}