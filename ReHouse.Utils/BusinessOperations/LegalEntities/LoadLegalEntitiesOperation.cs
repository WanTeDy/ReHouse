using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class LoadLegalEntitiesOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<EntrepreneurModel> EntrepreneurModels { get; set; }

        public LoadLegalEntitiesOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Загрузка списка партнеров(крупных покупателей)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var partners = Context.Contractors.Include("Role").Where(x => !x.Deleted && x.Role.Name == ConstV.RolePartner).Select(y => new EntrepreneurModel
            {
                Id = y.Id,
                Url = y.Url,
                //Adress = y.Adress, //TODO Adress
                CreditLimit = y.CreditLimit,
                //CustomerCards = y.CustomerCards.Where(x=>!x.Deleted).Select(x=>new CustomerCard
                //{
                //    DateTime = x.DateTime,
                //    Outgo = x.Outgo,
                //    Notes = x.Notes,
                //    Balance = x.Balance,
                //    ComesUsd = x.ComesUsd,
                //}).ToList(),
                Email = y.Email,
                FatherName = y.FatherName,
                FirstName = y.FirstName,
                FormOfTaxation = y.FormOfTaxation,
                Ownership = y.Ownership,
                Password = y.Password,
                Phone = y.Phone,
                Requisite = y.Requisite,
                RoleId = y.RoleId,
                SecondName = y.SecondName,
                TaxRate = y.TaxRate,
                IsActive = y.IsActive,
            }).ToList();

            foreach (var entrepreneurModel in partners)
            {
                var customerCard =
                    Context.CustomerCards.Where(x => x.ContractorId == entrepreneurModel.Id && !x.Deleted).ToList();
                if (customerCard.Count > 0)
                {
                    if (customerCard.Any(x => x.ComesMoney != null && !x.Deleted))
                    {
                        var paidDate = customerCard.Where(x => x.ComesMoney != null).Max(x => x.DateTime);
                        entrepreneurModel.LastPaidDate = paidDate;
                    }
                    var maxDate = customerCard.Max(x => x.DateTime);
                    var balance = customerCard.FirstOrDefault(x => x.DateTime == maxDate);
                    if (balance != null)
                        entrepreneurModel.Balance = balance.Balance;
                }
            }
            EntrepreneurModels = partners;
        }
    }
}