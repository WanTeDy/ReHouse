using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.Brain.Response.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.Brain
{
    public class ImportToOurCategoriesAndVendors
    {
        private DbItFamily Context { get; set; }
        private List<BrainCategory> BrainCategories { get; set; }
        private List<Vendor> Vendors { get; set; }
        public ImportToOurCategoriesAndVendors()
        {
            Context = new DbItFamily();
            Context.Configuration.ProxyCreationEnabled = true;
            Context.Configuration.AutoDetectChangesEnabled = true;
        }
        private List<BrainCategory> Recurs(List<BrainCategory> outCategory, List<BrainCategory> sourceCategory)
        {
            foreach (var categoriesModel in outCategory)
            {
                BrainCategory model = categoriesModel;
                var addCategories = sourceCategory.Where(x => x.parentID == model.categoryID).ToList();
                if (addCategories.Count > 0)
                {
                    foreach (var brainCategory in addCategories)
                    {
                        categoriesModel.Categories.Add(new BrainCategory
                        {
                            Id = brainCategory.Id,
                            BrainParentID = brainCategory.BrainParentID,
                            categoryID = brainCategory.categoryID,
                            name = brainCategory.name,
                            parentID = brainCategory.parentID,
                            Deleted = brainCategory.Deleted,
                        });
                    }
                    //categoriesModel.Categories.AddRange(addCategories);
                }
                Recurs(categoriesModel.Categories, sourceCategory);
            }
            return outCategory;
        }
        private void AddCategories()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            
            var categories = Context.Categories.ToList();

            var hierarchy = categories.Where(x => x.parentID == 1 || x.BrainParentID == null).ToList(); //.Select(Mapper.Map<BrainCategory, BrainCategory>)
            var newEl = hierarchy.Select(brainCategory => new BrainCategory
            {
                categoryID = brainCategory.categoryID,
                name = brainCategory.name,
                parentID = brainCategory.parentID,
            }).ToList();
            foreach (var brainCategory in hierarchy)
                categories.Remove(brainCategory);
            BrainCategories = Recurs(newEl, categories);

            Context.Dispose();
            Context = new DbItFamily();
            Context.Configuration.ProxyCreationEnabled = true;
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.Configuration.ValidateOnSaveEnabled = true;

            var listToAdd = new List<ItFamilyCategory>();
            foreach (var brainCategory in BrainCategories)
            {
                var category = Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory.categoryID && x.FromWhatProvider == FromWhatProvider.Provider1);
                if (category == null)
                {
                    listToAdd.Add(
                        new ItFamilyCategory
                        {
                            Name = brainCategory.name,
                            CategoryId = brainCategory.categoryID,
                            ParentId = brainCategory.parentID,
                            FromWhatProvider = FromWhatProvider.Provider1,
                            Deleted = brainCategory.Deleted,
                            Categories = brainCategory.Categories.Select(x=>new ItFamilyCategory
                            {
                                Name = x.name,
                                ParentId = x.parentID,
                                CategoryId = x.categoryID,
                                FromWhatProvider = FromWhatProvider.Provider1,
                                Deleted = x.Deleted,
                                Categories = x.Categories.Select(y=> new ItFamilyCategory
                                {
                                    Name = y.name,
                                    CategoryId = y.categoryID,
                                    ParentId = y.parentID,
                                    FromWhatProvider = FromWhatProvider.Provider1,
                                    Deleted = y.Deleted,
                                    Categories = y.Categories.Select(d=>new ItFamilyCategory
                                    {
                                        Name = d.name,
                                        CategoryId = d.categoryID,
                                        ParentId = d.parentID,
                                        FromWhatProvider = FromWhatProvider.Provider1,
                                        Deleted = d.Deleted,
                                        Categories = d.Categories.Select(f=>new ItFamilyCategory
                                        {
                                            Name = f.name,
                                            CategoryId = f.categoryID,
                                            ParentId = f.parentID,
                                            FromWhatProvider = FromWhatProvider.Provider1,
                                            Deleted = f.Deleted,
                                        }).ToList()
                                    }).ToList()
                                }).ToList()
                            }).ToList()
                        });
                }
                else
                {
                    var flag = false;
                    //if (category.Name != null && category.Name != brainCategory.name)
                    //{
                    //    category.Name = brainCategory.name;
                        category.Deleted = brainCategory.Deleted;
                    //    flag = true;
                    //}
                    foreach (var brainCategory1 in brainCategory.Categories)
                    {
                        var cat = category.Categories.FirstOrDefault(x => x.CategoryId == brainCategory1.categoryID);
                        if (cat != null)
                        {
                            cat.Deleted = brainCategory1.Deleted;
                            //if (brainCategory1.name != null && brainCategory1.name != cat.Name)
                            //{
                            //    cat.Name = brainCategory1.name;
                            //    cat.Deleted = brainCategory1.Deleted;
                                flag = true;
                            //}
                        }
                        else
                        {
                            Context.ItFamilyCategories.Add(new ItFamilyCategory
                            {
                                Name = brainCategory1.name,
                                CategoryId = brainCategory1.categoryID,
                                ParentId = brainCategory1.parentID,
                                FromWhatProvider = FromWhatProvider.Provider1,
                                Deleted = brainCategory1.Deleted,
                                ItFamilyParentId = category.Id,
                            });
                            flag = true;
                        }
                        Context.SaveChanges();
                        foreach (var category1 in brainCategory1.Categories)
                        {
                            var cat2 = Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == category1.categoryID);
                            if (cat2 != null)
                            {
                                cat2.Deleted = category1.Deleted;
                                //if (category1.name == null || category1.name == cat2.Name) continue;
                                //cat2.Name = category1.name;
                                flag = true;
                            }
                            else
                            {
                                var catPar = Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == category1.parentID);
                                if (catPar == null) continue;
                                Context.ItFamilyCategories.Add(new ItFamilyCategory
                                {
                                    Name = category1.name,
                                    CategoryId = category1.categoryID,
                                    ParentId = category1.parentID,
                                    FromWhatProvider = FromWhatProvider.Provider1,
                                    Deleted = category1.Deleted,
                                    ItFamilyParentId = catPar.Id,
                                });
                                flag = true;
                            }
                            Context.SaveChanges();
                            foreach (var brainCategory2 in category1.Categories)
                            {
                                var cat3 = Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory2.categoryID);
                                if (cat3 != null)
                                {
                                    cat3.Deleted = brainCategory2.Deleted;
                                    //if (category1.name == null || brainCategory2.name == cat3.Name) continue;
                                    //cat3.Name = brainCategory2.name;
                                    flag = true;
                                }
                                else
                                {
                                    var catPar = Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory2.parentID);
                                    if (catPar == null) continue;
                                    Context.ItFamilyCategories.Add(new ItFamilyCategory
                                    {
                                        Name = brainCategory2.name,
                                        CategoryId = brainCategory2.categoryID,
                                        ParentId = brainCategory2.parentID,
                                        FromWhatProvider = FromWhatProvider.Provider1,
                                        Deleted = brainCategory2.Deleted,
                                        ItFamilyParentId = catPar.Id,
                                    });
                                    flag = true;
                                }
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
            {
                Context.ItFamilyCategories.AddRange(listToAdd);
                Context.SaveChanges();
            }
            
            Context.Dispose();
            Context = new DbItFamily();
        }

        private void AddVendors()
        {
            var vendors = Context.Vendors.Where(x => !x.Deleted).Include("BrainCategories").ToList();

            var listToAdd = new List<ItFamilyVendor>();
            foreach (var vendor in vendors)
            {
                var flag = false;
                var existVendor = Context.ItFamilyVendors.FirstOrDefault(x => x.VendorId == vendor.vendorID && x.FromWhatProvider == FromWhatProvider.Provider1);
                if (existVendor != null)
                {
                    foreach (var brainCategory in vendor.BrainCategories)
                    {
                        var category =
                            Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory.categoryID);
                        var existCategory =
                            existVendor.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory.categoryID);
                        if (category == null || existCategory != null) continue;
                        existVendor.ItFamilyCategories.Add(category);
                        flag = true;
                    }
                    if (!String.IsNullOrEmpty(existVendor.Name) && existVendor.Name != vendor.name)
                    {
                        existVendor.Name = vendor.name;
                        flag = true;
                    }
                    if (flag)
                        Context.SaveChanges();
                }
                else
                {
                    var categor = vendor.BrainCategories.Select(brainCategory => Context.ItFamilyCategories.FirstOrDefault(x => x.CategoryId == brainCategory.categoryID && x.FromWhatProvider == FromWhatProvider.Provider1)).ToList();

                    listToAdd.Add(new ItFamilyVendor
                    {
                        CategoryId = vendor.categoryID,
                        Name = vendor.name,
                        VendorId = vendor.vendorID,
                        ItFamilyCategories = categor,
                        FromWhatProvider = FromWhatProvider.Provider1,
                    });
                }
            }
            if (listToAdd.Count > 0)
                Context.ItFamilyVendors.AddRange(listToAdd);

            Context.SaveChanges();
            Context.Dispose();
        }

        public void Import()
        {
            AddCategories();
            AddVendors();
        }
    }
}