using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response
{
    public class JournalResponse : BaseResponse
    {
        public List<OrderComesModel> OrderComesModels { get; set; } 
    }
}