using System;
using System.Linq;
using ITfamily.Utils.DataBase.Filters;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Properties
{
    public class AddOnePropertyForStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public PropertyValueModel PropertyValueModel { get; set; }

        public AddOnePropertyForStockProductOperation(string tokenHash, PropertyValueModel propertyValueModel)
        {
            TokenHash = tokenHash;
            PropertyValueModel = propertyValueModel;
            RussianName = "Добавление значений свойств для товара(Характеристики)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var stockPr = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.Id == PropertyValueModel.StockProductId);
            if(stockPr == null)
                throw new ObjectNotFoundException("Обьект данного товара не найден StockProduct.Id = " + PropertyValueModel.StockProductId);
            var prop = Context.ProductProperties.FirstOrDefault(x => !x.Deleted && x.Id == PropertyValueModel.ProductPropertyId);
            if(prop == null)
                throw new ObjectNotFoundException("Обьект данного свойства не найден ProductProperty.Id = " + PropertyValueModel.ProductPropertyId);

            var propV =
                Context.ProductPropertyValueses.FirstOrDefault(
                    x =>
                        !x.Deleted && x.Value == PropertyValueModel.Value &&
                        x.PropertyId == PropertyValueModel.ProductPropertyId &&
                        x.StockProductId == PropertyValueModel.StockProductId);
            if(propV != null)
                throw new ExistsObjectException("Такое значение с этим же названием свойства для данного товара уже присутствует. Пожалуйста укажите другое значение.");

            Context.ProductPropertyValueses.Add(new ProductPropertyValues
            {
                PropertyId = PropertyValueModel.ProductPropertyId,
                Value = PropertyValueModel.Value,
                StockProductId = PropertyValueModel.StockProductId,
            });
            Context.SaveChanges();
        }
    }
}