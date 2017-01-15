using System;
using System.Linq;
using ITfamily.Utils.Except;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.OurStock.UnitOfCommodity
{
    public class UnitOfCommodityReservationOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 UnitOfCommodityId { get; set; }
        public Int32 OrderComesId { get; set; }
        public Int32 Quantity { get; set; }
        public UnitOfCommodityReservationOperation(string tokenHash, int unitOfCommodityId, int orderComesId, int quantity)
        {
            TokenHash = tokenHash;
            UnitOfCommodityId = unitOfCommodityId;
            OrderComesId = orderComesId;
            if (quantity < 0)
                throw new ItFamilyException("Кол-во не может быть отрицательным");
            Quantity = quantity;
            RussianName = "Резервирование ед. товара на складе под опр. заказ";
        }

        protected override void InTransaction()
        {
            var unit = Context.UnitOfCommodities.FirstOrDefault(x => x.Id == UnitOfCommodityId && !x.Deleted);
            if (unit == null)
                throw new ObjectNotFoundException("Обьект UnitOfCommodity не найдет, Id = " + UnitOfCommodityId);
            var resUnit = Context.ReservedUnits.FirstOrDefault(x => x.UnitOfCommodityId == UnitOfCommodityId 
                && x.OrderComesId == OrderComesId && !x.Deleted);
            if (resUnit == null)
            {
                var orderComes = Context.OrderComes.FirstOrDefault(x => x.Id == OrderComesId && !x.Deleted);
                if (orderComes == null)
                    throw new ObjectNotFoundException("Заказ не найден, Id = " + OrderComesId);                
                var item = orderComes.OrdersItems.FirstOrDefault(x => x.productID == unit.StockProductId && !x.Deleted);
                if (item == null)
                    throw new ObjectNotFoundException("Товар не найдет, Id = " + unit.StockProductId);
                if (Quantity > item.quantity)
                    throw new ItFamilyException("Нельзя зарезервировать больше, чем необходимо");
                else if (Quantity == 0)
                    throw new ItFamilyException("Кол-во должно быть больше 0");
                else
                {
                    int quantityInStock = unit.Quantity - Quantity;
                    if (quantityInStock < 0)
                        throw new ItFamilyException("Нельзя зарезервировать больше, чем имеется на складе");                    
                    unit.ReservedQuantity += Quantity;
                }
                Context.ReservedUnits.Add(new ReservedUnit()
                {
                    Quantity = Quantity,
                    OrderComesId = OrderComesId,
                    UnitOfCommodityId = UnitOfCommodityId,
                    NeededQuantity = item.quantity,
                });
            }
            else
            {
                if (Quantity > resUnit.NeededQuantity)
                    throw new ItFamilyException("Нельзя зарезервировать больше, чем необходимо");                 
                else if (Quantity == 0)
                {
                    unit.ReservedQuantity -= resUnit.Quantity;                    
                    resUnit.Quantity = Quantity;
                    resUnit.Deleted = true;
                }
                else
                {
                    int quantityInStock = unit.Quantity + resUnit.Quantity - Quantity;
                    if (quantityInStock < 0)
                        throw new ItFamilyException("Нельзя зарезервировать больше, чем имеется на складе");                    
                    unit.ReservedQuantity = unit.ReservedQuantity - resUnit.Quantity + Quantity;
                    resUnit.Quantity = Quantity;
                }
            }
            Context.SaveChanges();
        }
    }
}