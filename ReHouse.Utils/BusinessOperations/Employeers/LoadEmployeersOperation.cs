using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.BusinessOperations.Employeers
{
    public class LoadEmployeersOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<EmployeerModel> EmployeerModels { get; set; }
        public LoadEmployeersOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Загрузка списка работников";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var employers = Context.Contractors.Include("Role").Where(x => !x.Deleted && (x.Role.Name == ConstV.RoleManager || x.Role.Name == ConstV.RoleAdministrator)).ToList();
            EmployeerModels = employers.Select(x => new EmployeerModel
            {
                Id = x.Id,
                FatherName = x.FatherName,
                SecondName = x.SecondName,
                IsBlocked = x.IsActive ? "Нет" : "Да",
                //TODO Adress = x.Adress,
                //Login = x.Login,
                FirstName = x.FirstName,
                Url = x.Url,
                Email = x.Email,
                Phone = x.Phone,
                RoleName = x.Role.Name,
            }).ToList();
        }
    }
}