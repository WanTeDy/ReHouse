using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBaseForLog;

namespace ITfamily.Utils.BusinessOperations.Loggers
{
    public class GetLoggersOperation : BaseOperation
    {
        private DateTime? From { get; set; }
        private DateTime? To { get; set; }
        public List<Logger> Loggers { get; set; }

        public GetLoggersOperation(DateTime? @from = null, DateTime? to = null)
        {
            From = @from;
            To = to;
        }

        protected override void InTransaction()
        {
            List<Logger> log = null;
            if (From.HasValue && To.HasValue)
                log =
                    Context.Loggers.Where(
                        x => From.Value.ToUniversalTime() > x.DateTime && To.Value.ToUniversalTime() < x.DateTime)
                        .ToList();
            else
                log =
                    Context.Loggers.ToList();
            if(log != null && log.Count>0)
                Loggers = log.Select(x => new Logger
                {
                    Id = x.Id,
                    Message = x.Message,
                    DateTime = x.DateTime,
                    InnerException = x.InnerException,
                    State = x.State
                }).ToList();
        }
    }
}