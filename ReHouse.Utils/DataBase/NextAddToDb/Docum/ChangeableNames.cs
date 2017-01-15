using System;

namespace ITfamily.Utils.DataBase.Docum
{
    public class ChangeableNames : BaseObj
    {
        public String LatinNameId { get; set; }
        public String RusNameId { get; set; }
        public Int32 HtmlTemplateId { get; set; }
        public Int32 Number { get; set; }
    }
}