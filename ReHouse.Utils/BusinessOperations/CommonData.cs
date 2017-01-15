using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations
{
    /// <summary>
    /// Methods that works with Groups and Menu
    /// </summary>
    public static class Common
    {
        //public static List<EmployeerModel> Employeers
        //{
        //    get
        //    {
        //        var op = new ContextOperation<List<EmployeerModel>>(
        //            cont =>
        //                cont.EmployeerSet.Where(x => !x.Deleted).Include("Role")
        //                .ToList().Select(x => new EmployeerModel
        //                {
        //                    Id = x.Id,
        //                    FatherName = x.FatherName,
        //                    SecondName = x.SecondName,
        //                    IsBlocked = x.IsActive ? "Нет" : "Да",
        //                    Adress = x.Adress,
        //                    Login = x.Login,
        //                    FirstName = x.FirstName,
        //                    Url = x.Url,
        //                    Email = x.Email,
        //                    Phone = x.Phone,
        //                    RoleName = x.Role.Name
        //                }).ToList());
        //        return op.ExcecuteTransaction();
        //    }
        //}

        //public static List<ClientModel> Clients
        //{
        //    get
        //    {
        //        var op = new ContextOperation<List<ClientModel>>(
        //            cont =>
        //                cont.ClientSet.Where(x => !x.Deleted)
        //                .ToList().Select(x => new ClientModel
        //                {
        //                    Id = x.Id,
        //                    Adress = x.Adress,
        //                    Email = x.Email,
        //                    FatherName = x.FatherName,
        //                    FirstName = x.FirstName,
        //                    SecondName = x.SecondName,
        //                    Url = x.Url,
        //                    Phone = x.Phone,
        //                    //Login = x.Login
        //                }).ToList());
        //        return op.ExcecuteTransaction();
        //    }
        //}

        //public static List<Entrepreneur> LegalEntities
        //{
        //    get
        //    {
        //        var op = new ContextOperation<List<Entrepreneur>>(
        //            cont =>
        //                cont.LegalEntitySet.Where(x => !x.Deleted).Select(y=>new Entrepreneur
        //                {
        //                    Id = y.Id,
        //                    Url = y.Url,
        //                    Adress = y.Adress,
        //                    CreditLimit = y.CreditLimit,
        //                    //CustomerCards = y.CustomerCards.Where(x=>!x.Deleted).Select(x=>new CustomerCard
        //                    //{
        //                    //    DateTime = x.DateTime,
        //                    //    Outgo = x.Outgo,
        //                    //    Notes = x.Notes,
        //                    //    Balance = x.Balance,
        //                    //    ComesUsd = x.ComesUsd,
        //                    //}).ToList(),
        //                    Email = y.Email,
        //                    FatherName = y.FatherName,
        //                    FirstName = y.FirstName,
        //                    FormOfTaxation = y.FormOfTaxation,
        //                    Login = y.FormOfTaxation,
        //                    Ownership = y.Ownership,
        //                    IsOur = y.IsOur,
        //                    Password = y.Password,
        //                    Phone = y.Phone,
        //                    Requisite = y.Requisite,
        //                    RoleId = y.RoleId,
        //                    SecondName = y.SecondName,
        //                    TaxRate = y.TaxRate,
        //                }).ToList());
        //        return op.ExcecuteTransaction();
        //    }
        //}
        public static List<OrderCities> OrderCities
        {
            get
            {
                var op = new ContextOperation<List<OrderCities>>(
                    cont =>
                        cont.OrderCities.ToList().Select(x => new OrderCities { Name = x.Name, Id = x.Id }).ToList());
                return op.ExcecuteTransaction();
            }
        }
        public static List<FrequencyPayment> FrequencyPayments
        {
            get
            {
                var op = new ContextOperation<List<FrequencyPayment>>(
                    cont =>
                        cont.FrequencyPayments.Where(x => !x.Deleted)
                        .ToList().Select(x=>new FrequencyPayment{ Name = x.Name, Id = x.Id }).ToList());
                return op.ExcecuteTransaction();
            }
        }

        public static List<Services> Services
        {
            get
            {
                var op = new ContextOperation<List<Services>>(
                    cont =>
                        cont.Serviceses.Where(x => !x.Deleted)
                        .ToList().Select(x => new Services
                        {
                            Name = x.Name,
                            PriceMin = x.PriceMin,
                            PriceRec = x.PriceRec,
                            PriceRetail = x.PriceRetail,
                            FrequencyPayment = new FrequencyPayment
                            {
                                Id = x.FrequencyPayment.Id,
                                Name = x.FrequencyPayment.Name
                            },
                            FrequencyPaymentId = x.FrequencyPaymentId,
                            Id = x.Id
                        }).ToList());
                return op.ExcecuteTransaction();
            }
        }

        public static List<RoleModel> RoleModel
        {
            get
            {
                var op = new ContextOperation<List<RoleModel>>(
                    cont =>
                        cont.RoleSet.Where(x => !x.Deleted)
                        .ToList().Select(x => new RoleModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                        }).ToList());
                return op.ExcecuteTransaction();
            }
        }

        public static Services LoadServices(int selId)
        {
            var op = new ContextOperation<Services>(c =>
            {
                var serv = c.Serviceses.FirstOrDefault(x => x.Id == selId && !x.Deleted);
                return new Services
                {
                    Name = serv.Name,
                    FrequencyPaymentId = serv.FrequencyPaymentId,
                    PriceRec = serv.PriceRec,
                    PriceMin = serv.PriceMin,
                    PriceRetail = serv.PriceRetail,
                    Id = serv.Id,
                    FrequencyPayment = new FrequencyPayment
                    {
                        Name = serv.FrequencyPayment.Name,
                        Id = serv.FrequencyPayment.Id
                    }
                };
            });
            return op.ExcecuteTransaction();
        }

        public static FrequencyPayment LoadFrequency(int selId)
        {
            var op = new ContextOperation<FrequencyPayment>(c =>
            {
                var freq = c.FrequencyPayments.FirstOrDefault(x => x.Id == selId && !x.Deleted);
                if(freq != null)
                {
                    var res = new FrequencyPayment
                    {
                        Name = freq.Name,
                        Id = freq.Id
                    };
                    return res;
                }
                return null;
            });
            return op.ExcecuteTransaction();
        }

        //public static Employeer LoadEmployeer(int selId)
        //{
        //    var op = new ContextOperation<Employeer>(c =>
        //    {
        //        var employeer = c.EmployeerSet.Include("Role").FirstOrDefault(x => x.Id == selId && !x.Deleted);
        //        if(employeer != null)
        //        return new Employeer
        //        {
        //            Role = new Role{ Name = employeer.Role.Name, Id = employeer.Role.Id },
        //            Adress = employeer.Adress,
        //            Id = employeer.Id,
        //            Email = employeer.Email,
        //            FatherName = employeer.FatherName,
        //            FirstName = employeer.FirstName,
        //            Login = employeer.Login,
        //            Password = employeer.Login,
        //            Phone = employeer.Phone,
        //            SecondName = employeer.SecondName,
        //            IsActive = employeer.IsActive,
        //            Url = employeer.Url,
        //            ProviderLogin1 = employeer.ProviderLogin1,
        //            ProviderPassword1 = employeer.ProviderPassword1,
        //            RoleId = employeer.RoleId,
        //            TokenHash = employeer.TokenHash
        //        };
        //        return null;
        //    });
        //    return op.ExcecuteTransaction();
        //}

        //public static Entrepreneur LoadLegalEntity(int selId)
        //{
        //    var op = new ContextOperation<Entrepreneur>(c =>
        //    {
        //        var legalEntity = c.LegalEntitySet.FirstOrDefault(x => x.Id == selId && !x.Deleted);
        //        return legalEntity;
        //    });
        //    return op.ExcecuteTransaction();
        //}
    }
}