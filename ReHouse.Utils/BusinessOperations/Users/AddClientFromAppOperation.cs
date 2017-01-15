using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Clients
{
    public class AddClientFromAppOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Contractor Contractor { get; set; }

        public AddClientFromAppOperation(string tokenHash, Contractor contractor)
        {
            TokenHash = tokenHash;
            Contractor = contractor;
            RussianName = "Добавление клиентов через приложение";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var role = Context.RoleSet.FirstOrDefault(x => x.Name == ConstV.RoleClient);
            if (role == null)
                throw new ObjectNotFoundException("Должность не найдена! Name = " + ConstV.RoleClient);
            var cont = Context.Contractors.FirstOrDefault(y => !y.Deleted && y.Email == Contractor.Email);
            if (cont != null)
                throw new ItFamilyException("Такой email уже существует!");
            Context.Contractors.Add(CreateClient(role, Contractor));
            Context.SaveChanges();
        }

        private static Contractor CreateClient(Role role, Contractor x)
        {
            var client = new Contractor
            {
                RoleId = role.Id,
                Password = x.Password,
                Email = x.Email,
                Phone = x.Phone,
                CreditLimit = 0,
                IsActive = true,
                Deleted = false,
                Url = x.Url,
                FatherName = x.FatherName,
                SecondName = x.SecondName,
                FirstName = x.FirstName,
                DeliveryAdditional = x.DeliveryAdditional,
                DeliveryStreet = x.DeliveryStreet,
                DeliveryAppartament = x.DeliveryAppartament,
                DeliveryCity = x.DeliveryCity,
                DeliveryHome = x.DeliveryHome,
            };
            return client;
        }
    }
}