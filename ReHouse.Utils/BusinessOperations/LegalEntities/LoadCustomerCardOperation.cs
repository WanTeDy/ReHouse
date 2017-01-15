using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class LoadCustomerCardOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 PartnerId { get; set; }
        public List<CustomerCardModel> CustomerCards { get; set; }

        public LoadCustomerCardOperation(string tokenHash, int partnerId)
        {
            TokenHash = tokenHash;
            PartnerId = partnerId;
            RussianName = "Загрузка платежной(кредитной) информации партнера";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var card = Context.CustomerCards.Where(x => !x.Deleted && x.ContractorId == PartnerId).Include("OrderComes").Include("ComesMoney").ToList();

            var custCards = new List<CustomerCardModel>();
            foreach (var x in card)
            {
                var custCard = new CustomerCardModel
                {
                    Id = x.Id,
                    Balance = x.Balance,
                    OrderComes = x.OrderComes != null ? new List<OrderComes>
                    { 
                        new OrderComes
                        {
                            Comment = x.OrderComes.Comment,
                            Id = x.OrderComes.Id,
                            OrderType = x.OrderComes.OrderType,
                            PaymentStatus = x.OrderComes.PaymentStatus,
                            Adress = x.OrderComes.Adress,
                            Currency = x.OrderComes.Currency,
                            DeliveryDate = x.OrderComes.DeliveryDate,
                            IsExistsInOtherStock = x.OrderComes.IsExistsInOtherStock,
                            PointOfDelivery = x.OrderComes.PointOfDelivery,
                            Quantity = x.OrderComes.Quantity,
                            ShipingDate = x.OrderComes.ShipingDate,
                            Amount = x.OrderComes.Amount,
                            OrdersItems = x.OrderComes.OrdersItems.Select(d=>new OrderItem
                            {
                                Id = d.Id,
                                ProductName = d.ProductName,
                                PurchasePrice = d.PurchasePrice,
                                SoldPrice = d.SoldPrice,
                                SoldPriceUah = d.SoldPriceUah,
                                productID = d.productID,
                                articul = d.articul,
                                product_code = d.product_code,
                                quantity = d.quantity
                            }).ToList(),
                        }
                    } : null,
                    DateTime = x.DateTime,
                    Notes = x.Notes,
                    Outgo = x.Outgo,
                    ComesUsd = x.ComesUsd,
                    ComesMoney = x.ComesMoney != null ? new List<ComesMoney>{ new ComesMoney
                    {
                        Id = x.ComesMoney.Id,
                        DetailsOfPayment = x.ComesMoney.DetailsOfPayment,
                        DateTime = x.ComesMoney.DateTime,
                        Amount = x.ComesMoney.Amount,
                        CurrencyType = x.ComesMoney.CurrencyType,
                    }} : null,
                    ContractorId = PartnerId,
                };
                custCards.Add(custCard);
            }
            CustomerCards = custCards;
        }
    }
}