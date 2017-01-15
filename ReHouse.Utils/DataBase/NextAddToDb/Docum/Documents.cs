using System;

namespace ITfamily.Utils.DataBase.Docum
{
    public class Documents : BaseObj
    {
        public byte[] Document { get; set; }
        public TypeDoc TypeDoc { get; set; }
        public DateTime DateOfSpining { get; set; }
        public DateTime DateExpired { get; set; }
        public String Notes { get; set; }
    }
}