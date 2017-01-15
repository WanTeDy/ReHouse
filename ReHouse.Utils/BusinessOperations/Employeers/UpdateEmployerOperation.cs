using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Employeers
{
    public class UpdateEmployerOperation : BaseOperation
    {
        public Contractor Employeer { get; set; }
        public Int32 RoleId { get; set; }
        public String TokenHash { get; set; }
        public UpdateEmployerOperation(Contractor employeer, int roleId, string tokenHash)
        {
            Employeer = employeer;
            RoleId = roleId;
            TokenHash = tokenHash;
            RussianName = "Изменение данных работника";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var role = Context.RoleSet.FirstOrDefault(x => x.Id == RoleId);
            if(role == null)
                throw new ObjectNotFoundException("Должность не найдена! Id = " + RoleId);
            var emp = Context.Contractors.FirstOrDefault(x => x.Id == Employeer.Id && !x.Deleted);
            if(emp == null)
                throw new ObjectNotFoundException("Изменения невозможны. Работник не найден. Id = " + Employeer.Id);

            SetObjInDb(emp);

            Context.SaveChanges();
        }
        private void SetObjInDb(Contractor lg)
        {
            lg.DeliveryAdditional = Employeer.DeliveryAdditional;
            lg.DeliveryStreet = Employeer.DeliveryStreet;
            lg.DeliveryAppartament = Employeer.DeliveryAppartament;
            lg.DeliveryCity = Employeer.DeliveryCity;
            lg.DeliveryHome = Employeer.DeliveryHome;
            lg.Url = Employeer.Url;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.ChangeCreditLimit))
                lg.CreditLimit = Employeer.CreditLimit;
            lg.Email = Employeer.Email;
            lg.FatherName = Employeer.FatherName;
            lg.FirstName = Employeer.FirstName;
            lg.FormOfTaxation = Employeer.FormOfTaxation;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.IsActive))
                lg.IsActive = Employeer.IsActive;
            lg.Ownership = Employeer.Ownership;
            lg.Password = Employeer.Password;
            lg.Phone = Employeer.Phone;
            lg.Requisite = Employeer.Requisite;
            if (CommonAccess.CheckContractorRoleAuthorityBool(Context, TokenHash, ConstV.ChangeRoles))
                lg.RoleId = RoleId;
            lg.SecondName = Employeer.SecondName;
            lg.TaxRate = Employeer.TaxRate;
        }
    }
}