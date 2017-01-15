using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryModels;

namespace ITfamily.Utils.Brain.Response
{
    public class ContactsResponse : BaseBrainResponse
    {
        public List<Contacts> result { get; set; }
    }
}