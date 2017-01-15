using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryModels;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.Brain.Response
{
    public class OrderResponse : BaseBrainResponse
    {
        public new List<OrdersItemForBrain> result { get; set; }
    }
}