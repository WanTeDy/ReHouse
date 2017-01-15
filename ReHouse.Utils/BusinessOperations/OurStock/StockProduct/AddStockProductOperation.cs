using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class AddStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private DataBase.OurStocks.StockProduct StockProduct { get; set; }
        public Byte[] MainImageBytes { get; set; }
        //public List<ImagesBytes> ImagesBytes { get; set; }
        
        public AddStockProductOperation(string tokenHash, DataBase.OurStocks.StockProduct stockProduct, byte[] mainImageBytes)//, List<ImagesBytes> imagesBytes)
        {
            TokenHash = tokenHash;
            StockProduct = stockProduct;
            MainImageBytes = mainImageBytes;
            //ImagesBytes = imagesBytes;
            RussianName = "Добавление товара на склад";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            CheckStockProduct();

            var stockProduct = new DataBase.OurStocks.StockProduct
            {
                Name = StockProduct.Name,
                BriefDescription = StockProduct.BriefDescription,
                Articul = StockProduct.Articul,
                ItFamilyCategoryId = StockProduct.ItFamilyCategoryId,
                ItFamilyVendorId = StockProduct.ItFamilyVendorId,
                Notes = StockProduct.Notes,
                ProductId = StockProduct.ProductId,
                Warranty = StockProduct.Warranty,
                AdditionalData = StockProduct.AdditionalData,
                Price = StockProduct.Price,
                PriceUah = StockProduct.PriceUah,
                FromWhatProvider = FromWhatProvider.OurProduct,
                IsAvailable = false,
                //IsAvailable = StockProductModel.UnitOfCommodities != null && StockProductModel.UnitOfCommodities.Any(x=>x.ProductStatusInStock == ProductStatusInStock.InStock),
                //UnitOfCommodities = StockProductModel.UnitOfCommodities,
                //PropertyValueses = StockProductModel.PropertyValueses,
            };
            Context.StockProducts.Add(stockProduct);
            Context.SaveChanges();
            var prod = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.Name == StockProduct.Name);
            if (prod != null)
            {
                if (MainImageBytes != null && MainImageBytes.Length > 0)
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
            if (StockProduct.Id != 0)
                throw new ProductExeption("Присутствует StockProduct.Id=" + StockProduct.Id);
            if (String.IsNullOrEmpty(StockProduct.Name))
                throw new ProductExeption("Название товара отсуствует");

            DataBase.OurStocks.StockProduct prod = null;

            prod = Context.StockProducts.FirstOrDefault(x => x.Name == StockProduct.Name && x.Id != StockProduct.Id && !x.Deleted);
            if (prod != null)
                throw new ExistsObjectException("Товар с таким таким названием (" + StockProduct.Name + ") уже существует.");
            
            if(!String.IsNullOrEmpty(StockProduct.Articul))
                prod = Context.StockProducts.FirstOrDefault(x => x.Articul == StockProduct.Articul);
            if (prod != null)
                throw new ExistsObjectException("Товар с данным артикулом уже существует");
            //prod =
            //    Context.StockProducts.FirstOrDefault(x => x.ProductCode == StockProductModel.ProductCode);
            //if (prod != null)
            //    throw new ExistsObjectException("Товар с данным кодом товара уже существует");
            if (StockProduct.ProductId != 0)
                prod = Context.StockProducts.FirstOrDefault(x => x.ProductId == StockProduct.ProductId);
            if (prod != null)
                throw new ExistsObjectException("Товар с данным ProductId уже существует");
        }
    }

}