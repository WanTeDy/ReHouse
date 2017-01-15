using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.ServicesOp
{
    public class AddServiceOperation : BaseOperation
    {
        public Services Service { get; set; }
        public String TokenHash { get; set; }

        public AddServiceOperation(Services service, string tokenHash)
        {
            Service = service;
            TokenHash = tokenHash;
            RussianName = "Добавление услуги";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var serv = Context.Serviceses.FirstOrDefault(x => x.Name == Service.Name && !x.Deleted);
            if(serv != null)
                throw new ObjectNotFoundException("Такой обьект уже существует! Имя: " + Service.Name);
            Context.Serviceses.Add(Service);
            Context.SaveChanges();
        }
    }
}