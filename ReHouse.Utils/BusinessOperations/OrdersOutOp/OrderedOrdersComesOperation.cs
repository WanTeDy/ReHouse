using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp
{
    public class OrderedOrdersComesOperation : BaseOperation
    {
        protected override void InTransaction()
        {
            //var orders =
            //    Context.OrderComes.Where(x => !x.Deleted && x.OrderType == OrderType.PrepaidOrder).Include("OrdersItems").ToList();
            //if (orders != null && orders.Count > 0)
            //{
            //    var listOrderedProducts = new List<OrderItem>();
            //    foreach (var orderComese in orders)
            //    {
            //        foreach (var ordersItem in orderComese.OrdersItems)
            //        {
            //            if(!ordersItem.Deleted)
            //                listOrderedProducts.Add(new OrderItem
            //                {
            //                    ProductName = ordersItem.ProductName,
            //                    articul = ordersItem.articul,
            //                    productID = ordersItem.productID,
            //                    product_code = ordersItem.product_code,
            //                    quantity = ordersItem.quantity,
            //                    //comment = ordersItem.comment
            //                });
            //        }
            //    }
            //    var auth = AuthBrainFacade.Auth(StaticAuth.Obj.Login, StaticAuth.Obj.Password).Result;
            //    if (auth != null && auth.status == 1)
            //    {
            //        var listOrdersProd = new List<DataPostOrder>();
            //        foreach (var listOrderedProduct in listOrderedProducts)
            //        {
            //            var r = BrainCommonFacade.GetProduct(listOrderedProduct.productID, auth.result).Result;
            //            if(r != null && r.status == 1)
            //            {
            //                listOrderedProduct.price = r.result.price;
            //                listOrderedProduct.price_uah = r.result.price_uah;
            //            }
            //            listOrdersProd.Add(new DataPostOrder
            //            {
            //                productID = listOrderedProduct.productID,
            //                quantity = listOrderedProduct.quantity
            //            });
            //        }
            //        var res = OrdersFacade.PostOrder(listOrdersProd, auth.result).Result;
            //        if (res != null && res.status == 1)
            //        {
            //            //var targets = OrdersFacade.GetTargets(auth.result).Result;
            //            //if (targets != null && targets.status == 1 && targets.result.Count > 0)
            //            //{
            //            //    var dataPutOrder = new DataPutOrder
            //            //    {
            //            //        targetID = targets.result[0].targetID,
            //            //        currency = "UAH"
            //            //        //comment = textBoxComments.Text
            //            //    };
            //            //    var putOrder = OrdersFacade.PutOrder(dataPutOrder, auth.result).Result;
            //            //    if (putOrder.status == 1)
            //            //    {
            //            //        var amount = listOrderedProducts.Where(listOrderedProduct => !listOrderedProduct.Deleted).Sum(listOrderedProduct => listOrderedProduct.price_uah);
            //            //        var ord = new OrderOut
            //            //        {
            //            //            OrderType = OrderType.Ordered,
            //            //            PointOfDelivery = targets.result[0].name,
            //            //            OrdersItems = listOrderedProducts,
            //            //            ShipingDate = DateTime.Now,
            //            //            DeliveryDate = DateTime.Now,
            //            //            Currency = "UAH",
            //            //            Amount = amount
            //            //        };
            //            //        Context.OrderOut.Add(ord);
            //            //        Context.SaveChanges();
            //            //    }
            //            //}
            //            var amount = listOrderedProducts.Where(listOrderedProduct => !listOrderedProduct.Deleted).Sum(listOrderedProduct => listOrderedProduct.price_uah);
            //            var ord = new OrderOut
            //            {
            //                OrderType = OrderType.Ordered,
            //                PointOfDelivery = "На склад в г.Одессу",
            //                OrdersItems = listOrderedProducts.Select(x=>new OrdersItemForBrain
            //                {
            //                    
            //                }).ToList(),
            //                ShipingDate = DateTime.Now,
            //                DeliveryDate = DateTime.Now,
            //                Currency = "UAH",
            //                Amount = amount,
            //                //Comment = 
            //            };
            //            Context.OrderOut.Add(ord);
            //            Context.SaveChanges();
            //        }
            //    }
            //}
        }
    }
}