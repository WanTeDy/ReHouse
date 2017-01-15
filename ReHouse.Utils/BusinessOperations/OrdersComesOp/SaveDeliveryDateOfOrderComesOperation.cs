using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class SaveDeliveryDateOfOrderComesOperation : BaseOperation
    {
        private DateTime DeliveryDate { get; set; }
        private Int32 SelectedOrderComesId { get; set; }
        private String TokenHash { get; set; }

        public SaveDeliveryDateOfOrderComesOperation(DateTime deliveryDate, int selectedOrderComesId, string tokenHash)
        {
            DeliveryDate = deliveryDate;
            SelectedOrderComesId = selectedOrderComesId;
            TokenHash = tokenHash;
            RussianName = "Сохранение даты доставки приходящего заказа";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var orderComes = Context.OrderComes.FirstOrDefault(x => x.Id == SelectedOrderComesId && !x.Deleted);
            if (orderComes == null)
                throw new ObjectNotFoundException("Заказ не найден");
            orderComes.DeliveryDate = DeliveryDate;

            Context.SaveChanges();
        }
    }
}