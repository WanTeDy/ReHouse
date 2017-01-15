using System;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class AuthorityForOneRoleModel
    {
        public Int32 AuthorityId { get; set; }
        //public Int32 RoleId { get; set; }
        public String RussianNameOperation { get; set; }
        /// <summary>
        /// true - is access
        /// false - isn't access
        /// </summary>
        public Boolean IsAccess { get; set; }
    }
}