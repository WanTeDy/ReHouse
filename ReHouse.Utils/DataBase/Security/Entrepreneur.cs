using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.CreditInformation;

namespace ITfamily.Utils.DataBase.Security
{
    public class Entrepreneur : BaseObj
    {
        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public String FatherName { get; set; }

        public String Adress { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Url { get; set; }
        /// <summary>
        /// Role of Empl
        /// </summary>
        public Role Role { get; set; }
        public Int32 RoleId { get; set; }

        public String Ownership { get; set; }
        public String Requisite { get; set; }

        /// <summary>
        /// bytes to Base64 for image
        /// </summary>
        //public byte[] DocumentImage { get; set; }

        /// <summary>
        /// For Our Legal Entity
        /// </summary>
        public String FormOfTaxation { get; set; }
        /// <summary>
        /// For Our Legal Entity
        /// </summary>
        public Double? TaxRate { get; set; }

        /// <summary>
        /// For other Legal Entity
        /// </summary>
        //public Int32? CustomerCardId { get; set; }

        /// <summary>
        /// Our - true (Our entity has FormOfTaxation and TaxRate)
        /// Other - false( Other entity has CustomerCard)
        /// </summary>
        public Boolean IsOur { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public String Login { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public String Password { get; set; }
        public String TokenHash { get; set; }
        public virtual List<CustomerCard> CustomerCards { get; set; }
        public Decimal CreditLimit { get; set; }
    }
}