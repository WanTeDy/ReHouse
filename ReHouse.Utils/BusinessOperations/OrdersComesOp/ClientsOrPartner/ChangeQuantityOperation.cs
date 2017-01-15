using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class ChangeQuantityOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 ProductId { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal AmountUah { get; set; }
        public Decimal AmountUsd { get; set; }
        public ChangeQuantityOperation(string tokenHash, int productId, int quantity)
        {
            TokenHash = tokenHash;
            ProductId = productId;
            Quantity = quantity;
            RussianName = "Изменение кол-ва товаров в корзине (клиент, партнер)";
        }
        
        protected override void InTransaction()
        {
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");


            OrderComes order = null;
            order = Context.OrderComes.FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);

            if (order != null && order.OrdersItems != null)
            {
                var prod = order.OrdersItems.FirstOrDefault(x => x.productID == ProductId);
                if (prod != null)
                {
                    prod.quantity = Quantity;
                    Context.SaveChanges();
                }
                Quantity = order.OrdersItems.Where(x=>!x.Deleted).Sum(x => x.quantity);
                AmountUah = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPriceUah * y.quantity);
                AmountUsd = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPrice * y.quantity);
            }
        }
    }
}