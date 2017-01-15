using System;

namespace ITfamily.Utils.BusinessOperations.BussOpWithDapper.ForDbTypes
{
    public class Properties
    {
        public Int32 Id { get; set; }
        public Int32 CategoryId { get; set; }
        public String PropertyName { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean Hidden { get; set; }        
    }
}