using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.Employeers
{
    public class LoadEmployeerOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 SelId { get; set; }
        public Contractor Employeer { get; set; }

        public LoadEmployeerOperation(string tokenHash, int selId)
        {
            TokenHash = tokenHash;
            SelId = selId;
            RussianName = "Загрузка данных конкретного работника";
        }

        protected override void InTransaction()
        {
            var employeer = Context.Contractors.Include("Role").FirstOrDefault(x => x.Id == SelId && !x.Deleted);
            if (employeer != null)
                Employeer = new Contractor
                {
                    Role = new Role { Name = employeer.Role.Name, Id = employeer.Role.Id },
                    DeliveryAdditional = employeer.DeliveryAdditional,
                    DeliveryStreet = employeer.DeliveryStreet,
                    DeliveryAppartament = employeer.DeliveryAppartament,
                    DeliveryCity = employeer.DeliveryCity,
                    DeliveryHome = employeer.DeliveryHome,
                    Id = employeer.Id,
                    Email = employeer.Email,
                    FatherName = employeer.FatherName,
                    FirstName = employeer.FirstName,
                    //Login = employeer.Login,
                    Password = employeer.Password,
                    Phone = employeer.Phone,
                    SecondName = employeer.SecondName,
                    IsActive = employeer.IsActive,
                    Url = employeer.Url,
                    //ProviderLogin1 = employeer.ProviderLogin1,
                    //ProviderPassword1 = employeer.ProviderPassword1,
                    RoleId = employeer.RoleId,
                    TokenHash = employeer.TokenHash,
                    CreditLimit = employeer.CreditLimit,
                };
        }
    }
}