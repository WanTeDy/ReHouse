using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Vacancies
{
    public class Vacancy : BaseObj
    {
        /// <summary>
        /// Vacancy's title
        /// </summary> 
        public String Title { get; set; }
        /// <summary>
        /// Vacancy's description
        /// </summary> 
        public String Description { get; set; }
        /// <summary>
        /// Vacancy's creation datetime
        /// </summary> 
        public DateTime Date { get; set; }
    }
}