using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class SetOurPriceForStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 ProductId { get; set; }
        public Decimal PriceUsdForManager { get; set; }
        public Decimal PriceUsdForPartner { get; set; }
        public Decimal PriceUsdForClients { get; set; }
        public Boolean IsPriceForOneProduct { get; set; }
        public SetOurPriceForStockProductOperation(string tokenHash, int productId, bool isPriceForOneProduct, decimal priceUsdForManager, decimal priceUsdForPartner, decimal priceUsdForClients)
        {
            TokenHash = tokenHash;
            ProductId = productId;
            IsPriceForOneProduct = isPriceForOneProduct;
            PriceUsdForManager = priceUsdForManager;
            PriceUsdForPartner = priceUsdForPartner;
            PriceUsdForClients = priceUsdForClients;
            RussianName = "Установка цены для одного товара";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            if(ProductId <= 0)
                throw new ProductExeption("Не указан ProductId. ProductId = " + ProductId);
            var prod = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.ProductId == ProductId);
            if(prod == null)
                throw new ObjectNotFoundException("Товар не найде. ProductId = " + ProductId);
            
            prod.IsPriceForOneProduct = IsPriceForOneProduct;
            prod.PriceUsdForClients = PriceUsdForClients;
            prod.PriceUsdForManager = PriceUsdForManager;
            prod.PriceUsdForPartner = PriceUsdForPartner;

            Context.SaveChanges();
        }
    }
}