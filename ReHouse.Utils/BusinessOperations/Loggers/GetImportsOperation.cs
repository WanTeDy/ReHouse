using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBaseForLog;

namespace ITfamily.Utils.BusinessOperations.Loggers
{
    public class GetImportsOperation : BaseOperation
    {
        public List<Import> Imports { get; set; }
        protected override void InTransaction()
        {
            var imp = Context.Imports.ToList();
            if (imp != null && imp.Count > 0)
                Imports = imp.Select(x => new Import
                {
                    Id = x.Id,
                    Deleted = x.Deleted,
                    DateTime = x.DateTime,
                }).ToList();
        }
    }
}