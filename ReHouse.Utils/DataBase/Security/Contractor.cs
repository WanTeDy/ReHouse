using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.CreditInformation;

namespace ITfamily.Utils.DataBase.Security
{
    public class Contractor : BaseObj
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public String SecondName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public String FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public String FatherName { get; set; }
        /// <summary>
        /// Обязательное поле (Используется как логин)
        /// </summary>
        public String Phone { get; set; }
        /// <summary>
        /// Обязательное поле
        /// </summary>
        public String Email { get; set; }
        public String Url { get; set; }
        public String Password { get; set; }
        public String TokenHash { get; set; }
        /// <summary>
        /// Активен - true
        /// </summary>
        public Boolean IsActive { get; set; }
        public Role Role { get; set; }
        public Int32 RoleId { get; set; }
        /// <summary>
        /// Форма собственности (ООО, ЧП, АО….) из таблицы
        /// </summary>
        public String Ownership { get; set; }
        /// <summary>
        /// Вид налогообложения
        /// </summary>
        public String FormOfTaxation { get; set; }
        /// <summary>
        /// Ставка налогообложения
        /// </summary>
        public Double? TaxRate { get; set; }
        /// <summary>
        /// Все реквизиты (реквизиты одного из предприятий в приложении)
        /// </summary>
        public String Requisite { get; set; }
        /// <summary>
        /// карточка клиента (движение товара, денег приход/отход)
        /// </summary>
        public virtual List<CustomerCard> CustomerCards { get; set; }
        /// <summary>
        /// Кредитный лимит
        /// </summary>
        public Decimal CreditLimit { get; set; }

        public String DeliveryCity { get; set; }
        public String DeliveryStreet { get; set; }
        public String DeliveryHome { get; set; }
        public String DeliveryAppartament { get; set; }
        public String DeliveryAdditional { get; set; }
        public String VkKey { get; set; }
        public String FacebookKey { get; set; }
        public String GoogleKey { get; set; }

        //public virtual List<OrderQueue> OrderQueues { get; set; }
    }
}