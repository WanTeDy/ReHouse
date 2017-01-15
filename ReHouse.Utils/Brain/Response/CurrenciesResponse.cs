using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryModels;

namespace ITfamily.Utils.Brain.Response
{
    public class CurrenciesResponse : BaseBrainResponse
    {
        public Int32 status { get; set; }
        public new List<Currencies> result { get; set; }
    }
}