using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response
{
    public class CustomerCardsResponse : BaseResponse
    {
        public List<CustomerCardModel> CustomerCards { get; set; }
    }
}