using System;
using System.Linq;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp.Customer
{
    public class PutOrderForProviderOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 OrderComesId { get; set; }
        private Int32 TargetId { get; set; }
        private Int32 AdressId { get; set; }
        private String Currency { get; set; }
        private String Comment { get; set; }
        public Int32 OrderId { get; set; }

        public PutOrderForProviderOperation(string tokenHash, int orderComesId, int targetId, string currency, string comment, int adressId = -1)
        {
            TokenHash = tokenHash;
            OrderComesId = orderComesId;
            TargetId = targetId;
            AdressId = adressId;
            Currency = currency;
            Comment = comment;
            RussianName = "Оформление заказа у поставщика";
        }

        protected override void InTransaction()
        {
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var role =
                Context.RoleSet.FirstOrDefault(
                    x =>
                        !x.Deleted && !String.IsNullOrEmpty(x.ProviderLogin1) &&
                        !String.IsNullOrEmpty(x.ProviderMd5Password1));
            if (role != null)
            {
                var sid = CommonAccess.GetSidToken(role.ProviderLogin1, role.ProviderMd5Password1);
                if (!String.IsNullOrEmpty(sid))
                {
                    var order = OrdersFacade.GetOrder(sid).Result;
                    if(order != null && order.Count > 0)
                    {
                        var quantity = order.Sum(x => x.quantity);
                        Decimal amount = 0;
                        amount = Currency == "USD" ? order.Sum(x => (x.price*x.quantity)) : order.Sum(x => (x.price_uah*x.quantity));
                        var orderOut = new OrderOut
                        {
                            OrderType = OrderOutType.New,
                            OrderedId = OrderComesId,
                            //PaymentStatus = PaymentStatus.CreditLine,
                            ShipingDate = DateTime.Now,
                            Currency = Currency,
                            Comment = Comment,
                            ContractorId = contr.Id,
                            OrdersItems = order,
                            Quantity = quantity,
                            Amount = amount,
                            InStock = false,
                        };
                        var targets = OrdersFacade.GetTargets(sid).Result;
                        if (targets != null && targets.result != null && targets.result.Any())
                        {
                            var targ = targets.result.FirstOrDefault(x => x.targetID == TargetId);
                            orderOut.TargetId = TargetId;
                            if (targ != null)
                                orderOut.TargetData = targ.region + " " + targ.name + targ.type;
                        }
                        var putOrder = new DataPutOrder
                        {
                            targetID = TargetId,
                            currency = Currency,
                            comment = Comment,
                        };
                        if (AdressId != -1)
                        {
                            putOrder.addressID = AdressId;
                            orderOut.AdressId = AdressId;
                            var adresses = OrdersFacade.GetAddresses(sid).Result;
                            if (adresses != null && adresses.result != null && adresses.result.Any())
                            {
                                var adr = adresses.result.FirstOrDefault(x => x.addressID == AdressId);
                                if (adr != null)
                                {
                                    orderOut.AdressData = adr.address;
                                }
                            }
                        }

                        
                        sid = CommonAccess.GetSidToken(role.ProviderLogin1, role.ProviderMd5Password1);
                        var res = OrdersFacade.PutOrder(putOrder, sid).Result;
                        if(res.status != 1)
                            throw new OrderException("Ошибка на сервере поставщика № = " + res.error_code + "\n " + ConstV.BrainErrors[res.error_code]);
                        orderOut.OrderId = res.result.orderID;//-1;
                        OrderId = res.result.orderID;
                        Context.OrderOut.Add(orderOut);
                        Context.SaveChanges();
                    }

                }
            }
        }
    }
}