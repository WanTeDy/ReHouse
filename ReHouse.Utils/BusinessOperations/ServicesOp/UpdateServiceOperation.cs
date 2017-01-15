using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.ServicesOp
{
    public class UpdateServiceOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Services Service { get; set; }

        public UpdateServiceOperation(string tokenHash, Services service)
        {
            TokenHash = tokenHash;
            Service = service;
            RussianName = "Изменение услуги";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var serv = Context.Serviceses.FirstOrDefault(x => x.Id == Service.Id);
            if(serv == null)
                throw new ObjectNotFoundException("Обьект не найден! Id=" + Service.Id);
            serv.Name = Service.Name;
            serv.PriceMin = Service.PriceMin;
            serv.PriceRec = Service.PriceRec;
            serv.PriceRetail = Service.PriceRetail;
            serv.FrequencyPaymentId = Service.FrequencyPaymentId;
            Context.SaveChanges();
        }
    }
}