using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class OrderGoodsForClientsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private DeliveryType DeliveryType { get; set; }
        private PaymentMethod PaymentMethod { get; set; }
        private String Adress { get; set; }
        private String Comment { get; set; }
        private String Email { get; set; }
        private String Phone { get; set; }
        private String FIO { get; set; }

        public OrderGoodsForClientsOperation(string tokenHash, DeliveryType deliveryType, PaymentMethod paymentMethod, string adress, string comment, string email, string phone, string fio)
        {
            TokenHash = tokenHash;
            DeliveryType = deliveryType;
            PaymentMethod = paymentMethod;
            Adress = adress;
            Comment = comment;
            Email = email;
            Phone = phone;
            FIO = fio;
            RussianName = "Заказ товара (клиент)";
        }

        protected override void InTransaction()
        {
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");

            OrderComes order = null;
            order = Context.OrderComes.Include("OrdersItems").FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);

            if (order == null)
                throw new ObjectNotFoundException("Заказ не найден");

            if (order.OrdersItems == null || order.OrdersItems.Count == 0)
                throw new ObjectNotFoundException("Отсуствуют товары в заказе");

            Decimal amount = 0;
            var courseCash = CommonAccess.GetOurCourseCash(Context);

            foreach (var ordersItem in order.OrdersItems)
            {
                var priceUah = courseCash * ordersItem.SoldPrice;
                if (Math.Abs(priceUah - ordersItem.SoldPriceUah) > 0)
                    ordersItem.SoldPriceUah = priceUah;
            }
            amount = order.OrdersItems.Where(ordersItem => !ordersItem.Deleted).Sum(ordersItem => ordersItem.quantity * ordersItem.SoldPriceUah);
            var quantity = order.OrdersItems.Where(ordersItem => !ordersItem.Deleted).Sum(ordersItem => ordersItem.quantity);
            order.Amount = amount;//подсчитана относительно CurrencyType.
            order.Quantity = quantity;
            order.OrderType = OrderType.NewOrder;//заказ подготовлен к оформлению
            order.ShipingDate = DateTime.Now;
            order.PointOfDelivery = ConstV.DeliveryTypes[DeliveryType]; //доставка
            order.Currency = CurrencyType.UAH;
            order.Comment = Comment;
            order.Adress = Adress;
            order.FIO = FIO;
            order.Email = Email;
            order.Phone = Phone;
            order.PaymentMethod = PaymentMethod;
            //if (PaymentMethod == PaymentMethod.PaymentOnDelivery)
            order.PaymentStatus = PaymentStatus.NotPayment;
            Context.SaveChanges();
        }
    }
}