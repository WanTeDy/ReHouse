using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.WebApi.Response
{
    public class AuthResponse : BaseResponse
    {
        public List<OrderCities> OrderCities { get; set; }
        public Contractor Contractor { get; set; }
        public String TokenHash { get; set; }
        public Role Role { get; set; }
        public String ProviderLogin1 { get; set; }
        public String ProviderPassword1 { get; set; }
        public List<AuthorityForOneRoleModel> AuthorityForOneRoleModels { get; set; }
        public String NameContractor { get; set; }
        public Int32 QuantityProducts { get; set; }
        public Decimal CourseExtractCashless { get; set; }
        public Decimal CourseCash { get; set; }
        public String SecondNameContractor { get; set; }
        public String FatherNameContractor { get; set; }
        public String Email { get; set; }
        public String DeliveryCity { get; set; }
        public String DeliveryStreet { get; set; }
        public String DeliveryHome { get; set; }
        public String DeliveryAppartament { get; set; }
        public String DeliveryAdditional { get; set; }


        public Decimal CreditLimit { get; set; }
        //public StatusRole StatusRole { get; set; }
    }
}