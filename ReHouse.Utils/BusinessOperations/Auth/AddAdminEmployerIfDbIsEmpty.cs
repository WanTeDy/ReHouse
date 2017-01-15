using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.BusinessOperations.Auth
{
    public class AddAdminEmployerIfDbIsEmpty : BaseOperation
    {
        protected override void InTransaction()
        {
            //var employer =
            //    Context.EmployeerSet.Include("Role")
            //        .FirstOrDefault(x => x.Role.Name == ConstV.RoleAdministrator && !x.Deleted && x.IsActive);
            ////var emp = Context.EmployeerSet.FirstOrDefault(x => x.Login == "admin" && x.Password == "admin" && !x.Deleted && x.IsActive);
            ////if (emp != null) return;
            //if (employer == null)
            //{
            //    var empl = new Employeer
            //    {
            //        FatherName = "admin",
            //        FirstName = "admin",
            //        SecondName = "admin",
            //        Adress = "admin",
            //        Login = "admin",
            //        Password = "admin",
            //        Phone = "admin",
            //        IsActive = true,
            //        Deleted = false,
            //        ProviderLogin1 = "Kovalchuk",
            //        ProviderPassword1 = GenerateHash.GetMd5Hash("t5y6u7i81!"),
            //    };
            //    var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleAdministrator && !x.Deleted);
            //    if (role == null)
            //    {
            //        Context.RoleSet.Add(new Role { Name = ConstV.RoleAdministrator });
            //        Context.SaveChanges();
            //        Context.Dispose();
            //        Context = new DbItFamily();
            //        role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleAdministrator && !x.Deleted);
            //        if (role == null) return;
            //    }
            //    empl.RoleId = role.Id;
            //    Context.EmployeerSet.Add(empl);
            //    var empl2 = new Employeer
            //    {
            //        FatherName = "admin",
            //        FirstName = "admin",
            //        SecondName = "admin",
            //        Adress = "admin",
            //        Login = "admin2",
            //        Password = "admin",
            //        Phone = "admin",
            //        IsActive = true,
            //        Deleted = false,
            //        ProviderLogin1 = "Kovalchuk",
            //        ProviderPassword1 = GenerateHash.GetMd5Hash("t5y6u7i81!"),
            //        RoleId = role.Id
            //    };
            //    var empl3 = new Employeer
            //    {
            //        FatherName = "admin",
            //        FirstName = "admin",
            //        SecondName = "admin",
            //        Adress = "admin",
            //        Login = "admin3",
            //        Password = "admin",
            //        Phone = "admin",
            //        IsActive = true,
            //        Deleted = false,
            //        ProviderLogin1 = "Kovalchuk",
            //        ProviderPassword1 = GenerateHash.GetMd5Hash("t5y6u7i81!"),
            //        RoleId = role.Id
            //    };
            //    Context.EmployeerSet.Add(empl2);
            //    Context.EmployeerSet.Add(empl3);
            //}
            //
            //
            //employer =
            //    Context.EmployeerSet.Include("Role")
            //        .FirstOrDefault(x => x.FirstName == ConstV.RolePartner
            //            && x.FatherName == ConstV.RolePartner
            //            && x.SecondName == ConstV.RolePartner
            //            && !x.Deleted
            //            && !x.IsActive
            //            && x.Role.Name == ConstV.RolePartner);
            //if (employer == null)
            //{
            //    var empl = new Employeer
            //    {
            //        FatherName = ConstV.RolePartner,
            //        FirstName = ConstV.RolePartner,
            //        SecondName = ConstV.RolePartner,
            //        Adress = ConstV.RolePartner,
            //        Phone = ConstV.RolePartner,
            //        Login = ConstV.RolePartner,
            //        Password = ConstV.RolePartner,
            //        IsActive = false,
            //        Deleted = false,
            //        ProviderLogin1 = "Kovalchuk",
            //        ProviderPassword1 = GenerateHash.GetMd5Hash("t5y6u7i81!"),
            //    };
            //    var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RolePartner && !x.Deleted);
            //    if (role == null)
            //    {
            //        Context.RoleSet.Add(new Role { Name = ConstV.RolePartner });
            //        Context.SaveChanges();
            //        Context.Dispose();
            //        Context = new DbItFamily();
            //        role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RolePartner && !x.Deleted);
            //        if (role == null) return;
            //    }
            //    empl.RoleId = role.Id;
            //    Context.EmployeerSet.Add(empl);
            //}
            //
            //employer =
            //    Context.EmployeerSet.Include("Role")
            //        .FirstOrDefault(x => x.FirstName == ConstV.RoleClient
            //            && x.FatherName == ConstV.RoleClient
            //            && x.SecondName == ConstV.RoleClient
            //            && !x.Deleted
            //            && !x.IsActive
            //            && x.Role.Name == ConstV.RoleClient);
            //if (employer == null)
            //{
            //    var empl = new Employeer
            //    {
            //        FatherName = ConstV.RoleClient,
            //        FirstName = ConstV.RoleClient,
            //        SecondName = ConstV.RoleClient,
            //        Adress = ConstV.RoleClient,
            //        Phone = ConstV.RoleClient,
            //        Login = ConstV.RoleClient,
            //        Password = ConstV.RoleClient,
            //        IsActive = false,
            //        Deleted = false
            //    };
            //    var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleClient && !x.Deleted);
            //    if (role == null)
            //    {
            //        Context.RoleSet.Add(new Role { Name = ConstV.RoleClient });
            //        Context.SaveChanges();
            //        Context.Dispose();
            //        Context = new DbItFamily();
            //        role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleClient && !x.Deleted);
            //        if (role == null) return;
            //    }
            //    empl.RoleId = role.Id;
            //    Context.EmployeerSet.Add(empl);
            //}
            //
            //var part = Context.LegalEntitySet.FirstOrDefault(x => x.FatherName == "Отчество"
            //                                                      && x.SecondName == "Фамилия"
            //                                                      && x.FirstName == "Имя"
            //                                                      && x.Login == "p"
            //                                                      && x.Password == "p");
            //if (part == null)
            //{
            //    var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RolePartner && !x.Deleted);
            //    if(role != null)
            //    {
            //        var p = new Entrepreneur
            //        {
            //            Adress = "г.Одесса ул.Б.Арнаутского 25",
            //            Email = "19832partner19832@gmail.com",
            //            Phone = "+380 97 77 88 999",
            //            Login = "p",
            //            Password = "p",
            //            RoleId = role.Id,
            //            //CustomerCard = 123456789,
            //            FatherName = "Отчество",
            //            SecondName = "Фамилия",
            //            FirstName = "Имя",
            //            Requisite = "ИПС \"Законодательство\"",
            //            Ownership = "ФОП",
            //        };
            //        Context.LegalEntitySet.Add(p);
            //        Context.SaveChanges();
            //    }
            //}
            //
            //var ourCurrency = Context.CollectionCurrencies.FirstOrDefault(x => x.Type == "ItFamily" && !x.Deleted);
            //if (ourCurrency == null)
            //{
            //    var ourCur = new CollectionCurrency
            //    {
            //        Name = ConstV.OurCurrencyName,
            //        Type = "ItFamily",
            //        Currencies = new List<DataBase.Currencies.Currency>
            //        {
            //            new DataBase.Currencies.Currency
            //            {
            //                Name = ConstV.CourseExtractCashless,
            //                Value = 0,
            //            },
            //            new DataBase.Currencies.Currency
            //            {
            //                Name = ConstV.CourseEnrollmentCashless,
            //                Value = 0,
            //            },
            //            new DataBase.Currencies.Currency
            //            {
            //                Name = ConstV.CourseCash,
            //                Value = 0,
            //            }
            //        },
            //    };
            //    Context.CollectionCurrencies.Add(ourCur);
            //    Context.SaveChanges();
            //}
            //var providerCurrency = Context.CollectionCurrencies.FirstOrDefault(x => x.Type == "Brain" && !x.Deleted);
            //if (providerCurrency == null)
            //{
            //    var cur = new CollectionCurrency
            //    {
            //        Name = ConstV.Provider1CurrencyName,
            //        Type = "Brain",
            //    };
            //    Context.CollectionCurrencies.Add(cur);
            //    Context.SaveChanges();
            //}
            //
            //Context.SaveChanges();
        }
    }
}