using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class CheckNewOrderComesOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 NowExist { get; set; }
        public Boolean HasNew { get; set; }
        public Int32 CountPrepaidOrder { get; set; }
        public Boolean HasPrepaidOrder { get; set; }
        public CheckNewOrderComesOperation(int nowExist, string tokenHash)
        {
            NowExist = nowExist;
            TokenHash = tokenHash;
            HasNew = false;
            RussianName = "Проверка новых приходящих заказов";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var r = Context.OrderComes.Count(x => x.OrderType == OrderType.NewOrder);
            HasPrepaidOrder = r > 0;
            if (r == NowExist) return;
            HasNew = true;
            CountPrepaidOrder = r;
        }
    }
}