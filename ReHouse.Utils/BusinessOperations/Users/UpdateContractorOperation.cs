using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Clients
{
    public class UpdateContractorOperation : BaseOperation
    {
        public Contractor Contractor { get; set; }
        public String TokenHash { get; set; }

        public UpdateContractorOperation(Contractor contractor, string tokenHash)
        {
            Contractor = contractor;
            TokenHash = tokenHash;
            RussianName = "Изменять данные контр.агентов";
        }

        protected override void InTransaction()
        {
            var cont = Context.Contractors.FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            if(cont == null)
                throw new ObjectNotFoundException("Object Contractor not found id: " + Contractor.Id);
            if (cont.Id != Contractor.Id)
            {
                CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
                cont = Context.Contractors.FirstOrDefault(x => x.Id == Contractor.Id && !x.Deleted);
                if(cont != null)
                {
                    SetFields(cont);
                    SetOtherField(cont);
                }
            }
            else
            {
                SetFields(cont);
            }
            
            
            Context.SaveChanges();
        }

        private void SetFields(Contractor x)
        {
            x.DeliveryAdditional = Contractor.DeliveryAdditional;
            x.DeliveryStreet = Contractor.DeliveryStreet;
            x.DeliveryAppartament = Contractor.DeliveryAppartament;
            x.DeliveryCity = Contractor.DeliveryCity;
            x.DeliveryHome = Contractor.DeliveryHome;
            //x.Adress = Contractor.Adress;
            if (x.Email != Contractor.Email)
            {
                var otherEmail = Context.Contractors.FirstOrDefault(c => c.Email == Contractor.Email);
                if(otherEmail != null)
                    throw new ExistsObjectException("Такой email уже сужествует.");
                x.Email = Contractor.Email;
            }
            x.FatherName = Contractor.FatherName;
            x.FirstName = Contractor.FirstName;

            if (x.Phone != Contractor.Phone)
            {
                var otherPhone = Context.Contractors.FirstOrDefault(c => c.Phone == Contractor.Phone);
                if (otherPhone != null)
                    throw new ExistsObjectException("Такой телефон уже сужествует.");
                x.Phone = Contractor.Phone;
            }
            
            x.SecondName = Contractor.SecondName;
            x.Url = Contractor.Url;
            //Delivery

            x.FormOfTaxation = Contractor.FormOfTaxation;
            x.Ownership = Contractor.Ownership;
            x.Requisite = Contractor.Requisite;
            x.TaxRate = Contractor.TaxRate;
            x.FormOfTaxation = Contractor.FormOfTaxation;
        }

        private void SetOtherField(Contractor x)
        {
            if (Contractor.RoleId != 0 && CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.ChangeRoles))
                x.RoleId = Contractor.RoleId;
            if(CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.ChangeCreditLimit))
                x.CreditLimit = Contractor.CreditLimit;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.IsActive))
                x.IsActive = Contractor.IsActive;
            x.Password = Contractor.Password; 
        }
    }
}