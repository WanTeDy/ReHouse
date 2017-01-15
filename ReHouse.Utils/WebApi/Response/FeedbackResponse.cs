using System.Collections.Generic;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Response
{
    public class FeedbackResponse : BaseResponse
    {
        public List<Feedback> Feedbacks { get; set; }
    }
}