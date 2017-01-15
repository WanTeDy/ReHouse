using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITfamily.Utils.DataBaseForLog;

namespace ITfamily.Utils.WebApi.Request
{
    public class LogRequest : BaseRequest
    {
        public String objects { get; set; }
        public String message { get; set; }
        public String innerException { get; set; }
        public State State { get; set; }
        public DateTime DateTime { get; set; }

    }
}
