using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Properties
{
    public class LoadFilledPropertiesForProductOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 StockProductId { get; set; }
        public List<PropertyValueModel> PropertyValueModels { get; set; }
        public LoadFilledPropertiesForProductOperation(string tokenHash, int stockProductId)
        {
            TokenHash = tokenHash;
            StockProductId = stockProductId;
        }

        protected override void InTransaction()
        {
            var prod = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.Id == StockProductId);
            if(prod == null)
                throw new ObjectNotFoundException("Обьект этого товара не найден. StockProductId = " + StockProductId);
            if (prod.PropertyValueses != null && prod.PropertyValueses.Any(x=>!x.Deleted))
            {
                var specifications = prod.PropertyValueses.Where(y=>!y.Deleted).Select(x => new PropertyValueModel
                {
                    StockProductId = x.StockProductId,
                    Value = x.Value,
                    ProductPropertyId = x.PropertyId,
                    PropValueId = x.Id,
                    ProductPropertyName = x.ProductProperty != null ? x.ProductProperty.PropertyName : x.PropertyId.ToString(),
                }).ToList();
                PropertyValueModels = specifications;
            }
        }
    }
}