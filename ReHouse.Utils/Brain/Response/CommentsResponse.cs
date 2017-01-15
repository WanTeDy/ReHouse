using System;
using System.Collections.Generic;
using ITfamily.Utils.Brain.Response.Models;

namespace ITfamily.Utils.Brain.Response
{
    public class CommentsResponse : BaseBrainResponse
    {
        public new HelperComments result { get; set; }

        public class HelperComments
        {
            public List<CommentsModel> list { get; set; }
            public Int32 count { get; set; }
        }
    }
}