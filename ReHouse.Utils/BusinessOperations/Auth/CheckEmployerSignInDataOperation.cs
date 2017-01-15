using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.BusinessOperations.Auth
{
    public class CheckEmployerSignInDataOperation : BaseOperation
    {
        private String Login { get; set; }
        private String Password { get; set; }
        public String TokenHash { get; set; }
        public Role Role { get; set; }
        public String ProviderLogin1 { get; set; }
        public String ProviderPassword1 { get; set; }
        public StatusRole StatusRole { get; set; }
        public CheckEmployerSignInDataOperation(string login, string password)
        {
            Login = login;
            Password = password;
        }

        protected override void InTransaction()
        {
            //StatusRole = StatusRole.Undefined;
            //var emp =
            //    Context.EmployeerSet.Include("Role").FirstOrDefault(
            //        x => x.Login == Login && x.Password == Password && !x.Deleted && x.IsActive);
            //
            //var legalEnt =
            //        Context.LegalEntitySet.Include("Role").FirstOrDefault(x => x.Login == Login && x.Password == Password && !x.Deleted);
            //var client = Context.ClientSet.Include("Role").FirstOrDefault(x => x.Login == Login && x.Password == Password && !x.Deleted);
            //
            //if (emp == null && legalEnt == null && client == null)
            //{
            //    throw new ObjectNotFoundException("Не найден обьект. Login = " + Login);
            //}
            //if (emp != null)
            //{
            //    emp.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + emp.Password + Guid.NewGuid() + emp.Login + Guid.NewGuid() + emp.Email);
            //    TokenHash = emp.TokenHash;
            //    if (emp.Role.Name == ConstV.RoleAdministrator)
            //        StatusRole = StatusRole.Administrator;
            //    else if (emp.Role.Name == ConstV.RoleManager)
            //        StatusRole = StatusRole.Manager;
            //    Role = new Role {Name = emp.Role.Name};
            //    ProviderLogin1 = emp.ProviderLogin1;
            //    ProviderPassword1 = emp.ProviderPassword1;
            //}
            //else if (legalEnt != null)
            //{
            //    legalEnt.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + legalEnt.Password + Guid.NewGuid() + legalEnt.Login + Guid.NewGuid() + legalEnt.Email);
            //    TokenHash = legalEnt.TokenHash;
            //    StatusRole = legalEnt.IsOur ? StatusRole.OurPartner : StatusRole.OtherPartner;
            //    Role = new Role { Name = legalEnt.Role.Name };
            //    var emp2 =
            //    Context.EmployeerSet.Include("Role").FirstOrDefault(
            //        x => x.FirstName == ConstV.RolePartner 
            //            && x.FatherName == ConstV.RolePartner 
            //            && x.SecondName == ConstV.RolePartner
            //            && !x.Deleted 
            //            && !x.IsActive 
            //            && x.Role.Name == ConstV.RolePartner);
            //    if (emp2 != null)
            //    {
            //        ProviderLogin1 = emp2.ProviderLogin1;
            //        ProviderPassword1 = emp2.ProviderPassword1;
            //    }
            //}
            //else if (client != null)
            //{
            //    client.TokenHash = GenerateHash.GetSha1Hash(Guid.NewGuid() + client.Password + Guid.NewGuid() + client.Login + Guid.NewGuid() + client.Email);
            //    TokenHash = client.TokenHash;
            //    Role = new Role { Name = client.Role.Name };
            //    StatusRole = StatusRole.Client;
            //    var emp2 =
            //    Context.EmployeerSet.Include("Role").FirstOrDefault(
            //        x => x.FirstName == ConstV.RoleClient
            //            && x.FatherName == ConstV.RoleClient
            //            && x.SecondName == ConstV.RoleClient
            //            && !x.Deleted
            //            && !x.IsActive
            //            && x.Role.Name == ConstV.RoleClient);
            //    if (emp2 != null)
            //    {
            //        ProviderLogin1 = emp2.ProviderLogin1;
            //        ProviderPassword1 = emp2.ProviderPassword1;
            //    }
            //}
            //Context.SaveChanges();
        }
    }
}