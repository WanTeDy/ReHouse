using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryModels;

namespace ITfamily.Utils.Brain.Response
{
    public class TargetsResponse : BaseBrainResponse
    {
        public new List<Targets> result { get; set; }
    }
}