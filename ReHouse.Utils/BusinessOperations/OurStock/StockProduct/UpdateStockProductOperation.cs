using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class UpdateStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        //private StockProductModel StockProductModel { get; set; }
        public DataBase.OurStocks.StockProduct StockProduct { get; set; }
        public Byte[] MainImageBytes { get; set; }
        public UpdateStockProductOperation(string tokenHash, DataBase.OurStocks.StockProduct stockProduct, byte[] mainImageBytes)
        {
            TokenHash = tokenHash;
            StockProduct = stockProduct;
            MainImageBytes = mainImageBytes;
            RussianName = "Изменение конкретного товара на своем складе";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var stockProd = Context.StockProducts.Include("AdditionalData").Include("UnitOfCommodities").FirstOrDefault(x => !x.Deleted && x.Id == StockProduct.Id);
            if (stockProd == null)
                throw new ObjectNotFoundException("Товар не найден");
            CheckStockProduct();

            stockProd.Articul = StockProduct.Articul;
            stockProd.BriefDescription = StockProduct.BriefDescription;
            stockProd.ItFamilyCategoryId = StockProduct.ItFamilyCategoryId;
            stockProd.ItFamilyVendorId = StockProduct.ItFamilyVendorId;
            stockProd.Name = StockProduct.Name;
            stockProd.Notes = StockProduct.Notes;
            stockProd.Price = StockProduct.Price;
            stockProd.PriceUah = StockProduct.PriceUah;
            stockProd.ProductId = StockProduct.ProductId;
            if (stockProd.AdditionalData != null)
            {
                stockProd.AdditionalData.Volume = StockProduct.AdditionalData.Volume;
                stockProd.AdditionalData.DateModified = DateTime.Now;
                stockProd.AdditionalData.Description = StockProduct.AdditionalData.Description;
            }
            else if (StockProduct.AdditionalData != null && StockProduct.AdditionalData.Id == 0)
            {
                stockProd.AdditionalData = new AdditionalStockProductData
                {
                    DateAdded = DateTime.Now,
                    Description = StockProduct.AdditionalData.Description,
                    Volume = StockProduct.AdditionalData.Volume,
                };
            }
            else if (StockProduct.AdditionalData != null && StockProduct.AdditionalData.Id != 0)
            {
                var addit = Context.AdditionalStockProductDatas.FirstOrDefault(x => x.Id == StockProduct.AdditionalData.Id);
                if (addit != null)
                {
                    addit.DateModified = DateTime.Now;
                    addit.Description = StockProduct.AdditionalData.Description;
                    addit.Volume = StockProduct.AdditionalData.Volume;
                }
            }
            stockProd.Warranty = StockProduct.Warranty;
            stockProd.IsAvailable = stockProd.UnitOfCommodities.Any(x => x.ProductStatusInStock == ProductStatusInStock.InStock);
            
            Context.SaveChanges();

            if (MainImageBytes != null && MainImageBytes.Length > 0)
            {
                var prod = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.Id == StockProduct.Id);
                if (prod != null)
                {
                    var fileName = ConstV.PathStocks + "\\" + prod.Id + "m.jpg";
                    CommonAccess.CreateDitectoryIfNotExist();
                    CommonAccess.ByteArrayToFile(fileName, MainImageBytes);
                    prod.MainImage = ConstV.UrlStocks + prod.Id + "m.jpg";
                    Context.SaveChanges();
                }
            }
        }

        private void CheckStockProduct()
        {
            if (String.IsNullOrEmpty(StockProduct.Name))
                throw new ProductExeption("Название товара отсуствует");

            DataBase.OurStocks.StockProduct prod = null;

            prod = Context.StockProducts.FirstOrDefault(x => x.Name == StockProduct.Name && x.Id != StockProduct.Id && !x.Deleted);
            if (prod != null)
                throw new ExistsObjectException("Товар с таким таким названием (" + StockProduct.Name + ") уже существует.");

            if (!String.IsNullOrEmpty(StockProduct.Articul))
            {
                prod = Context.StockProducts.FirstOrDefault(x => x.Articul == StockProduct.Articul && x.Id != StockProduct.Id && !x.Deleted);
                if (prod != null)
                    throw new ExistsObjectException("Товар с данным артикулом уже существует");
            }
            
            //prod =
            //    Context.StockProducts.FirstOrDefault(x => x.ProductCode == StockProductModel.ProductCode && x.Id != StockProductModel.Id && !x.Deleted);
            //if (prod != null)
            //    throw new ExistsObjectException("Товар с данным кодом товара уже существует");
            if (StockProduct.ProductId != 0)
            {
                prod = Context.StockProducts.FirstOrDefault(x => x.ProductId == StockProduct.ProductId && x.Id != StockProduct.Id && !x.Deleted);
                if (prod != null)
                    throw new ExistsObjectException("Товар с данным ProductId уже существует");
            }
            
        }
    }
}