using System;

namespace ITfamily.Utils.DataBase.OtherOurDataForDb
{
    public class UpdateFile : BaseObj
    {
        public String FullPath { get; set; }
        public String FileName { get; set; }
        public byte[] HashMd5 { get; set; }
    }
}