using System;

namespace ITfamily.Utils.Brain.Request
{
    public class BrainAuthRequest : BaseBrainRequest
    {
        public String login { get; set; }
        public String password { get; set; }
    }
}