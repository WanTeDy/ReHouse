using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.BusinessOperations.Auth
{
    public class ExternalSignInOperation : BaseOperation
    {
        private String ProviderKey { get; set; }
        public String GoogleEmail { get; set; }
        private Provider Provider { get; set; }

        public ExternalSignInOperation(string providerKey, Provider provider, string googleEmail)
        {
            ProviderKey = providerKey;
            Provider = provider;
            GoogleEmail = googleEmail;
        }

        public String TokenHash { get; set; }
        public Role Role { get; set; }
        public Contractor Contractor { get; set; }
        public String NameContractor { get; set; }
        public String SecondNameContractor { get; set; }
        public String FatherNameContractor { get; set; }
        public String Email { get; set; }
        public String DeliveryCity { get; set; }
        public String DeliveryStreet { get; set; }
        public String DeliveryHome { get; set; }
        public String DeliveryAppartament { get; set; }
        public String DeliveryAdditional { get; set; }

        public Decimal CourseExtractCashless { get; set; }
        public Decimal CourseCash { get; set; }
        public Decimal CreditLimit { get; set; }
        public List<AuthorityForOneRoleModel> AuthorityForOneRoleModels { get; set; }
        public Int32 QuantityProducts { get; set; }

        protected override void InTransaction()
        {
            QuantityProducts = 0;
            DataBase.Security.Contractor contr = null;
            if (!String.IsNullOrEmpty(Email) && Provider == Provider.Google)
            {
                contr = Context.Contractors.FirstOrDefault(x => x.GoogleKey == ProviderKey && !x.Deleted && x.IsActive);
                if (contr == null)
                {
                    contr = Context.Contractors.FirstOrDefault(x => x.Email == GoogleEmail && !x.Deleted && x.IsActive);
                    if (contr != null)
                        contr.GoogleKey = ProviderKey;
                }
            }
            else
            {
                contr = Context.Contractors.FirstOrDefault(x => (x.GoogleKey == ProviderKey || x.VkKey == ProviderKey || x.FacebookKey == ProviderKey) && !x.Deleted && x.IsActive);
            }
            Role role = null;
            if (contr == null)
            {
                role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleClient);
                if(role == null)
                    throw new ObjectNotFoundException("Не найден обьект. Role = " + ConstV.RoleClient);
                if (Provider == Provider.Facebook)
                {
                    contr = new Contractor
                    {
                        FacebookKey = ProviderKey,
                        RoleId = role.Id
                    };
                }
                else if (Provider == Provider.Google)
                {
                    contr = new Contractor
                    {
                        Email = GoogleEmail,
                        GoogleKey = ProviderKey,
                        RoleId = role.Id
                    };
                }
                else //if (Provider == Provider.Vk)
                {
                    contr = new Contractor
                    {
                        VkKey = ProviderKey,
                        RoleId = role.Id,
                    };
                }
                contr.Deleted = false;
                contr.IsActive = true;
                Context.Contractors.Add(contr);
                Context.SaveChanges();
                contr = null;
                contr =
                    Context.Contractors.FirstOrDefault(
                        x => (x.GoogleKey == ProviderKey || x.VkKey == ProviderKey || x.FacebookKey == ProviderKey) && !x.Deleted && x.IsActive);
                QuantityProducts = 0;
                if(contr == null)
                    return;
            }
            contr.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + contr.Password + Guid.NewGuid() + contr.Phone + Guid.NewGuid() + contr.Email);
            TokenHash = contr.TokenHash;
            NameContractor = contr.FirstName;
            FatherNameContractor = contr.FatherName;
            SecondNameContractor = contr.SecondName;
            Email = contr.Email;

            DeliveryCity = contr.DeliveryCity;
            DeliveryStreet = contr.DeliveryStreet;
            DeliveryHome = contr.DeliveryHome;
            DeliveryAppartament = contr.DeliveryAppartament;
            DeliveryAdditional = contr.DeliveryAdditional;

            CreditLimit = contr.CreditLimit;
            role = Context.RoleSet.Include("Authorities").FirstOrDefault(x => x.Id == contr.RoleId && !x.Deleted);
            if (role != null)
            {
                Role = new Role
                {
                    Id = role.Id,
                    Name = role.Name,
                    ProviderLogin1 = role.ProviderLogin1,
                    ProviderPassword1 = role.ProviderPassword1,
                    ProviderMd5Password1 = role.ProviderMd5Password1
                };
                //AuthorityForOneRoleModels = role.Authorities.Select(OurMaps.ConvertToModel).ToList();
                if (role.Name == ConstV.RolePartner)
                {
                    var courseExtr = Context.Currencies.Where(x => !x.Deleted && x.Name == ConstV.CourseExtractCashless && x.EnumBelongsType == BelongsType.OurCource);
                    if (courseExtr.Any())
                    {
                        var maxDate = courseExtr.Max(x => x.DateTime);
                        var extr = courseExtr.FirstOrDefault(x => x.DateTime == maxDate);
                        if (extr != null)
                            CourseExtractCashless = extr.Value;
                    }

                    var courseCash = Context.Currencies.Where(x => !x.Deleted && x.Name == ConstV.CourseCash && x.EnumBelongsType == BelongsType.OurCource);
                    if (courseExtr.Any())
                    {
                        var maxDate = courseCash.Max(x => x.DateTime);
                        var cash = courseCash.FirstOrDefault(x => x.DateTime == maxDate);
                        if (cash != null)
                            CourseCash = cash.Value;
                    }
                }
            }

            var order = Context.OrderComes.FirstOrDefault(
                    x => x.ContractorId == contr.Id && !x.Deleted && x.OrderType == OrderType.Draft);
            if (order != null && order.OrdersItems != null && order.OrdersItems.Any())
            {
                QuantityProducts = order.OrdersItems.Sum(x => x.quantity);
            }

            var contractor = new Contractor
            {
                FatherName = contr.FatherName,
                Email = contr.Email,
                SecondName = contr.SecondName,
                TokenHash = contr.TokenHash,
                CreditLimit = contr.CreditLimit,
                FirstName = contr.FirstName,
                Id = contr.Id,
                IsActive = contr.IsActive,
                Ownership = contr.Ownership,
                FormOfTaxation = contr.FormOfTaxation,
                Requisite = contr.Requisite,
                TaxRate = contr.TaxRate,
                Url = contr.Url,
                Phone = contr.Phone,
                DeliveryAdditional = contr.DeliveryAdditional,
                DeliveryAppartament = contr.DeliveryAppartament,
                DeliveryCity = contr.DeliveryCity,
                DeliveryHome = contr.DeliveryHome,
                DeliveryStreet = contr.DeliveryStreet
            };
            Contractor = contractor;

            Context.SaveChanges();
        }
    }
}