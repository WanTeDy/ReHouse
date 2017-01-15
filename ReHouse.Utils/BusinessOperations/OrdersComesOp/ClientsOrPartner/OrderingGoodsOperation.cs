using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    /// <summary>
    /// Оформление заказа
    /// </summary>
    public class OrderingGoodsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private DeliveryType DeliveryType { get; set; }
        private PaymentMethod PaymentMethod { get; set; }
        private String Adress { get; set; }
        private String Comment { get; set; }
        private CurrencyType CurrencyType { get; set; }
        public OrderingGoodsOperation(string tokenHash, DeliveryType deliveryType, PaymentMethod paymentMethod, CurrencyType currencyType, string comment)
        {
            TokenHash = tokenHash;
            DeliveryType = deliveryType;
            PaymentMethod = paymentMethod;
            Comment = comment;
            CurrencyType = currencyType;
            RussianName = "Заказ товара (клиент, партнер)";
        }

        public OrderingGoodsOperation(string tokenHash, DeliveryType deliveryType, PaymentMethod paymentMethod, CurrencyType currencyType, string comment, string adress) : 
            this(tokenHash, deliveryType, paymentMethod, currencyType, comment)
        {
            Adress = adress;
        }

        protected override void InTransaction()
        {
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");

            OrderComes order = null;
            order = Context.OrderComes.Include("OrdersItems").FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);

            if(order == null)
                throw new ObjectNotFoundException("Заказ не найден");

            if(order.OrdersItems == null || order.OrdersItems.Count == 0)
                throw new ObjectNotFoundException("Отсуствуют товары в заказе");

            Decimal amount = 0;
            var courseCash = CommonAccess.GetOurCourseCash(Context);

            foreach (var ordersItem in order.OrdersItems)
            {
                var priceUah = courseCash * ordersItem.SoldPrice;
                if (Math.Abs(priceUah - ordersItem.SoldPriceUah) > 0)
                    ordersItem.SoldPriceUah = priceUah;
            }
            
            if (CurrencyType == CurrencyType.UAH)
                amount = order.OrdersItems.Where(ordersItem => !ordersItem.Deleted)
                        .Sum(ordersItem => ordersItem.quantity*ordersItem.SoldPriceUah);
            else if (CurrencyType == CurrencyType.USD)
                amount = order.OrdersItems.Where(ordersItem => !ordersItem.Deleted)
                        .Sum(ordersItem => ordersItem.quantity*ordersItem.SoldPrice);
            
            var quantity = order.OrdersItems.Where(ordersItem => !ordersItem.Deleted).Sum(ordersItem => ordersItem.quantity);
            order.Amount = amount;//подсчитана относительно CurrencyType.
            order.Quantity = quantity;
            //TODO  проверить на актуальность доступности на удаленном складе
            order.OrderType = OrderType.NewOrder;//заказ подготовлен к оформлению
            order.ShipingDate = DateTime.Now;
            order.PointOfDelivery = ConstV.DeliveryTypes[DeliveryType]; //доставка
            order.Currency = CurrencyType;
            order.Comment = Comment;
            order.Adress = Adress;

            if (PaymentMethod == PaymentMethod.CreditLine || PaymentMethod == PaymentMethod.PaymentOnDelivery)
            {
                order.PaymentStatus = PaymentStatus.CreditLine;
                    var card = new CustomerCard
                    {
                        DateTime = DateTime.Now,
                        Notes = "Покупка товара",
                        OrderComes = order,
                        ContractorId = contr.Id
                    };

                    Decimal amountOrd = 0;
                    if (CurrencyType != CurrencyType.USD)
                        amountOrd = order.OrdersItems.Where(ordersItem => !ordersItem.Deleted).Sum(ordersItem => ordersItem.quantity * ordersItem.SoldPrice);
                    
                    if(contr.CustomerCards.Count>0)
                    {
                        var date = contr.CustomerCards.Max(x=>x.DateTime);
                        var cardDb = contr.CustomerCards.FirstOrDefault(x => x.DateTime == date);
                        
                        if (cardDb != null)
                        {
                            if (CurrencyType == CurrencyType.USD)
                            {
                                card.Balance = cardDb.Balance - order.Amount;
                                card.Outgo = order.Amount;
                            }
                            else
                            {
                            
                                card.Balance = cardDb.Balance - amountOrd;
                                card.Outgo = amountOrd;
                            }
                        }
                    }
                    else
                    {
                        if (CurrencyType == CurrencyType.USD)
                        {
                            card.Balance = -order.Amount;
                            card.Outgo = order.Amount;
                        }
                        else
                        {
                            card.Balance = -amountOrd;
                            card.Outgo = amountOrd;
                        }
                    }
                    contr.CustomerCards.Add(card);
            }

            Context.SaveChanges();
        }
    }
}