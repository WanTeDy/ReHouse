using System;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class InStockOperation
    {
        public Boolean InStock { get; set; }
        private String TokenHash { get; set; }
        private Int32 ProductId { get; set; }
        public DateTime DateTimeArrival { get; set; }
        public InStockOperation(string tokenHash, int productId)
        {
            InStock = false;
            TokenHash = tokenHash;
            ProductId = productId;
        }

        private void GetDeliveryTime()
        {
            var date = Facade.BrainCommonFacade.GetDeliveryTime(ProductId, 168, TokenHash).Result;
            if (date == null) return;
            InStock = true;
            DateTimeArrival = date.Value;
        }
        public void ExcecuteTransaction()
        {
            GetDeliveryTime();
        }
    }
}