using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class UpdateLegalEntityOperation : BaseOperation
    {
        public Contractor Entrepreneur { get; set; }
        public String TokenHash { get; set; }

        public UpdateLegalEntityOperation(Contractor entrepreneur, string tokenHash)
        {
            Entrepreneur = entrepreneur;
            TokenHash = tokenHash;
            RussianName = "Изменение полей партнера";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            
            var legal = Context.Contractors.FirstOrDefault(x => x.Id == Entrepreneur.Id && !x.Deleted);
            if(legal == null)
                throw new ObjectNotFoundException("Object Entrepreneur not found id: " + Entrepreneur.Id);
            SetObjInDb(legal);

            Context.SaveChanges();
        }

        private void SetObjInDb(Contractor lg)
        {
            lg.DeliveryAdditional = Entrepreneur.DeliveryAdditional;
            lg.DeliveryStreet = Entrepreneur.DeliveryStreet;
            lg.DeliveryAppartament = Entrepreneur.DeliveryAppartament;
            lg.DeliveryCity = Entrepreneur.DeliveryCity;
            lg.DeliveryHome = Entrepreneur.DeliveryHome;
            lg.Url = Entrepreneur.Url;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.ChangeCreditLimit))
                lg.CreditLimit = Entrepreneur.CreditLimit;
            lg.Email = Entrepreneur.Email;
            lg.FatherName = Entrepreneur.FatherName;
            lg.FirstName = Entrepreneur.FirstName;
            lg.FormOfTaxation = Entrepreneur.FormOfTaxation;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.IsActive))
                lg.IsActive = Entrepreneur.IsActive;
            lg.Ownership = Entrepreneur.Ownership;
            lg.Password = Entrepreneur.Password;
            lg.Phone = Entrepreneur.Phone;
            lg.Requisite = Entrepreneur.Requisite;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.ChangeRoles))
                lg.RoleId = Entrepreneur.RoleId;
            lg.SecondName = Entrepreneur.SecondName;
            lg.TaxRate = Entrepreneur.TaxRate;
        }
    }
}