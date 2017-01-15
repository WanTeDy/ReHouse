using System;
using System.Linq;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp.ClientsOrPartner
{
    public class AddProductToOrderOperation : BaseOperation
    {
        public DataPostOrder DataPostOrder { get; set; }
        public String TokenHash { get; set; }
        public Decimal AmountUah { get; set; }
        public Decimal AmountUsd { get; set; }
        public Int32 QuantityProducts { get; set; }

        public AddProductToOrderOperation(DataPostOrder dataPostOrder, string tokenHash)
        {
            DataPostOrder = dataPostOrder;
            TokenHash = tokenHash;
            RussianName = "Добавление товаров в корзину (клиент, партнер)";
        }

        protected override void InTransaction()
        {
            var contr = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if (contr == null)
                throw new ObjectNotFoundException("Выбраного клиента нет в базе данных");
            
            
            OrderComes order = null;
            order = Context.OrderComes.FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);
                
            if (order == null)
            {
                var ord = new OrderComes
                {
                    OrderType = OrderType.NewOrder,
                    PaymentStatus = PaymentStatus.CreditLine,
                    ContractorId = contr.Id,
                };
                Context.OrderComes.Add(ord);
                Context.SaveChanges();
                order = Context.OrderComes.FirstOrDefault(
                        x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);
                if(order == null)
                    throw new ObjectNotFoundException("Заказ не найден!");
            }
            StockProduct prod = null;
            if(DataPostOrder.productID != 0)
                prod = Context.StockProducts.FirstOrDefault(x => x.Id == DataPostOrder.productID);
            else if (!String.IsNullOrEmpty(DataPostOrder.articul))
                prod = Context.StockProducts.FirstOrDefault(x => x.Articul == DataPostOrder.articul);
            else if (!String.IsNullOrEmpty(DataPostOrder.product_code))
                prod = Context.StockProducts.Include("AdditionalData").FirstOrDefault(x => x.AdditionalData.ProductCode == DataPostOrder.product_code);
            if(prod == null)
                throw new ObjectNotFoundException("Товар не найден!");

            var rule = Context.RuleForPrices.FirstOrDefault(
                x => x.ForWhomId == contr.RoleId && !x.Deleted && x.OurCategoryId == prod.ItFamilyCategoryId && x.From <= prod.Price && prod.Price < x.To) ??
                                Context.RuleForPrices.FirstOrDefault(
                                    x => x.ForWhomId == contr.RoleId && !x.Deleted && x.Category == null && x.From <= prod.Price && prod.Price < x.To);

            Decimal courseCash = 0;
            var curs = CommonAccess.GetOurCourseCurrencies(Context); ;
            if (curs != null)
                courseCash = curs.Value;

            var orderItem = new OrderItem
            {
                ProductName = prod.Name,
                articul = prod.Articul,
                product_code = prod.AdditionalData.ProductCode,
                PurchasePrice = prod.Price,
                quantity = DataPostOrder.quantity,
                productID = prod.Id,
            };
            if (rule != null)
            {
                orderItem.SoldPrice = FormPriceUsd(prod.Price, rule);
                orderItem.SoldPriceUah = FormPriceUah(prod.Price, rule, courseCash);
            }

            if (order.OrdersItems.Any())
            {
                var item = order.OrdersItems.Where(x=>!x.Deleted).FirstOrDefault(x => x.productID == orderItem.productID);
                if (item != null)
                    item.quantity += DataPostOrder.quantity;
                else
                    order.OrdersItems.Add(orderItem);
            }
            else
                order.OrdersItems.Add(orderItem);

            //order.OrdersItems.Add(orderItem);
            Context.SaveChanges();
            order = Context.OrderComes.FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);
            if (order == null || order.OrdersItems == null || order.OrdersItems.Count(x => !x.Deleted) == 0)
            {
                QuantityProducts = 0;
                AmountUah = 0;
                AmountUsd = 0;
            }
            else
            {
                QuantityProducts = order.OrdersItems.Where(x => !x.Deleted).Sum(x=>x.quantity);
                AmountUah = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPriceUah * y.quantity);
                AmountUsd = order.OrdersItems.Where(x => !x.Deleted).Sum(y => y.SoldPrice * y.quantity);
            }
        }
        private Decimal FormPriceUsd(Decimal price, RuleForPrice ruleForPrice)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd)
                return (price + ruleForPrice.ActionRule);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent)
                return (price * ruleForPrice.ActionRule);
            else
                return -1;
        }
        private Decimal FormPriceUah(Decimal price, RuleForPrice ruleForPrice, Decimal courseCash)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd && courseCash != 0)
                return ((price + ruleForPrice.ActionRule) * courseCash);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent && courseCash != 0)
                return ((price * ruleForPrice.ActionRule) * courseCash);
            else
                return -1;
        }
    }
}