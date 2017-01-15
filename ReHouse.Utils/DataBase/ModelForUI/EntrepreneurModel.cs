using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class EntrepreneurModel
    {
        public Int32 Id { get; set; }
        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public String FatherName { get; set; }

        public String DeliveryCity { get; set; }
        public String DeliveryStreet { get; set; }
        public String DeliveryHome { get; set; }
        public String DeliveryAppartament { get; set; }
        public String DeliveryAdditional { get; set; }
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
        /// For Our Legal Entity
        /// </summary>
        public String FormOfTaxation { get; set; }
        /// <summary>
        /// For Our Legal Entity
        /// </summary>
        public Double? TaxRate { get; set; }

        public String Password { get; set; }
        public String TokenHash { get; set; }
        public DateTime LastPaidDate { get; set; }
        public Decimal Balance { get; set; }
        //public virtual List<CustomerCard> CustomerCards { get; set; }
        public Decimal CreditLimit { get; set; }
        public Boolean IsActive { get; set; }
    }
}