using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class LoadStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 SelectedStockProductId { get; set; }
        //public StockProductModel StockProductModel { get; set; }
        
        public DataBase.OurStocks.StockProduct StockProduct { get; set; }
        public String CategoryName { get; set; }
        public String VendorName { get; set; }
        public LoadStockProductOperation(string tokenHash, int selectedStockProductId)
        {
            TokenHash = tokenHash;
            SelectedStockProductId = selectedStockProductId;
            RussianName = "Загрузка конкретного товара со своего склада по Id";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var y = Context.StockProducts.Include("AdditionalData").Include("AdditionalData.PathImageses").FirstOrDefault(x => !x.Deleted && x.Id == SelectedStockProductId);
            if (y == null)
                throw new ObjectNotFoundException("Товар не найден");

            var st = new DataBase.OurStocks.StockProduct
            {
                Id = y.Id,
                Articul = y.Articul,
                BriefDescription = y.BriefDescription,
                CounterVisit = y.CounterVisit,
                FromWhatProvider = y.FromWhatProvider,
                IsAvailable = y.IsAvailable,
                ItFamilyCategoryId = y.ItFamilyCategoryId,
                ItFamilyVendorId = y.ItFamilyVendorId,
                MainImage = y.MainImage,
                Name = y.Name,
                Notes = y.Notes,
                Warranty = y.Warranty,
                Price = y.Price,
                PriceUah = y.PriceUah,
                ProductId = y.ProductId,
                AdditionalData = y.AdditionalData != null ? new AdditionalStockProductData
                {
                    Id = y.AdditionalData.Id,
                    DateAdded = y.AdditionalData.DateAdded,
                    DateModified = y.AdditionalData.DateModified,
                    ProductCode = y.AdditionalData.ProductCode,
                    Description = y.AdditionalData.Description,
                    Volume = y.AdditionalData.Volume,
                    PathImageses = y.AdditionalData.PathImageses != null ? y.AdditionalData.PathImageses.Where(p=>!p.Deleted).Select(x=>new PathImages
                    {
                        Id = x.Id,
                        AdditionalDataId = x.AdditionalDataId,
                        BigImage = x.BigImage,
                        SmallImage = x.SmallImage,
                    }).ToList() : null
                } : null,
                Available = y.IsAvailable ? "В наличии" : "Недоступен",
                IsPriceForOneProduct = y.IsPriceForOneProduct,
                PriceUsdForClients = y.PriceUsdForClients,
                PriceUsdForManager = y.PriceUsdForManager,
                PriceUsdForPartner = y.PriceUsdForPartner,
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