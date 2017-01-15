using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.Brain.Response;
using ITfamily.Utils.Brain.Response.Models;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper.ForDbTypes;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper.Helpers;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Helpers;
using ITfamily.Utils.Logging;
using ITfamily.Utils.WebApi.Response;
using Newtonsoft.Json;

namespace ITfamily.Utils.Brain
{
    public class ImportToDbFromApi
    {
        public DbItFamily Context { get; set; }
        public List<BrainCategory> BrainCategories { get; set; }
        public BrainStocksResponse BrainStockses { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<BrainProduct> BrainProducts { get; set; }
        public String SID { get; set; }
        public ImportToDbFromApi()
        {
            Context = new DbItFamily();
            Context.Configuration.ProxyCreationEnabled = true;
            Context.Configuration.AutoDetectChangesEnabled = true;
        }

        private bool GetSID()
        {
            var r = AuthBrainFacade.Auth("KovalchukImport", GenerateHash.GetMd5Hash("19Vfrcbv19")).Result;
            if (r == null)
            {
                Logg.Error("", "Ошибка связи с поставщиком", State.ErrorOnBrainApiServer);
                Log.AddError("\nОшибка связи с поставщиком\n");
                
                return false;
            }
            if (r.status != 1)
            {
                Logg.Error(JsonConvert.SerializeObject(r), "In Import Auth exception on api.brain.com.ua", State.ErrorOnBrainApiServer);
                Log.AddError("\nIn Import Auth exception on api.brain.com.ua\n" + JsonConvert.SerializeObject(r));
                return false;
            }
            SID = r.result;
            return true;
        }
        private void GetAllCategories()
        {
            if (GetSID())
            {
                BrainCategories = BrainCommonFacade.GetCategories(SID).Result;
                if (BrainCategories != null)
                    AddCategories();
            }
        }
        private void AddCategories()
        {
            var listToAdd = new List<BrainCategory>();
            foreach (var brainCategory in BrainCategories)
            {
                var category = Context.Categories.FirstOrDefault(x => x.categoryID == brainCategory.categoryID);
                if (category == null)
                {
                    //brainCategory.HasRule = false;
                    listToAdd.Add(brainCategory);
                }
                else
                {
                    var flag = false;
                    if (category.name != null && category.name != brainCategory.name)
                    {
                        category.name = brainCategory.name;
                        flag = true;
                    }
                    foreach (var brainCategory1 in brainCategory.Categories)
                    {
                        var cat = category.Categories.FirstOrDefault(x => x.categoryID == brainCategory1.categoryID);
                        if (cat != null)
                        {
                            if (brainCategory1.name != null && brainCategory1.name != cat.name)
                            {
                                cat.name = brainCategory1.name;
                                flag = true;
                            }
                        }
                        else
                        {
                            category.Categories.Add(new BrainCategory
                            {
                                name = brainCategory1.name, 
                                categoryID = brainCategory1.categoryID,
                                parentID = brainCategory1.parentID,
                            });
                            flag = true;
                        }
                        foreach (var category1 in brainCategory1.Categories)
                        {
                            var cat2 = Context.Categories.FirstOrDefault(x => x.categoryID == category1.categoryID);
                            if (cat2 != null)
                            {
                                if (category1.name == null || category1.name == cat2.name) continue;
                                cat2.name = category1.name;
                                flag = true;
                            }
                            else
                            {
                                var catPar = Context.Categories.FirstOrDefault(x => x.categoryID == category1.parentID);
                                if (catPar == null) continue;
                                catPar.Categories.Add(new BrainCategory
                                {
                                    name = category1.name,
                                    categoryID = category1.categoryID,
                                    parentID = category1.parentID,
                                });
                                flag = true;
                            }
                        }
                    }
                    
                    if (flag)
                    {
                        Context.SaveChanges();
                        Context.Dispose();
                        Context = new DbItFamily();
                    }
                }
            }
            if (listToAdd.Count > 0)
                Context.Categories.AddRange(listToAdd);

            Context.SaveChanges();
            Context.Dispose();
            Context = new DbItFamily();
        }
        private void GetAllStocks()
        {
            if (GetSID())
            {
                BrainStockses = BrainCommonFacade.GetStocks(SID).Result;
                if (BrainStockses != null && BrainStockses.status == 1)
                    AddStocks();
            }
        }

        private void AddStocks()
        {
            var listToAdd = new List<BrainStocks>();
            foreach (var brainStock in BrainStockses.result)
            {
                var stock = Context.BrainStockses.FirstOrDefault(x => x.stockID == brainStock.stockID);
                if (stock == null)
                    listToAdd.Add(brainStock);
                else
                {
                    if (stock.name == null || stock.name == brainStock.name) continue;
                    stock.name = brainStock.name;
                    Context.SaveChanges();
                    Context.Dispose();
                    Context = new DbItFamily();
                }
            }
            if (listToAdd.Count > 0)
                Context.BrainStockses.AddRange(listToAdd);

            Context.SaveChanges();
            Context.Dispose();
            Context = new DbItFamily();
        }

        private void GetAllVendors()
        {
            if (GetSID())
            {
                Vendors = BrainCommonFacade.GetVendors(SID).Result;
                if (Vendors != null)
                {
                    AddVendors();
                    Logg.InfoMessage("Vendors finished download and add To DbBrain: " + DateTime.Now.ToString());
                    Log.AddInfo("Vendors finished download and add To DbBrain: " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    AddCategoriesToVendors();
                    Logg.InfoMessage("Add Categories To Vendors finished (DbBrain): " + DateTime.Now.ToString());
                    Log.AddInfo("Add Categories To Vendors finished (DbBrain): " + DateTime.Now.ToString());
                }
            }
        }

        private void AddCategoriesToVendors()
        {
            var unicVendors = Context.Vendors.Include("BrainCategories").ToList();
            foreach (var unicVendor in unicVendors)
            {
                Vendor vendorUn = unicVendor;
                var allVendors = Vendors.Where(x => x.vendorID == vendorUn.vendorID).ToList();
                foreach (var vendor in allVendors)
                {
                    var category = Context.Categories.FirstOrDefault(x => x.categoryID == vendor.categoryID);
                    var existCategory = unicVendor.BrainCategories.FirstOrDefault(x => x.categoryID == vendor.categoryID);
                    if (category != null && existCategory == null)
                    {
                        unicVendor.BrainCategories.Add(category);
                    }
                }
            }
            var otherVendor = Context.Vendors.FirstOrDefault(x => x.name == "Other");
            if (otherVendor == null)
            {
                otherVendor = new Vendor { name = "Other", categoryID = -1, vendorID = -1 };
                Context.Vendors.Add(otherVendor);
                Context.SaveChanges();
                otherVendor = Context.Vendors.FirstOrDefault(x => x.name == "Other");
                if (otherVendor != null)
                {
                    var cat = Context.Categories.Include("Vendors").ToList();
                    foreach (var brainCategory in cat)
                        brainCategory.Vendors.Add(otherVendor);
                }
            }
            
            Context.SaveChanges();
            Context.Dispose();
            Context = new DbItFamily();
        }
        private void AddVendors()
        {
            var unicNames = new List<VendorModel>();
            foreach (var vendor in Vendors)
            {
                var exists = unicNames.FirstOrDefault(x => x.name == vendor.name && x.vendorID == vendor.vendorID);
                if (exists == null)
                    unicNames.Add(new VendorModel { name = vendor.name, vendorID = vendor.vendorID });
            }

            var vendorToDb = unicNames.Select(x => new Vendor { name = x.name, vendorID = x.vendorID }).ToList();

            var listToAdd = new List<Vendor>();
            foreach (var vendor in vendorToDb)
            {
                var vd = Context.Vendors.FirstOrDefault(x => x.vendorID == vendor.vendorID);
                if (vd == null)
                    listToAdd.Add(vendor);
                else
                {
                    if (vd.name != null && vd.name != vendor.name)
                    {
                        vd.name = vendor.name;
                    }
                }
            }

            if (listToAdd.Count > 0)
                Context.Vendors.AddRange(listToAdd);

            Context.SaveChanges();
            Context.Dispose();
            Context = new DbItFamily();
        }

        public void ImportCategoriesStrocksVendors()
        {
            try
            {
                DateTime = DateTime.Now;
                Logg.InfoMessage("Start Import To DbBrain: " + DateTime, "");
                Log.AddInfo("Start Import To DbBrain: " + DateTime);
                GetAllCategories();
                Logg.InfoMessage("Categories finished download and add To DbBrain: " + DateTime.Now, "");
                Log.AddInfo("Categories finished download and add To DbBrain: " + DateTime.Now);
                GetAllStocks();
                Logg.InfoMessage("Stocks finished download and add To DbBrain: " + DateTime.Now, "");
                Log.AddInfo("Stocks finished download and add To DbBrain: " + DateTime.Now);
                GetAllVendors();
                GetAndAddProducts();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Logg.InfoMessage("Exception when import data from api: " + DateTime.Now.ToUniversalTime(),
                                    JsonConvert.SerializeObject(ex));
                Log.AddError("Exception when import data from api: " + DateTime.Now.ToUniversalTime() + "\n JsonConvert.SerializeObject: " +
                    JsonConvert.SerializeObject(ex));
                Thread.Sleep(60000);
                ImportCategoriesStrocksVendors();
            }
        }

        private void GetAndAddProducts()
        {
            GetAllProducts();
            AddProductsToDb();
        }

        public DateTime DateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime StartDateTimeAdd { get; set; }
        public DateTime EndDateTimeAdd { get; set; }
        private void GetAllProducts()
        {
            StartDateTime = DateTime.Now;
            while (BrainProducts == null)
            {
                try
                {
                    Logg.InfoMessage("Start Download Produts and formation objects: " + StartDateTime);
                    Log.AddInfo("Start Download Produts and formation objects: " + StartDateTime);
                    if (GetSID())
                    {
                        var products = new List<BrainProduct>();
                        BrainCategories = BrainCategories.Where(x => x.parentID == 1 || x.BrainParentID == null).ToList();
                        foreach (var brainCategory in BrainCategories)
                        {
                            var res = BrainCommonFacade.GetProducts(brainCategory.categoryID, SID).Result;
                            var offset = 100;
                            var counter = 0;
                            if (res != null && res.status == 1)
                            {
                                products.AddRange(res.result.list);
                                counter = res.result.list.Count;
                            }
                            if (res != null && res.result.count > 100)
                            {
                                while (res != null && counter < res.result.count)
                                {
                                    res = BrainCommonFacade.GetProducts(brainCategory.categoryID, SID, offset).Result;
                                    if (res != null && res.status == 1)
                                    {
                                        products.AddRange(res.result.list);
                                        counter += res.result.list.Count;
                                    }
                                    else
                                    {
                                        if (GetSID())
                                        {
                                            res = BrainCommonFacade.GetProducts(brainCategory.categoryID, SID, offset).Result;
                                            if (res != null && res.status == 1)
                                            {
                                                products.AddRange(res.result.list);
                                                counter += res.result.list.Count;
                                            }
                                        }
                                    }
                                    offset += 100;
                                }
                            }
                            //if(products.Count > 5000)
                            //    break;
                        }
                        BrainProducts = products.GroupBy(x=>x.productID).Select(g=>g.First()).ToList();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.WaitForFullGCApproach();
                        GC.WaitForFullGCComplete();
                    }
                }
                catch (Exception ex)
                {
                    Logg.Error(JsonConvert.SerializeObject(ex), "Error in GetAllProducts products: " + DateTime.Now, State.Error);
                    Thread.Sleep(10000);
                    //GC.Collect();
                    GetAllProducts();
                }
            }
            
            Logg.InfoMessage("End download products: " + DateTime.Now);
            Log.AddInfo("End download products: " + DateTime.Now);
            //Logg.InfoMessage("Start fill recomended price: " + DateTime.Now, "");
            //GetSID();
            //foreach (var brainProduct in BrainProducts)
            //{
            //    var res = BrainCommonFacade.GetProduct(brainProduct.productID, SID).Result;
            //    if (res == null)
            //    {
            //        GetSID();
            //        res = BrainCommonFacade.GetProduct(brainProduct.productID, SID).Result;
            //    }
            //    if (res != null && res.status != 1)
            //    {
            //        GetSID();
            //        res = BrainCommonFacade.GetProduct(brainProduct.productID, SID).Result;
            //    }
            //    if (res != null && res.status == 1)
            //        brainProduct.priceRecomenedUAH = res.result.recommendable_price;
            //}
            //Logg.InfoMessage("End fill recomended price: " + DateTime.Now, "");
            EndDateTime = DateTime.Now;
        }

        private void AddProductsToDb()
        {
            StartDateTimeAdd = DateTime.Now;
            Logg.InfoMessage("Start add products: " + StartDateTimeAdd);
            Log.AddInfo("Start add products: " + StartDateTimeAdd);
            var listToAdd = new List<BrainProduct>();
            var vendors = Context.Vendors.ToList();
            var stocks = Context.BrainStockses.ToList();
            var i = 0;
            var index = 0;
            foreach (var brainProduct in BrainProducts)
            {
                i++;
                var product = Context.Products.Include("BrainStockses").FirstOrDefault(x => x.productID == brainProduct.productID);
                if (product == null)
                {
                    var vendor = vendors.FirstOrDefault(x => x.vendorID == brainProduct.vendorID);
                    if (vendor != null)
                        brainProduct.BrainVendorID = vendor.Id;
                    else
                    {
                        Logg.InfoMessage("Vendor == null, brainProduct.vendorID=" + brainProduct.vendorID,
                            JsonConvert.SerializeObject(brainProduct));
                        Log.AddInfo("Vendor == null, brainProduct.vendorID=" + brainProduct.vendorID + "\nJsonConvert.SerializeObject: \t" +
                            JsonConvert.SerializeObject(brainProduct));
                        vendor = vendors.FirstOrDefault(x => x.name == "Other");
                        if (vendor != null)
                            brainProduct.BrainVendorID = vendor.Id;}

                    if (brainProduct.stocks != null && brainProduct.stocks.Count > 0)
                    {
                        foreach (var stock in brainProduct.stocks)
                        {
                            var stockInDb = stocks.FirstOrDefault(x => x.stockID == stock);
                            if (stockInDb != null)
                            {
                                if(brainProduct.BrainStockses == null || brainProduct.BrainStockses.Count ==0)
                                    brainProduct.BrainStockses = new List<BrainStocks>();
                                brainProduct.BrainStockses.Add(stockInDb);
                            }
                            else
                            {
                                Logg.InfoMessage("stockInDb == null, brainProduct.stocks.stockId=" + stock,
                                    JsonConvert.SerializeObject(brainProduct));
                                Log.AddInfo("stockInDb == null, brainProduct.stocks.stockId=" + stock + "\nJsonConvert.SerializeObject: \t" +
                                    JsonConvert.SerializeObject(brainProduct));
                            }
                        }
                    }
                    var category = Context.Categories.FirstOrDefault(x => x.categoryID == brainProduct.categoryID);
                    //TODO В случай оптимизации импорта load categories from context (local) search category in local categories
                    if (category != null)
                        brainProduct.BrainCategoryID = category.Id;
                    else
                    {
                        Logg.InfoMessage("categoryInDb == null, brainProduct.categoryID=" + brainProduct.categoryID,
                            JsonConvert.SerializeObject(brainProduct));
                        Log.AddInfo("categoryInDb == null, brainProduct.categoryID=" + brainProduct.categoryID + "\nJsonConvert.SerializeObject: \t" +
                            JsonConvert.SerializeObject(brainProduct));
                    }

                    listToAdd.Add(brainProduct);
                }
                else
                {
                    if(Math.Abs(product.volume - brainProduct.volume) > 0)
                        product.volume = brainProduct.volume;
                    if(product.warranty != brainProduct.warranty)
                        product.warranty = brainProduct.warranty;
                    if(product.brief_description != brainProduct.brief_description)
                        product.brief_description = brainProduct.brief_description;
                    if(product.is_archive != brainProduct.is_archive)
                        product.is_archive = brainProduct.is_archive;
                    if(product.is_new != brainProduct.is_new)
                        product.is_new = brainProduct.is_new;
                    if (product.articul != brainProduct.articul)
                        product.articul = brainProduct.articul;
                    //if(product.MainImage != brainProduct.large_image)
                    //    product.large_image = brainProduct.large_image;
                    //if(product.medium_image != brainProduct.medium_image)
                    //    product.medium_image = brainProduct.medium_image;
                    if(product.name != brainProduct.name)
                        product.name = brainProduct.name;
                    if(product.price != brainProduct.price)
                        product.price = brainProduct.price;
                    if(product.price_uah != brainProduct.price_uah)
                        product.price_uah = brainProduct.price_uah;
                    //if(product.small_image != brainProduct.small_image)
                    //    product.small_image = brainProduct.small_image;

                    if (brainProduct.stocks != null)
                    {
                        var flag = false;
                        if (product.BrainStockses.Count != brainProduct.stocks.Count)
                        {
                            flag = true;
                            while (product.BrainStockses.Count > 0)
                                product.BrainStockses.Remove(product.BrainStockses[0]);
                        }
                        
                        foreach (var stock in brainProduct.stocks)
                        {
                            var st = product.BrainStockses.FirstOrDefault(x => x.stockID == stock);
                            if (st == null)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            while (product.BrainStockses.Count > 0)
                                product.BrainStockses.Remove(product.BrainStockses[0]);
                            foreach (var stock in brainProduct.stocks)
                            {
                                var st = stocks.FirstOrDefault(x => x.stockID == stock);
                                if (st != null)
                                {
                                    if(product.BrainStockses == null || product.BrainStockses.Count == 0)
                                        product.BrainStockses = new List<BrainStocks>();
                                    product.BrainStockses.Add(st);
                                }
                                else
                                    Logg.InfoMessage(
                                        "var st = stocks.FirstOrDefault(x => x.stockID == stock) == null, stock=" +
                                        stock, JsonConvert.SerializeObject(brainProduct));
                            }
                        }
                    }
                    else if (product.BrainStockses.Count > 0 && (brainProduct.stocks == null || brainProduct.stocks.Count == 0))
                    {
                        while (product.BrainStockses.Count > 0)
                            product.BrainStockses.Remove(product.BrainStockses[0]);
                    }
                    if (brainProduct.vendorID != 0 && brainProduct.vendorID != product.vendorID)
                    {
                        var vendor = vendors.FirstOrDefault(x => x.vendorID == brainProduct.vendorID);
                        if (vendor != null)
                        {
                            product.BrainVendorID = vendor.Id;
                            product.vendorID = brainProduct.vendorID;
                        }
                        else
                            Logg.InfoMessage("vendor == null, brainProduct.vendorID=" + brainProduct.vendorID + " 1 Object - brainProduct, 2 Object - product from Db",
                                JsonConvert.SerializeObject(brainProduct), JsonConvert.SerializeObject(product));
                    }
                    if (brainProduct.categoryID != product.categoryID)
                    {
                        var category = Context.Categories.FirstOrDefault(x => x.categoryID == brainProduct.categoryID);
                        //TODO В случай оптимизации импорта load categories from context (local) search category in local categories
                        if (category != null)
                        {
                            product.BrainCategoryID = category.Id;
                            product.categoryID = category.categoryID;
                        }
                        else
                            Logg.InfoMessage("categoryInDb == null, brainProduct.categoryID=" + brainProduct.categoryID + " 1 Object - brainProduct, 2 Object - product from Db",
                                JsonConvert.SerializeObject(brainProduct), JsonConvert.SerializeObject(product));
                    }
                    if (i > 10000)
                    {
                        Context.SaveChanges();
                        //Context.Dispose();
                        //Context = new DbBrain();
                        i = 0;
                    }
                }
                //if (index > 1000 && listToAdd.Count > 0)
                //{
                //    Context.Products.AddRange(listToAdd);
                //    Context.SaveChanges();
                //    listToAdd = new List<BrainProduct>();
                //    index = 0;
                //}
            }
            Logg.InfoMessage("Products finished download from api.brain.com.ua: " + DateTime.Now);
            Log.AddInfo("Products finished download from api.brain.com.ua: " + DateTime.Now);
            if (listToAdd.Count > 0)
            {
                listToAdd = listToAdd.GroupBy(x => x.productID).Select(g => g.First()).ToList();
                Context.Products.AddRange(listToAdd);
                Context.SaveChanges();
                Context.Dispose();
                Context = new DbItFamily();
                Context.Dispose();
                Context = null;
            }
            var productIds = BrainProducts.Select(y => new TablesOfId { Id = y.productID }).ToList();
            var products = productIds.AsTableValuedParameter("dbo.TableOfId", new[] { "Id" });
            using (var gateWay = new DatabaseGateway())
            {
                gateWay.SetDeletedBrainProducts(products);
            }

            Logg.InfoMessage("IMPORT TO DbBrain FINISHED: " + DateTime.Now);
            Log.AddInfo("IMPORT TO DbBrain FINISHED: " + DateTime.Now);
        }
    }
}