using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReHouse.Utils.Helpers
{
    public class CartModel
    {
        public Int32 AdvertId { get; set; }
        public AdvertsType Type { get; set; }
        public Boolean IsAdd { get; set; }
    }
}
