using System;
using System.ComponentModel.DataAnnotations;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class Specifications
    {
        [Key]
        public Int32 Id { get; set; }
        public String name { get; set; }
        public String value { get; set; }
        public virtual BrainProduct BrainProduct { get; set; }
        public Int32 BrainProductId { get; set; }
    }
}