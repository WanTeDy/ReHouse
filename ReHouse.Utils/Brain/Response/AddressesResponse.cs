using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryModels;

namespace ITfamily.Utils.Brain.Response
{
    public class AddressesResponse : BaseBrainResponse
    {
        public new List<Addresses> result { get; set; } 
    }
}