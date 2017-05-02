using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.Vacancies;

namespace ReHouse.Utils.BusinessOperations.Vacancies
{
    public class LoadVacanciesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<Vacancy> _vacancies { get; set; }

        public LoadVacanciesOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех вакансий";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            _vacancies = Context.Vacancies.Where(x => !x.Deleted).OrderBy(x => x.Date).ToList();

        }
    }
}