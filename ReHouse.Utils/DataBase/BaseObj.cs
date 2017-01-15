using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReHouse.Utils.DataBase
{
    /// <summary>
    /// Base object for each database object
    /// </summary>
    public class BaseObj
    {
        /// <summary>
        /// Database id of object
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        /// <summary>
        /// Is object deleted
        /// </summary>
        public Boolean Deleted { get; set; }
    }
}