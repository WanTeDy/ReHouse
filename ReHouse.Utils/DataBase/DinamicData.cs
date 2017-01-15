using System;
using System.ComponentModel.DataAnnotations;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase
{
    public class DinamicData
    {
        public Int32 Id { get; set; }
        [MaxLength(50)]
        public String FirstString { get; set; }
        [MaxLength(50)]
        public String SecondString { get; set; }
        [MaxLength(50)]
        public String ThirdString { get; set; }
        [MaxLength(350)]
        public String UrlPicture { get; set; }
        [MaxLength(350)]
        public String UrlHref { get; set; }
        [MaxLength(350)]
        public String Notes { get; set; }
        public TypeData TypeData { get; set; }
        public Boolean Deleted { get; set; }
    }
}