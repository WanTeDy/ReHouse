using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class GetAllDataBeforePartnerPutOrderOperation : BaseOperation
    {
        public String FIO { get; set; }
        public String MobPhone { get; set; }
        public String Email { get; set; }
        private String TokenHash { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public GetAllDataBeforePartnerPutOrderOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Просмотр данных перед оформлением заказа (клиент, партнер)";
        }

        protected override void InTransaction()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");

            OrderComes order = null;
            order = Context.OrderComes.Include("OrdersItems").FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);

            if (order == null)
                throw new ObjectNotFoundException("Заказ не найден");

            FIO = contr.SecondName + " " + contr.FirstName + " " + contr.FatherName;
            MobPhone = contr.Phone;
            Email = contr.Email;
            
            Decimal courseCash = 0;
            var colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && x.Name == ConstV.CourseCash && !x.Deleted);
            DataBase.Currencies.Currency cur = null;
            if (colCur.Any())
            {
                var maxDate = colCur.Max(x => x.DateTime);
                cur = colCur.FirstOrDefault(x => x.DateTime == maxDate);
            }
            //var curs = Context.CollectionCurrencies.Include("Currencies").FirstOrDefault(x => x.Name == ConstV.OurCurrencyName && !x.Deleted);
            //if (curs != null)
            //{
            //    var c = curs.Currencies.FirstOrDefault(x => !x.Deleted && x.Name == ConstV.CourseCash);
            //    if (c != null) courseCash = c.Value;
            //}
            if (cur != null)
                courseCash = cur.Value;
            foreach (var ordersItem in order.OrdersItems)
            {
                var priceUah = courseCash * ordersItem.SoldPrice;
                if (Math.Abs(priceUah - ordersItem.SoldPriceUah) > 0)
                    ordersItem.SoldPriceUah = priceUah;
            }

            OrderItems = order.OrdersItems.Where(y => !y.Deleted).Select(x => new OrderItem
            {
                Id = x.Id,
                productID = x.productID,
                SoldPrice = x.SoldPrice,
                SoldPriceUah = x.SoldPriceUah,
                articul = x.articul,
                quantity = x.quantity,
                product_code = x.product_code,
                ProductName = x.ProductName,
            }).ToList();
        }
    }
}