using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Vacancies
{
    public class Vacancy : BaseObj
    {        
        /// <summary>
        /// Feedback's description
        /// </summary> 
        public String Description { get; set; }
        /// <summary>
        /// Feedback's creation datetime
        /// </summary> 
        public DateTime Date { get; set; }
    }
}