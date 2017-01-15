using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.BusinessOperations.Clients
{
    public class LoadClientsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<ClientModel> ClientModels { get; set; }
        public LoadClientsOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Загрузка списка клиентов";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var clients = Context.Contractors.Where(x => !x.Deleted && x.Role.Name == ConstV.RoleClient).ToList();
            if (clients.Count > 0)
            {
                ClientModels = clients.Select(x => new ClientModel
                {
                    Id = x.Id,
                    //TODO Adress = x.Adress,
                    Email = x.Email,
                    FatherName = x.FatherName,
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    Url = x.Url,
                    Phone = x.Phone,
                    RoleId = x.RoleId,
                    IsActive = x.IsActive
                    //,Password = x.Password
                }).ToList();
            }
        }
    }
}