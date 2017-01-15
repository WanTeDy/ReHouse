using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class RegisterLegalEntitiesOperation : BaseOperation
    {
         public Contractor LegalEntity { get; set; }

        public RegisterLegalEntitiesOperation(Contractor legalEntity)
        {
            LegalEntity = legalEntity;
        }

        protected override void InTransaction()
        {
            var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RolePartner);
            if (role == null)
                throw new ObjectNotFoundException("Role Name=" + ConstV.RolePartner);
            LegalEntity.RoleId = role.Id;
            var cont = Context.Contractors.FirstOrDefault(y => !y.Deleted && y.Email == LegalEntity.Email);
            if (cont != null)
                throw new ItFamilyException("Такой email уже существует!");
            Context.Contractors.Add(LegalEntity);
            Context.SaveChanges();
        }
    }
}