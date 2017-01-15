using System;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.BusinessOperations.BussOpWithDapper.ForDbTypes
{
    public class Categories
    {
        public Int32 Id { get; set; }
        public Int32? ItFamilyParentId { get; set; }
        public Int32 ParentId { get; set; }
        public Int32 CategoryId { get; set; }
        public String Name { get; set; }
        public Boolean HasRule { get; set; }
        public Int32? BrainProduct_Id { get; set; }
        public FromWhatProvider FromWhatProvider { get; set; }
    }
}