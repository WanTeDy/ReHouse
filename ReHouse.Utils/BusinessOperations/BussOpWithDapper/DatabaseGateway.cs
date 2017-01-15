using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper.ForDbTypes;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Filters;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.BussOpWithDapper
{
    public class DatabaseGateway : IDisposable
    {
        private SqlTransaction _transaction;
        private readonly SqlConnection _connection;
        private String _connectionString;

        public DatabaseGateway(string connectionStirng = null)
        {
            _connectionString = connectionStirng;
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
        }

        private String ConnectionString
        {
            get
            {
                if (!String.IsNullOrEmpty(_connectionString))
                    return ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString;

                return ConfigurationManager.ConnectionStrings["DbBrain"].ConnectionString;
            }
        }

        public void Dispose()
        {
            try
            {
                _connection.Close();
                _connection.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }

        
        //[GetProductModelFromListStockId]
        public IEnumerable<BrainProductModel> GetProductModelFromListStockId(SqlMapper.ICustomQueryParameter prop)
        {
            return _connection.Query<BrainProductModel>("GetProductModelFromListStockId",
                new
                {
                    @StId = prop
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }
        public IEnumerable<Properties> GetUnicProdPropertiesFromBrainCategoryId(int categoryId)
        {
            return _connection.Query<Properties>("selectUnicPropFromBrainProduct",
                new
                {
                    BrainCategoryId = categoryId
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }
        public IEnumerable<dynamic> SetUnicProdPropertiesWithItfamilyId(SqlMapper.ICustomQueryParameter prop)
        {
            return _connection.Query("insertProp",
                new
                {
                    @Prop = prop
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        public IEnumerable<int> SetPropValues(SqlMapper.ICustomQueryParameter prop)
        {
            return _connection.Query<int>("insertPropValues",
                new
                {
                    @PropVal = prop
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        //GetAllPropValuesForStock
        public IEnumerable<ProductPropertyValues> GetAllPropValuesForStock()
        {
            return _connection.Query<ProductPropertyValues>("GetAllPropValuesForStock",
                null, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }
        //GetItFamilyCategories
        public IEnumerable<Categories> GetItFamilyCategories()
        {
            return _connection.Query<Categories>("GetItFamilyCategories",
                null, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Получение всех категорий у которых есть unitOfCommodity(даже если это он др. поставщиков), и тех в которые мы добавляли свой товар
        /// </summary>
        /// <param name="stockFromWhatProvider">Указать ч/з enum свой склад</param>
        /// <returns></returns>
        public IEnumerable<ItFamilyCategory> GetItFamilyCategoriesWithParams(int stockFromWhatProvider)
        {
            return _connection.Query<ItFamilyCategory>("dbo.GetItfamilyCategoriesForOurStock",
                new
                {
                    @StockProductFromWhatProvider = stockFromWhatProvider,
                }, transaction: _transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Получение всех категорий у которых есть unitOfCommodity
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<ItFamilyCategory> GetItFamilyCategoriesWithUnitsForoOurStock(int ourStockRoomId)
        {
            return _connection.Query<ItFamilyCategory>("dbo.GetItfamilyCategoriesForOurStockNew",
                new
                {
                    @OurStockRoomId = ourStockRoomId,
                },
                transaction: _transaction, commandType: CommandType.StoredProcedure);
        }
        //[GetProdModFromStockIdsCursor]
        //[[insertBrainProductSpecification]]
        public IEnumerable<BrainProductModel> InsertBrainProductSpecification(SqlMapper.ICustomQueryParameter prop, int productId, decimal priceUsd, decimal priceUah, decimal recommendablePrice, string description, string briefDescription, DateTime? dateTimeModified)
        {
            return _connection.Query<BrainProductModel>("insertBrainProductSpecification",
                new
                {
                    @ProductId = productId,
                    @RecommendablePrice = recommendablePrice,
                    @PriceUsd = priceUsd,
                    @PriceUah = priceUah,
                    @Description = description,
                    @BriefDescription = briefDescription,
                    @DateTimeModified = dateTimeModified,
                    @Spec = prop
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        /// <summary>
        /// Получает все товары от категории (до 3ур. категории), которые доступны и не удаленны, сортирует по вставлемому полю и есть pagination
        /// </summary>
        /// <param name="categoryId">Id от ItfamilyCategory</param>
        /// <param name="skip">сколько товаров пропустить</param>
        /// <param name="take">сколько товаров получить</param>
        /// <param name="orderbyColumnName">Название колонки брать из StockProduct</param>
        /// <param name="orderByDirection">может быть ASC или DESC</param>
        /// <param name="whereClause">может оставаться пустым, если нужно указать дополнительные условия, писать в след. виде (без "")
        /// AND st."Название колоки" = условие</param>
        /// <returns>возращает сначала List<StockProduct> и count</returns>
        public SqlMapper.GridReader GetStockProductsFromCategory(int categoryId, int skip, int take, string orderbyColumnName, string orderByDirection, string whereClause = "")
        {
            return _connection.QueryMultiple("dbo.GetStockProductsFromCategory",
                new
                {
                    @CategoryId = categoryId,
                    @Offset = skip,
                    @Take = take,
                    @OrderByColumn = orderbyColumnName,
                    @OrderByDirection = orderByDirection,
                    @WhereClause = whereClause,
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 15);
        }

        public IEnumerable<dynamic> SetDeletedBrainProducts(SqlMapper.ICustomQueryParameter prop)
        {
            return _connection.Query("dbo.setDeletedBrainProducts",
                new
                {
                    @ProductsId = prop
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        /// <summary>
        /// Получение всех товаров через QueryMultiple, при большом кол-ве UnitOfCommodity эта ф-ция быстрее
        /// Получает все свои товары(вместе с Units) и товары от поставщиков у которых есть Units (те которые попали к нам на склад)
        /// </summary>
        /// <param name="categoryId">Id от ItfamilyCategory</param>
        /// <param name="fromWhatProvider">Указать ч/з enum свой склад</param>
        /// <param name="stockRooms">Необходимо передать список всех своих складов для формирования в Units названий складов</param>
        /// <returns>возращает сформированый список StockProduct</returns>
        public List<StockProduct> GetStockProductsWithUnitOfCommodityQueryMultipleV2(int categoryId, int fromWhatProvider, Dictionary<int, string> stockRooms)
        {
            var con = _connection.QueryMultiple("dbo.GetStockProductsWithUnitOfCommodityQueryMultipleV2",
                new
                {
                    @CategoryId = categoryId,
                    @FromWhatProvider = fromWhatProvider,
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 35);

            var stocks = con.Read<StockProduct>().ToList();//TODO может быть потребуется исп. distinct
            var units = con.Read<UnitOfCommodity>().ToList();
            var reserved = con.Read<ReservedUnit>().ToList();
            foreach (var stockProduct in stocks)
            {
                stockProduct.Available = stockProduct.IsAvailable ? "В наличии" : "Отсутствует";
                stockProduct.UnitOfCommodities = units.Where(x => x.StockProductId == stockProduct.Id).ToList();
                stockProduct.FromWhatProviderString = ConstV.FromWhatProviderToString[stockProduct.FromWhatProvider];
                foreach (var unitOfCommodity in stockProduct.UnitOfCommodities)
                {
                    unitOfCommodity.OurStockRoomName = stockRooms[unitOfCommodity.OurStockRoomId];
                    unitOfCommodity.ProductStatusInStockString = ConstV.ProductStatusInStocks[unitOfCommodity.ProductStatusInStock];
                    unitOfCommodity.ReservedUnits = reserved.Where(x => x.UnitOfCommodityId == unitOfCommodity.Id).ToList();
                }
            }

            return stocks;
        }

        /// <summary>
        /// Получает Id PathImage & AdditionalId которые нужно присвоить этим PathImages по Id'шникам ( это новые уже присвоены для 1 поставщика картинки, но еще не присвоены для StockProduct )
        /// </summary>
        /// <returns>колекцию которой нужно поприсваивать данные</returns>
        public IEnumerable<ForPathImagesStockProducts> GetNewPathImagesWhereAdditionalIdNull()
        {
            return _connection.Query<ForPathImagesStockProducts>("dbo.GetNewPathImagesWhereAdditionalIdNull",
                null, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        public IEnumerable<dynamic> ImportProductToOurStock()
        {
            return _connection.Query("dbo.importProductToOurStock",
                null, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        public IEnumerable<StockProduct> GetStockProductFromListStockId(SqlMapper.ICustomQueryParameter articulAndProductId)
        {
            return _connection.Query<StockProduct>("dbo.GetStockProductFromListStockId",
                new
                {
                    @StId = articulAndProductId
                }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        #region CommitedFunctionUnUsed
        //GetBrainProductModel
        //// <summary>
        //// 
        //// </summary>
        //// <param name="stockProductId">StockProduct.Id</param>
        //// <returns></returns>
        //public IEnumerable<BrainProductModel> GetBrainProductModel(int stockProductId)
        //{
        //    return _connection.Query<BrainProductModel>("GetBrainProductModel",
        //        new
        //        {
        //            @StockId = stockProductId
        //        }, transaction: _transaction, commandType: CommandType.StoredProcedure);
        //}
        //[GetPropValuesFromItfamilyCategoryId]
        //public IEnumerable<ProductPropertyValues> GetPropValuesFromItfamilyCategoryId(int itfamilyCategoryId)
        //{
        //    return _connection.Query<ProductPropertyValues>("GetPropValuesFromItfamilyCategoryId",
        //        new
        //        {
        //            @ItfamilyCategoryId = itfamilyCategoryId
        //        }, transaction: _transaction, commandType: CommandType.StoredProcedure);
        //}
        //
        //public IEnumerable<dynamic> InserDateImport(DateTime date)
        //{
        //    return _connection.Query("insertNewDate",
        //        new
        //        {
        //            @dateImp = date
        //        }, transaction: _transaction, commandType: CommandType.StoredProcedure);
        //}
        //public List<StockProduct> GetStockProductsForOurStockWithUnitOfCommodity(int categoryId, string takeComlumn, string whereClause, Dictionary<int, string> ourStockRoom)
        //{
        //    var lookup = new Dictionary<int, StockProduct>();
        //    var stProd =  _connection.Query<StockProduct, UnitOfCommodity, StockProduct>("dbo.GetStockProductsForOurStockWithUnitOfCommodity",
        //    (st, units) => {
        //                        StockProduct stockProduct;
        //                        if (!lookup.TryGetValue(st.Id, out stockProduct))
        //                        {
        //                            st.Available = st.IsAvailable ? "В наличии" : "Отсутствует";
        //                            lookup.Add(st.Id, stockProduct = st);
        //                        }
        //                        if (stockProduct.UnitOfCommodities == null && units != null)
        //                            stockProduct.UnitOfCommodities = new List<UnitOfCommodity>();
        //                       if (units == null) return stockProduct;
        //                       units.OurStockRoomName = ourStockRoom[units.OurStockRoomId];
        //                       units.ProductStatusInStockString = ConstV.ProductStatusInStocks[units.ProductStatusInStock];
        //                       stockProduct.UnitOfCommodities.Add(units); /* Add locations to course */
        //                       return stockProduct;
        //                    },
        //        param: new
        //        {
        //            @CategoryId = categoryId,
        //            @TakeColumn = takeComlumn,
        //            @WhereClause = whereClause
        //        }, 
        //        transaction: _transaction, 
        //        commandType: CommandType.StoredProcedure, 
        //        commandTimeout: 45);
        //    return lookup.Select(x => x.Value).ToList();
        //}


        //// <summary>
        //// Получение всех товаров через QueryMultiple, при большом кол-ве UnitOfCommodity эта ф-ция быстрее
        //// </summary>
        //// <param name="categoryId">Id от ItfamilyCategory</param>
        //// <param name="takeColumnForStockProd"> st.* </param>
        //// <param name="takeColumnForUnitOfCommodity"> un.* </param>
        //// <param name="whereClauseForStockProduct">AND st.FromWhatProvider = 0</param>
        //// <param name="whereClauseForUnitOfCommodity">AND un.OurStockRoomId = 1</param>
        //// <returns>возращает сформированый список StockProduct</returns>
        //public List<StockProduct> GetStockProductsWithUnitOfCommodityQueryMultiple(int categoryId, string takeColumnForStockProd, string takeColumnForUnitOfCommodity, string whereClauseForStockProduct, string whereClauseForUnitOfCommodity)
        //{
        //    var con = _connection.QueryMultiple("dbo.GetStockProductsWithUnitOfCommodityQueryMultiple",
        //        new
        //        {
        //            @CategoryId = categoryId,
        //            @TakeColumnForStockProd = takeColumnForStockProd,
        //            @TakeColumnForUnitOfCommodity = takeColumnForUnitOfCommodity,
        //            @WhereClauseForStockProduct = whereClauseForStockProduct,
        //            @WhereClauseForUnitOfCommodity = whereClauseForUnitOfCommodity,
        //        }, transaction: _transaction, commandType: CommandType.StoredProcedure, commandTimeout: 15);
        //
        //    var stocks = con.Read<StockProduct>().ToList();
        //    var units = con.Read<UnitOfCommodity>().ToList();
        //    foreach (var stockProduct in stocks)
        //    {
        //        stockProduct.UnitOfCommodities = units.Where(x => x.StockProductId == stockProduct.Id).ToList();
        //    }
        //    return stocks;
        //}
        #endregion
    }
}