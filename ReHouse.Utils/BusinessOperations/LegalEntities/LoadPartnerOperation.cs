using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class LoadPartnerOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 SelectedId { get; set; }

        public Contractor Entrepreneur { get; set; }
        public LoadPartnerOperation(string tokenHash, int selectedId)
        {
            TokenHash = tokenHash;
            SelectedId = selectedId;
            RussianName = "Загрузка данных конкретного партнера";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var part = Context.Contractors.Include("Role").FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if (part != null)
                Entrepreneur = new Contractor
                {
                    Id = part.Id,
                    Url = part.Url,
                    DeliveryAdditional = part.DeliveryAdditional,
                    DeliveryStreet = part.DeliveryStreet,
                    DeliveryAppartament = part.DeliveryAppartament,
                    DeliveryCity = part.DeliveryCity,
                    DeliveryHome = part.DeliveryHome,
                    CreditLimit = part.CreditLimit,
                    Email = part.Email,
                    FatherName = part.FatherName,
                    FirstName = part.FirstName,
                    FormOfTaxation = part.FormOfTaxation,
                    //IsOur = part.IsOur,
                    //Login = part.Login,
                    Ownership = part.Ownership,
                    Password = part.Password,
                    Phone = part.Phone,
                    Requisite = part.Requisite,
                    RoleId = part.RoleId,
                    Role = new Role
                    {
                        Name = part.Role.Name
                    },
                    SecondName = part.SecondName,
                    TaxRate = part.TaxRate,
                    IsActive = part.IsActive,
                };
        }
    }
}