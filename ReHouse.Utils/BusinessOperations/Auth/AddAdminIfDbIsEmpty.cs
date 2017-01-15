using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.BusinessOperations.Auth
{
    public class AddAdminIfDbIsEmpty: BaseOperation
    {
        protected override void InTransaction()
        {
            var authorities = Context.Authorities.ToList();

            var employer = Context.Contractors.Include("Role")
                    .FirstOrDefault(x => x.Role.Name == ConstV.RoleAdministrator && !x.Deleted && x.IsActive);
            if (employer == null)
            {
                var empl = new Contractor
                {
                    FatherName = "admin",
                    FirstName = "admin",
                    SecondName = "admin",
                    Password = "admin",
                    Phone = "admin",
                    IsActive = true,
                    Deleted = false,
                    
                };
                var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleAdministrator && !x.Deleted);
                if (role == null)
                {
                    Context.RoleSet.Add(new Role
                    {
                        Name = ConstV.RoleAdministrator,
                        ProviderLogin1 = "Kovalchuk",
                        ProviderMd5Password1 = GenerateHash.GetMd5Hash("t5y6u7i81!"),
                        ProviderPassword1 = "t5y6u7i81!",
                        Authorities = authorities
                    });
                    Context.SaveChanges();
                    Context.Dispose();
                    Context = new DbItFamily();
                    role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleAdministrator && !x.Deleted);
                    if (role == null) return;
                }
                else if (String.IsNullOrEmpty(role.ProviderLogin1) || String.IsNullOrEmpty(role.ProviderMd5Password1) ||
                         String.IsNullOrEmpty(role.ProviderPassword1))
                {
                    role.ProviderLogin1 = "Kovalchuk";
                    role.ProviderMd5Password1 = GenerateHash.GetMd5Hash("t5y6u7i81!");
                    role.ProviderPassword1 = "t5y6u7i81!";
                }
                empl.RoleId = role.Id;
                Context.Contractors.Add(empl);
                var empl2 = OurMaps.Copy(empl);
                empl2.Phone = "admin2";
                Context.Contractors.Add(empl2);
            }
            else 
            if (String.IsNullOrEmpty(employer.Role.ProviderLogin1) || String.IsNullOrEmpty(employer.Role.ProviderMd5Password1) ||
                         String.IsNullOrEmpty(employer.Role.ProviderPassword1))
            {
                employer.Role.ProviderLogin1 = "Kovalchuk";
                employer.Role.ProviderMd5Password1 = GenerateHash.GetMd5Hash("t5y6u7i81!");
                employer.Role.ProviderPassword1 = "t5y6u7i81!";
            }

            //var part = Context.LegalEntitySet.FirstOrDefault(x => x.FatherName == "Отчество"
            //                                                      && x.SecondName == "Фамилия"
            //                                                      && x.FirstName == "Имя"
            //                                                      && x.Login == "p"
            //                                                      && x.Password == "p");
            //if (part == null)
            //{
            //    var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RolePartner && !x.Deleted);
            //    if (role != null)
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

            var ourCurrency = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && !x.Deleted);
            if (!ourCurrency.Any())
            {
                var curOur = new DataBase.Currencies.Currency
                {
                    Name = ConstV.CourseExtractCashless,
                    Value = 0,
                    DateTime = DateTime.Now,
                    BelongsCourse = ConstV.OurCurrencyName,
                    BelongsCourseType = ConstV.OurCurrencyName,
                    EnumBelongsType = BelongsType.OurCource,
                };
                var curOur2 = new DataBase.Currencies.Currency
                {
                    Name = ConstV.CourseExtractCashless,
                    Value = 0,
                    DateTime = DateTime.Now,
                    BelongsCourse = ConstV.OurCurrencyName,
                    BelongsCourseType = ConstV.OurCurrencyName,
                    EnumBelongsType = BelongsType.OurCource,
                };
                var curOur3 = new DataBase.Currencies.Currency
                {
                    Name = ConstV.CourseExtractCashless,
                    Value = 0,
                    DateTime = DateTime.Now,
                    BelongsCourse = ConstV.OurCurrencyName,
                    BelongsCourseType = ConstV.OurCurrencyName,
                    EnumBelongsType = BelongsType.OurCource,
                };
                //var ourCur = new CollectionCurrency
                //{
                //    Name = ConstV.OurCurrencyName,
                //    Type = "ItFamily",
                //    Currencies = new List<DataBase.Currencies.Currency>
                //    {
                //        new DataBase.Currencies.Currency
                //        {
                //            Name = ConstV.CourseExtractCashless,
                //            Value = 0,
                //        },
                //        new DataBase.Currencies.Currency
                //        {
                //            Name = ConstV.CourseEnrollmentCashless,
                //            Value = 0,
                //        },
                //        new DataBase.Currencies.Currency
                //        {
                //            Name = ConstV.CourseCash,
                //            Value = 0,
                //        }
                //    },
                //};
                //Context.CollectionCurrencies.Add(ourCur);
                Context.Currencies.Add(curOur3);
                Context.Currencies.Add(curOur2);
                Context.Currencies.Add(curOur);
                Context.SaveChanges();
            }
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

            Context.SaveChanges();
        }
    }
}