using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.WebApi.Response
{
    public class AnalyzeResponse : BaseResponse
    {
        public List<AnalyzeGoodsFromOrderComesModel> AnalyzeDatas { get; set; }
    }
}