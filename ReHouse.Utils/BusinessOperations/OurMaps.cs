using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations
{
    public static class OurMaps
    {
        public static Contractor Copy(Contractor x)
        {
            return new Contractor
            {
                Id = x.Id,
                TokenHash = x.TokenHash,
                Deleted = x.Deleted,
                DeliveryAdditional = x.DeliveryAdditional,
                DeliveryStreet = x.DeliveryStreet,
                DeliveryAppartament = x.DeliveryAppartament,
                DeliveryCity = x.DeliveryCity,
                DeliveryHome = x.DeliveryHome,
                RoleId = x.RoleId,
                CreditLimit = x.CreditLimit,
                Email = x.Email,
                FatherName = x.FatherName,
                FirstName = x.FirstName,
                FormOfTaxation = x.FormOfTaxation,
                IsActive = x.IsActive,
                Ownership = x.Ownership,
                Password = x.Password,
                Phone = x.Phone,
                Requisite = x.Requisite,
                SecondName = x.SecondName,
                TaxRate = x.TaxRate,
                Url = x.Url,
                
            };
        }
        public static BrainCategory Copy(BrainCategory x)
        {
            return new BrainCategory
            {
                Id = x.Id,
                BrainParentID = x.BrainParentID,
                //HasRule = x.HasRule,
                categoryID = x.categoryID,
                name = x.name,
                parentID = x.parentID,
                Deleted = x.Deleted,
            };
        }
        public static AuthorityForOneRoleModel ConvertToModel(Authority x)
        {
            return new AuthorityForOneRoleModel
            {
                AuthorityId = x.Id,
                RussianNameOperation = x.RussianNameOperation,
                IsAccess = true
            };
        }
        public static RuleForPrice ConvertFromModel(RuleForPriceModel x)
        {
            return new RuleForPrice
            {
                Id = x.Id,
                To = x.To,
                From = x.From,
                ForWhomId = x.ForWhomId,
                //ForWhom = ConstV.ForWhomStringToType[x.ForWhom],
                ActionRule = x.ActionRule,
                TypeRule = ConstV.TypesRuleStringToType[x.TypeRule],
                Deleted = x.Deleted,
                OurCategoryId = x.ItFamilyCategoryId,
            };
        }
        public static RuleForPriceModel ConvertToModel(RuleForPrice x)
        {
            return new RuleForPriceModel
            {
                Id = x.Id,
                To = x.To,
                From = x.From,
                ForWhomId = x.ForWhomId,
                ForWhom = x.ForWhom.Name,
                //ConstV.ForWhoms[x.ForWhom],
                ActionRule = x.ActionRule,
                TypeRule = ConstV.TypesRule[x.TypeRule],
                Deleted = x.Deleted,
                ItFamilyCategoryId = x.OurCategoryId,
            };
        }
        public static void SetRuleForPriceFromModel(RuleForPrice ruleForPriceInDb, RuleForPriceModel ruleForPriceModel)
        {
            ruleForPriceInDb.ForWhomId = ruleForPriceModel.ForWhomId;//ConstV.ForWhomStringToType[ruleForPriceModel.ForWhom];
            ruleForPriceInDb.To = ruleForPriceModel.To;
            ruleForPriceInDb.TypeRule = ConstV.TypesRuleStringToType[ruleForPriceModel.TypeRule];
            ruleForPriceInDb.From = ruleForPriceModel.From;
            ruleForPriceInDb.ActionRule = ruleForPriceModel.ActionRule;
            ruleForPriceInDb.Deleted = ruleForPriceModel.Deleted;
            ruleForPriceInDb.OurCategoryId = ruleForPriceModel.ItFamilyCategoryId;
        }

        public static BrainProductModel ConvertToModel(BrainProduct prod, bool withImages = false)
        {
            return new BrainProductModel
            {
                //BrainCategoryID = prod.BrainCategoryID,
                ItfamilyVendorID = prod.BrainVendorID,
                articul = prod.articul,
                brief_description = prod.brief_description,
                ItfamilyCategoryID = prod.categoryID,
                //is_archive = prod.is_archive,
                //is_new = prod.is_new,
                name = prod.name,
                price = prod.price,
                price_uah = prod.price_uah,
                productID = prod.productID,
                //product_code = prod.product_code,
                //vendorID = prod.vendorID,
                //volume = prod.volume,
                warranty = prod.warranty,
                Id = prod.Id,
                //Stocks = prod.BrainStockses.Select(x=>new BrainStocks
                //{
                //    name = x.name,
                //    stockID = x.stockID
                //}).ToList(),
                IsAvailable = prod.BrainStockses.Count > 0 ? "В наличии" : "Недоступен",
                //PriceUahForClients = 0,
                //PriceUsdForPartner = 0,
                //PriceUsdForManager = 0,
                //Vendor = prod.Vendor != null ? new Vendor
                //{
                //    name = prod.Vendor.name,
                //    Id = prod.Vendor.Id,
                //    vendorID = prod.Vendor.vendorID
                //} : null,
                MediumImage = prod.MainImage,
                Images = withImages && prod.PathImageses != null && prod.PathImageses.Any() ? prod.PathImageses.Select(x=>new PathImages
                {
                    BigImage = x.BigImage,
                    Id = x.Id,
                    SmallImage = x.SmallImage,
                }).ToList() : null,
                DateTimeModified = prod.DateTimeModified
            };
        }
        public static BrainProductModel ConvertToModel(StockProduct prod, bool withImages = false)
        {
            return new BrainProductModel
            {
                //BrainCategoryID = prod.ItFamilyCategoryId,
                ItfamilyVendorID = prod.ItFamilyVendorId,
                articul = prod.Articul,
                brief_description = prod.BriefDescription,
                //categoryID = prod.C,
                //is_archive = prod.is_archive,
                //is_new = prod.is_new,
                name = prod.Name,
                price = prod.Price,
                price_uah = prod.PriceUah,
                productID = prod.ProductId,
                //product_code = prod.AdditionalData!=null ? prod.AdditionalData.ProductCode : "",
                //vendorID = prod.vendorID,
                //volume = prod.AdditionalData!=null ? prod.AdditionalData.Volume : 0,
                warranty = prod.Warranty,
                Id = prod.Id,
                //Stocks = prod.BrainStockses.Select(x=>new BrainStocks
                //{
                //    name = x.name,
                //    stockID = x.stockID
                //}).ToList(),
                IsAvailable = prod.IsAvailable ? "В наличии" : "Недоступен",
                //PriceUahForClients = 0,
                //PriceUsdForPartner = 0,
                //PriceUsdForManager = 0,
                //Vendor = prod.ItFamilyVendor != null ? new ItFamilyVendor
                //{
                //    Name = prod.ItFamilyVendor.Name,
                //    Id = prod.ItFamilyVendor.Id,
                //    VendorId = prod.ItFamilyVendor.VendorId
                //} : null,
                MediumImage = prod.MainImage,
                Images = withImages && prod.AdditionalData != null && prod.AdditionalData.PathImageses != null && prod.AdditionalData.PathImageses.Any() ? prod.AdditionalData.PathImageses.Select(x => new PathImages
                {
                    BigImage = x.BigImage,
                    Id = x.Id,
                    SmallImage = x.SmallImage,
                    AdditionalDataId = x.AdditionalDataId
                }).ToList() : null,
                DateTimeModified = prod.AdditionalData!=null ? prod.AdditionalData.DateModified : null,
                ItfamilyCategoryID = prod.ItFamilyCategoryId,
                VendorName = prod.ItFamilyVendor != null ? prod.ItFamilyVendor.Name : "",
                IsAv = prod.IsAvailable
                //vendorID = prod.ItFamilyVendorId
            };
        }
        public static BrainProductModel ConvertToModelForListStocks(StockProduct prod, bool withImages, bool withDateModif)
        {
            return new BrainProductModel
            {
                ItfamilyVendorID = prod.ItFamilyVendorId,
                articul = prod.Articul,
                brief_description = prod.BriefDescription,
                name = prod.Name,
                price = prod.Price,
                price_uah = prod.PriceUah,
                productID = prod.ProductId,
                warranty = prod.Warranty,
                Id = prod.Id,
                IsAvailable = prod.IsAvailable ? "В наличии" : "Недоступен",
                MediumImage = prod.MainImage,
                Images = withImages && prod.AdditionalData != null && prod.AdditionalData.PathImageses != null && prod.AdditionalData.PathImageses.Any() ? prod.AdditionalData.PathImageses.Select(x => new PathImages
                {
                    BigImage = x.BigImage,
                    Id = x.Id,
                    SmallImage = x.SmallImage,
                    AdditionalDataId = x.AdditionalDataId
                }).ToList() : null,
                DateTimeModified = prod.AdditionalData != null && withDateModif ? prod.AdditionalData.DateModified : null,
                ItfamilyCategoryID = prod.ItFamilyCategoryId,
                VendorName = prod.ItFamilyVendor != null ? prod.ItFamilyVendor.Name : "",
                IsAv = prod.IsAvailable
                //vendorID = prod.ItFamilyVendorId
            };
        }
        public static StockProductModel ConvertToStockModel(DataBase.OurStocks.StockProduct stockProd)
        {
            return new StockProductModel
            {
                Id = stockProd.Id,
                Name = stockProd.Name,
                ItFamilyCategoryId = stockProd.ItFamilyCategoryId,
                ItFamilyVendor = stockProd.ItFamilyVendor != null ?
                    new ItFamilyVendor
                    {
                        Name = stockProd.ItFamilyVendor.Name,
                        Id = stockProd.ItFamilyVendor.Id,
                        VendorId = stockProd.ItFamilyVendor.VendorId
                    } : null,
                ItFamilyVendorId = stockProd.ItFamilyVendorId,
                BriefDescription = stockProd.BriefDescription,
                Articul = stockProd.Articul,
                //Amount = stockProd.Amount,
                //NeedQuantity = stockProd.NeedQuantity,
                //PriceProviderUah = stockProd.PriceProviderUah,
                //PriceProviderUsd = stockProd.PriceProviderUsd,
                //ProductCode = stockProd.ProductCode,
                ProductId = stockProd.ProductId,
                //ProductStatusInStock = ConstV.ProductStatusInStocks[stockProd.ProductStatusInStock],
                //Volume = stockProd.Volume,
                Warranty = stockProd.Warranty,
                Notes = stockProd.Notes,
                //OurStockRoomId = stockProd.OurStockRoomId,
                ItFamilyCategory = stockProd.ItFamilyCategory !=null ? new ItFamilyCategory
                {
                    Id = stockProd.ItFamilyCategory.Id,
                    Name = stockProd.ItFamilyCategory.Name,
                    CategoryId = stockProd.ItFamilyCategory.CategoryId,
                } : null,

            };
        }

        public static OrderComesModel ConvertToModel(OrderComes x, StatusRole statusRole)
        {
            var model = new OrderComesModel
            {
                Id = x.Id,
                DeliveryDate = x.DeliveryDate,
                Amount = x.Amount,
                ContractorId = x.ContractorId,
                Currency = ConstV.CurrencyTypes[x.Currency],
                PointOfDelivery = x.PointOfDelivery,
                OrderType = ConstV.OrderTypes[x.OrderType],
                ShipingDate = x.ShipingDate,
                Comment = x.Comment,
                PaymentStatus = ConstV.PaymentStatuses[x.PaymentStatus],
                Quantity = x.Quantity,
                OrdersItems = x.OrdersItems.Where(h => !h.Deleted).Select(y => SetToSet(y, statusRole)).ToList(),
                ManagerId = x.ManagerId,
            };
            //if (statusRole == StatusRole.Client)
            //    model.ContractorRole = " Клиента";
            //if (statusRole == StatusRole.Administrator)
            //    model.ContractorRole = " Администратора";
            //if (statusRole == StatusRole.Partner)
            //    model.ContractorRole = " Партнера";
            //if (statusRole == StatusRole.Manager)
            //    model.ContractorRole = " Менеджера";
            return model;
        }

        public static OrderItem SetToSet(OrderItem y, StatusRole forWhom)
        {
            var res = new OrderItem
            {
                Id = y.Id,
                ProductName = y.ProductName,
                articul = y.articul,
                productID = y.productID,
                SoldPrice = y.SoldPrice,
                SoldPriceUah = y.SoldPriceUah,
                //PurchasePrice = y.PurchasePrice,
                product_code = y.product_code,
                quantity = y.quantity,
                Deleted = y.Deleted,
            };
            if (forWhom == StatusRole.Administrator || forWhom == StatusRole.Manager)
            {
                res.PurchasePrice = y.PurchasePrice;
            }
            return res;
        }
    }
}