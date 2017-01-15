using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBaseForLog;

namespace ITfamily.Utils.BusinessOperations.Loggers
{
    public class GetInfoStatesOperation : BaseOperation
    {
        private DateTime? From { get; set; }
        private DateTime? To { get; set; }
        public List<InfoState> InfoStates { get; set; }

        public GetInfoStatesOperation(DateTime? @from = null, DateTime? to = null)
        {
            From = @from;
            To = to;
        }

        protected override void InTransaction()
        {
            List<InfoState> infoStates = null;
            if (From.HasValue && To.HasValue)
                infoStates =
                    Context.InfoState.Where(
                        x => From.Value.ToUniversalTime() > x.DateTime && To.Value.ToUniversalTime() < x.DateTime)
                        .ToList();
            else
                infoStates =
                    Context.InfoState.ToList();
            if (infoStates != null && infoStates.Count > 0)
                InfoStates = infoStates.Select(x => new InfoState
                {
                    Id = x.Id,
                    Message = x.Message,
                    DateTime = x.DateTime,
                    State = x.State,
                }).ToList();
        }
    }
}