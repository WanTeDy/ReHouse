using System;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.Clients
{
    public class LoadClientOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 SelId { get; set; }
        public ClientModel ClientModel { get; set; }
        public LoadClientOperation(string tokenHash, int selId)
        {
            TokenHash = tokenHash;
            SelId = selId;
            RussianName = "Просмотр данных клиента";
        }

        protected override void InTransaction()
        {
            var cont = Context.Contractors.FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            if (cont != null && cont.Id != SelId)
                CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var client = Context.Contractors.FirstOrDefault(x => x.Id == SelId && !x.Deleted);
            if (client != null)
            {
                var cl = new ClientModel
                {
                    Id = client.Id,
                    //TokenHash = client.TokenHash,
                    //Login = client.Login,
                    //Password = "****",
                    RoleId = client.RoleId,
                    SecondName = client.SecondName,
                    Url = client.Url,
                    Phone = client.Phone,
                    FirstName = client.FirstName,
                    FatherName = client.FatherName,
                    //TODO Adress = client.Adress,
                    Email = client.Email,
                    Password = client.Password,
                    IsActive = client.IsActive
                };
                ClientModel = cl;
            }
        }
    }
}