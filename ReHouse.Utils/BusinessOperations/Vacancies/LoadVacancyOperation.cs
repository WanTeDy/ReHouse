using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.Vacancies;

namespace ReHouse.Utils.BusinessOperations.Vacancies
{
    public class LoadVacancyOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _vacancyId { get; set; }
        public Vacancy _vacancy { get; set; }

        public LoadVacancyOperation(string tokenHash, int vacancyId)
        {
            _tokenHash = tokenHash;
            _vacancyId = vacancyId;
            RussianName = "Получение вакансии";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _vacancy = Context.Vacancies.FirstOrDefault(x => !x.Deleted && x.Id == _vacancyId);
        }
    }
}