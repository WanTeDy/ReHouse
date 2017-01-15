using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class LoadStockProductByProductIdOperation : BaseOperation
    {
         private String TokenHash { get; set; }
        private Int32 SelectedStockProductId { get; set; }
        //public StockProductModel StockProductModel { get; set; }
        
        public DataBase.OurStocks.StockProduct StockProduct { get; set; }
        public String CategoryName { get; set; }
        public String VendorName { get; set; }
        public LoadStockProductByProductIdOperation(string tokenHash, int selectedStockProductId)
        {
            TokenHash = tokenHash;
            SelectedStockProductId = selectedStockProductId;
            RussianName = "Загрузка конкретного товара со своего склада по ProductId";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            if(SelectedStockProductId <= 0)
                throw new ActionNotAllowedException("Не правильный ProductId");
            var stockProduct = Context.StockProducts.Include("AdditionalData").Include("AdditionalData.PathImageses").FirstOrDefault(x => !x.Deleted && x.ProductId == SelectedStockProductId);
            if (stockProduct == null)
                throw new ObjectNotFoundException("Товар не найден");

            var st = new DataBase.OurStocks.StockProduct
            {
                Id = stockProduct.Id,
                Articul = stockProduct.Articul,
                BriefDescription = stockProduct.BriefDescription,
                CounterVisit = stockProduct.CounterVisit,
                FromWhatProvider = stockProduct.FromWhatProvider,
                IsAvailable = stockProduct.IsAvailable,
                ItFamilyCategoryId = stockProduct.ItFamilyCategoryId,
                ItFamilyVendorId = stockProduct.ItFamilyVendorId,
                MainImage = stockProduct.MainImage,
                Name = stockProduct.Name,
                Notes = stockProduct.Notes,
                Warranty = stockProduct.Warranty,
                Price = stockProduct.Price,
                PriceUah = stockProduct.PriceUah,
                ProductId = stockProduct.ProductId,
                AdditionalData = stockProduct.AdditionalData != null ? new AdditionalStockProductData
                {
                    Id = stockProduct.AdditionalData.Id,
                    DateAdded = stockProduct.AdditionalData.DateAdded,
                    DateModified = stockProduct.AdditionalData.DateModified,
                    ProductCode = stockProduct.AdditionalData.ProductCode,
                    Description = stockProduct.AdditionalData.Description,
                    Volume = stockProduct.AdditionalData.Volume,
                    PathImageses = stockProduct.AdditionalData.PathImageses != null ? stockProduct.AdditionalData.PathImageses.Where(p=>!p.Deleted).Select(x=>new PathImages
                    {
                        Id = x.Id,
                        AdditionalDataId = x.AdditionalDataId,
                        BigImage = x.BigImage,
                        SmallImage = x.SmallImage,
                    }).ToList() : null
                } : null,
                Available = stockProduct.IsAvailable ? "В наличии" : "Недоступен",
                IsPriceForOneProduct = stockProduct.IsPriceForOneProduct,
                PriceUsdForClients = stockProduct.PriceUsdForClients,
                PriceUsdForManager = stockProduct.PriceUsdForManager,
                PriceUsdForPartner = stockProduct.PriceUsdForPartner,
            };

            StockProduct = st;
            var cat = Context.ItFamilyCategories.FirstOrDefault(x => !x.Deleted && x.Id == st.ItFamilyCategoryId);
            if (cat != null)
            {
                CategoryName = cat.Name;
            }
            var vend = Context.ItFamilyVendors.FirstOrDefault(x => !x.Deleted && x.Id == st.ItFamilyVendorId);
            if (vend != null)
            {
                VendorName = vend.Name;
            }

            //StockProductModel = OurMaps.ConvertToStockModel(y);
        }
    }
}