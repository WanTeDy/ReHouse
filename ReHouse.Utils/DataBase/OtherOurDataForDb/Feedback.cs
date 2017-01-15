using System;
using System.ComponentModel.DataAnnotations;

namespace ITfamily.Utils.DataBase.OtherOurDataForDb
{
    public class Feedback
    {
        public Int32 Id { get; set; }
        [MaxLength(128)]
        public String Name { get; set; }
        [MaxLength(255)]
        public String Email { get; set; }
        [MaxLength(1000)]
        public String Message { get; set; }
        public DateTime SendDate { get; set; }
        public Boolean Processed { get; set; }
    }
}