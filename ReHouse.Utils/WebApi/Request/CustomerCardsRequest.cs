using System;
using ITfamily.Utils.DataBase.CreditInformation;

namespace ITfamily.Utils.WebApi.Request
{
    public class CustomerCardsRequest : BaseRequest
    {
        public Int32 SelectedId { get; set; }
        public CustomerCard AddComesMoneyCustomerCard { get; set; }
    }
}